using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Common.Models;
using ToDo.Common.Requests;

namespace ToDo.Common.Services
{
    //change to public
    //async not needed in interface
    public interface ITaskService
    {
        Task<Result> CreateTaskAsync(CreateTaskRequest request);
    }

    //color with async
    public class TaskService : ITaskService
    {

        //references the interface IFileDataService
        private readonly IFileDataService fileDataService; 
        public TaskService(IFileDataService fileDataService)
        {
            this.fileDataService = fileDataService; 
        }

        public async Task<Result> CreateTaskAsync(CreateTaskRequest request)
        {
            //must be here to compile
            //request -> map to model
            var modelResult = TaskModel.CreateTask(request);

            if (modelResult.IsErr())
            {
                return Result.Err(modelResult.GetErr()); 
            }
            await this.fileDataService.SaveAsync(modelResult.GetVal());
            return Result.Ok(); 
        }
    }
}
