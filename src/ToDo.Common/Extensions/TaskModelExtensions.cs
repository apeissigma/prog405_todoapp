using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ToDo.Common.Models; 

namespace ToDo.Common.Extensions
{
    public static class TaskModelExtensions
    {
        public static string ToFileName(this TaskModel model)
            => $"{model.Key}.json";

        //overload
        public static string ToFileName(string key) 
            => $"{key}.json";
    }
}
