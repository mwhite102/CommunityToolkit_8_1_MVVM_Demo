using CommunityToolkitDemo.WPF.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityToolkitDemo.WPF.DataAccess
{
    public class ToDoDataContext : DbContext, IToDoDataContext
    {
        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite("Data Source=ToDo.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToDo>().ToTable("Todos");
        }
    }
}
