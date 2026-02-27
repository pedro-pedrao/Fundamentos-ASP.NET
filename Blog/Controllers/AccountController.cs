using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using SecureIdentity.Password;

namespace Blog.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        // private readonly TokenService _tokenService;
        // public AccountController(TokenService tokenService)
        // {
        //     _tokenService = tokenService;
        // }

        [HttpPost("v1/accounts/")]
        public async Task<IActionResult> Post([FromBody] RegisterViewModel model, [FromServices] BlogDataContext context)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            }

            var user = new User
            {
                Name = model.Nome,
                Email = model.Email,
                Slug = model.Email.Replace("@", "-").Replace(".", "-")
            };

            var password = PasswordGenerator.Generate(25);

            user.PasswordHash = PasswordHasher.Hash(password);

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Ok(
                    new ResultViewModel<dynamic>(new
                    {
                        user = user.Email,
                        password
                    })
                );
            }
            catch (DbUpdateException)
            {   
                return StatusCode(500, new ResultViewModel<string>("05XE99 - Este email já está cadastrado"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE4 - Falha interna no servidor"));
            }

        }

        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model, [FromServices] TokenService tokenService,
        [FromServices] BlogDataContext context)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            }

            var user = await context
                .Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
            {
                return StatusCode(401, new ResultViewModel<Category>("05XE12 - Usuário ou senha inválidos"));
            }

            if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
            {
                return StatusCode(401, new ResultViewModel<string>("05XE12 - Usuário ou senha inválidos"));
            }

            try
            {
                var token = tokenService.GenerateToken(user);
                return Ok(new ResultViewModel<string>(token, null));

            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE4 - Falha interna no servidor"));
            }


        }

        /*
                // [Authorize(Roles = "user")]
                // [HttpGet("v1/user")]
                // public IActionResult GetUser() => Ok(User.Identity.Name);

                // [Authorize(Roles = "author")]
                // [HttpGet("v1/author")]
                // public IActionResult GetAuthor() => Ok(User.Identity.Name);

                // [Authorize(Roles = "admin")]
                // [HttpGet("v1/admin")]
                // public IActionResult GetAdmin() => Ok(User.Identity.Name);
        */


    }
}