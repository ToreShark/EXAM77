using ToDo.BLL.Repositories;
using ToDo.DAL.Datas;

namespace ToDo.BLL.uow;

public class UnitOfWork : IDisposable
{
    private AppTDDbContext context;

    public UnitOfWork(AppTDDbContext _context)
    {
        context = _context;
    }
        
    private bool disposed = false;
    private TaskRepository taskRepository;

    public TaskRepository TaskRepository
    {
        get
        {
            if (taskRepository == null)
            {
                taskRepository = new TaskRepository(context);
            }
            return taskRepository;
        }
    }

    public void Save()
    {
        context.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}