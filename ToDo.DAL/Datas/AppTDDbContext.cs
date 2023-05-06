using Microsoft.EntityFrameworkCore;
using ToDo.DAL.Entities;

namespace ToDo.DAL.Datas;

public class AppTDDbContext : DbContext
{
    public AppTDDbContext(DbContextOptions<AppTDDbContext> options) : base(options)
    {
        
    }
    public DbSet<TaskEntity> Task { get; set; }
}