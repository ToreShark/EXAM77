using ToDo.DAL.Entities;

namespace ToDo.DAL.Interface;

public interface ITaskRepository
{
    TaskEntity GetTask(int id);
    IEnumerable<TaskEntity> GetAllTasks();
    TaskEntity AddTask(TaskEntity task);
    TaskEntity UpdateTask(TaskEntity task);
    void DeleteTask(int id);
}