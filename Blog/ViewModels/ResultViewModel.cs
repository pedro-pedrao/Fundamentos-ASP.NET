using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class ResultViewModel<T>
    {
        public ResultViewModel(T Data, List<String> errors)
        {
            Data = Data;
            Errors = errors;
        }

        public ResultViewModel(T data)
        {
            Data = data;
        }

        public ResultViewModel(string errors)
        {
            Errors.Add(errors);
        }

          public ResultViewModel(List <string> errors)
        {
            Errors = errors;
        }

        public T Data { get; private set; }
        public List<String> Errors { get; private set; } = new ();
    }
}