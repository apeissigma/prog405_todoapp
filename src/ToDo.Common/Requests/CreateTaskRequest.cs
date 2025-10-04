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

        //remove set--no behavior, just data
        public string Name { get; }
        public string Description { get; }
        public DateTime DueDate { get; }

    }
}
