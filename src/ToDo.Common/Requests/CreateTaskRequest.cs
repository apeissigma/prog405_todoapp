using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ToDo.Common.Requests
{
    //represents the data needed to create a task
    public class CreateTaskRequest
    {
        //DTO / accessors
        public CreateTaskRequest(string name, string description, DateTime dueDate)
        {
            //take in requesite data
            this.Name = name;
            this.Description = description;
            this.DueDate = dueDate; 
        }

        public CreateTaskRequest(string name, DateTime dueDate)
        {
            this.Name = name;
            this.DueDate = dueDate; 
        }

        //remove set--no behavior, just data
        public string Name { get; }
        public string Description { get; }
        public DateTime DueDate { get; }

        
        public Result IsValid()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                return Result.Err("Name Required"); 
            }

            if (this.DueDate <= DateTime.UtcNow)
            {
                return Result.Err("Due Date Must Be In Future");
            }

            return Result.Ok(); 
        }
    }
}
