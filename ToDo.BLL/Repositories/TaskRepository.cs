using ToDo.DAL.Datas;
using ToDo.DAL.Entities;
using ToDo.DAL.Interface;

namespace ToDo.BLL.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppTDDbContext _context;

    public TaskRepository(AppTDDbContext context)
    {
        _context = context;
    }

    public TaskEntity GetTask(int id)
    {
        return _context.Task.Find(id);
    }

    public IEnumerable<TaskEntity> GetAllTasks()
    {
        return _context.Task.ToList();
    }

    public TaskEntity AddTask(TaskEntity task)
    {
        _context.Task.Add(task);
        _context.SaveChanges();
        return task;
    }

    public TaskEntity UpdateTask(TaskEntity task)
    {
        _context.Task.Update(task);
        _context.SaveChanges();
        return task;
    }

    public void DeleteTask(int id)
    {
        var task = _context.Task.Find(id);
        if (task != null)
        {
            _context.Task.Remove(task);
            _context.SaveChanges();
        }
    }
    public bool Exists(int id)
    {
        return _context.Task.Any(e => e.Id == id);
    }
}