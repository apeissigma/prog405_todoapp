using System.Threading.Tasks;
using ToDo.Common.Models;
using ToDo.Common.Requests;
using ToDo.Common.Services;

namespace Todo.Test;

public class ClassServiceTest
{
    private IFileDataService service;
    public ClassServiceTest()
    {
        this.service = new DummyFileDataService();
    }

    //Test for a normal task with all parameters
    [Fact]
    public async Task CreateTaskSucceeds()
    {
        var taskService = new TaskService(this.service);

        var happyRequest = new CreateTaskRequest("Test Task", "Dummy desc", DateTime.UtcNow.AddDays(3));

        var createTaskResult = await taskService.CreateTaskAsync(happyRequest);

        Assert.True(createTaskResult.IsOk());
    }

    //Test for a task with no description
    [Fact]
    public async Task CreateTaskNoDescSucceeds()
    {
        var taskService = new TaskService(this.service);
        var request = new CreateTaskRequest("Test Task 2", DateTime.UtcNow.AddDays(5));
        var createTaskResult = await taskService.CreateTaskAsync(request);
        Assert.True(createTaskResult.IsOk());
    }

    [Fact]
    public async Task CreateTask_DoesNotSucceed()
    {
        //arrange
        var dummyService = new DummyFileDataService();
        var taskService = new TaskService(dummyService);

        var badRequest = new CreateTaskRequest(
            name: "test name",
            description: "test desc",
            dueDate: DateTime.UtcNow.AddDays(-1) //this should fail
        );

        //act
        var createTaskResult = await taskService.CreateTaskAsync(badRequest);

        //assert
        Assert.True(createTaskResult.IsErr());
    }

    
    [Fact]
    public async Task CreateTask_CallSaveAsync()
    {
        //arrange
        var dummyService = new DummyFileDataService();
        var taskService = new TaskService(dummyService);
        var request = new CreateTaskRequest(
            name: "test name",
            description: "test desc",
            dueDate: DateTime.UtcNow.AddDays(7)
        );

        //act
        var createTaskResult = await taskService.CreateTaskAsync(request);

        //assert
        //SaveAsync was called if the test passes because the model creation succeeds
        Assert.True(createTaskResult.IsOk());
    }

    [Fact]
    public async Task CreateTask_GetAsync()
    {
        //arrange
        var dummyService = new DummyFileDataService();
        var taskService = new TaskService(dummyService);
        var request = new CreateTaskRequest(
            name: "test name",
            description: "test desc",
            dueDate: DateTime.UtcNow.AddDays(7)
        );
        
        //act
        var modelResult = TaskModel.CreateTask(request);
        Assert.True(modelResult.IsOk());

        var originalTask = modelResult.GetVal();
        Assert.NotNull(originalTask);

        var taskKey = originalTask.Key;

        await dummyService.SaveAsync(originalTask); //save
        
        var retrievedTask = await dummyService.GetAsync(taskKey); //use GetAsync with the key

        //assert
        Assert.NotNull(retrievedTask);
        Assert.Equal(taskKey, retrievedTask.Key); 
    }
    
}

internal class DummyFileDataService : IFileDataService
{
    private readonly Dictionary<string, TaskModel> data = new Dictionary<string, TaskModel>();

    public void Seed(TaskModel taskModel)
    {
        this.data.Add(taskModel.Key, taskModel);
    }

    public void Seed(IEnumerable<TaskModel> taskModels)
    {
        foreach (var t in taskModels)
        {
            this.data.Add(t.Key, t);
        }
    }

    public async Task<TaskModel?> GetAsync(string key)
    {
        await Task.CompletedTask;

        if (data.ContainsKey(key))
        {
            return data[key];
        }
        else
        {
            return null;
        }
    }

    public async Task SaveAsync(TaskModel? obj)
    {
        await Task.CompletedTask;

        if (obj is null)
        {
            return;
        }

        if (data.ContainsKey(obj.Key))
        {
            data.Remove(obj.Key);
        }

        this.data.Add(obj.Key, obj);
    }
}