using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkitDemo.WPF.DataAccess;
using CommunityToolkitDemo.WPF.Messages;
using CommunityToolkitDemo.WPF.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace CommunityToolkitDemo.WPF.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IToDoDataContext _DataContext;

        public MainViewModel(IToDoDataContext dataContext)
        {
            _DataContext = dataContext;
            RegisterMessages();
            LoadTodos();
        }

        [ObservableProperty]
        public ObservableCollection<ToDo> _ToDos = new();

        [RelayCommand]
        private void MarkToDoAsComplete(ToDo toDo)
        {
            toDo.IsDone = true;
            _DataContext.ToDos.Remove(toDo);
            _DataContext.SaveChanges();
            LoadTodos();
        }

        private void LoadTodos()
        {
            ToDos.Clear();
            var todos = _DataContext.ToDos.Where(o => !o.IsDone).ToList();
            todos.ForEach(o =>
            {
                ToDos.Add(o);
            });
        }

        private void RegisterMessages()
        {
            WeakReferenceMessenger.Default.Register<ToDoCreatedMessage>(this, (o, e) =>
            {
                ToDos.Add(e.Value);
            });
        }
    }
}
