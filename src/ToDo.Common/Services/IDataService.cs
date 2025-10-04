using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Common.Extensions;

//for using models
using ToDo.Common.Models;
//for json deserializer
using System.Text.Json; 

namespace ToDo.Common.Services
{
    public interface IFileDataService<T, TKey>
    {
        Task SaveAsync(T obj);
        Task<T> GetAsync(TKey id);
    }

    //interface that sits on top
    //optional
    //filedataservice inherits from dataserrvice with types
    public interface IFileDataService : IFileDataService<TaskModel?, string>
    {

    }


    public class FileDataService : IFileDataService//IDataService<TaskModel?, string>
    {
        //where file is saved
        private readonly string path;

        //TODO: configure ILogger
        //  ILogger<FileDataService> logger ...
        public FileDataService(string path)
        {
            this.path = path; 
        }

        //get model
        //reads a file and serializes in json


        public async Task<TaskModel?> GetAsync(string id)
        {
            try
            {
                //creates filename
                string fileName = TaskModelExtensions.ToFileName(id);
                //concatinates path with filename
                string combinedPath = Path.Combine(this.path, fileName);

                //add more guards
                //check combined path exists
                if (!File.Exists(combinedPath))
                {
                    Console.WriteLine($"File does not exist at path: {combinedPath}");
                    return null;
                    //throw new FileNotFoundException(combinedPath);
                }

                using StreamReader sr = new StreamReader(this.path);
                string text = await sr.ReadToEndAsync();

                //checks if file is empty
                if (string.IsNullOrWhiteSpace(text))
                {
                    Console.WriteLine($"Empty file at path: {combinedPath}");
                    return null;
                    //throw new InvalidDataException(); 
                }

                return JsonSerializer.Deserialize<TaskModel>(text);
            }
            catch (IOException)
            {
                Console.WriteLine($"Getting file failed for {id}");
                throw;
            }
            //checks deserializer
            catch (JsonException)
            {
                Console.WriteLine($"Deserializer File Failed");
                throw; 
            }
            //global exception catch
            catch (Exception)
            {
                Console.WriteLine("General fail"); 
                throw; 
            }
        }


        public Task<TaskModel?> GetAsync<TKey>(TKey id)
        {
            throw new NotImplementedException();
        }
        public Task SaveAsyn(TaskModel? obj)
        {
            throw new NotImplementedException();
        }



        //save model
        //text writer
        public async Task SaveAsync(TaskModel? obj)
        {
            if (obj is null)
            {
                return;
            }

            //TODO: test if overwriting is silent
            try
            {
                //call extension method
                string fileName = obj.ToFileName();
                string combinedPath = Path.Combine(this.path, fileName); 

                using StreamWriter sw = new StreamWriter(combinedPath);
                //take obj from class and turn into json
                string text = JsonSerializer.Serialize(obj);
                await sw.WriteAsync(text); 

            }
            catch (IOException)
            {
                //log and throw
                Console.WriteLine($"Failed writing file for Task {obj.Key}");
                throw; 
            }
            //checks serializer
            catch (JsonException)
            {
                Console.WriteLine($"Serializer File Failed");
                throw;
            }
            //global exception catch
            catch (Exception)
            {
                Console.WriteLine("General fail");
                throw;
            }
        }

    }

}
