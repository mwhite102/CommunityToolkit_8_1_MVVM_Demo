using CommunityToolkitDemo.WPF.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityToolkitDemo.WPF.DataAccess
{
    public interface IToDoDataContext
    {
        DbSet<ToDo> ToDos { get; set; }

        int SaveChanges();
    }
}