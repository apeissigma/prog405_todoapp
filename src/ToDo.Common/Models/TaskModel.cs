using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Common.Requests;

namespace ToDo.Common.Models
{
    public class TaskModel
    {
        private TaskModel()
        {
            //MUST: exist
            this.Key = string.Empty;
            //MUST: exist
            this.Name = string.Empty;
            //optional
            this.Description = string.Empty;
            //MUST: be in the future & exist
            this.DueDate = DateTime.MinValue.ToUniversalTime(); 
        }

        public string Key { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime DueDate { get; private set; }

        //adds reference to request obj, maps
        public static Result<TaskModel> CreateTask(CreateTaskRequest request)
        {
            var validationResult = request.IsValid(); 
            if (validationResult.IsErr())
            {
                return Result<TaskModel>.Err(validationResult.GetErr());
            }

            return Result<TaskModel>.Ok(new TaskModel
            {
                Key = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                DueDate = request.DueDate
            });
        }
    }
}
