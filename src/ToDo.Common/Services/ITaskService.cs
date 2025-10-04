using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Common.Requests;

namespace ToDo.Common.Services
{
    //change to public
    //async not needed in interface
    public interface ITaskService
    {
        Task CreateTaskAsync(CreateTaskRequest request);
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

        public async Task CreateTaskAsync(CreateTaskRequest request)
        {
            //must be here to compile
            await Task.CompletedTask; 
        }
    }
}
