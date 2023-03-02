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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataContext">The DataContext injected with dependency injection</param>
        public MainViewModel(IToDoDataContext dataContext)
        {
            _DataContext = dataContext;
            RegisterMessages();
            LoadTodos();
        }

        /// <summary>
        /// Observable property with INotifyChanged created by source generators
        /// </summary>
        [ObservableProperty]
        public ObservableCollection<ToDo> _ToDos = new();

        /// <summary>
        /// MarkToDoAsCompleteCommand RelayCommand
        /// </summary>
        /// <param name="toDo">The ToDo to mark as compelte</param>
        [RelayCommand]
        private void MarkToDoAsComplete(ToDo toDo)
        {
            toDo.IsDone = true;
            _DataContext.ToDos.Remove(toDo);
            _DataContext.SaveChanges();
            LoadTodos();
        }

        /// <summary>
        /// Loads existing ToDos from the DataContext
        /// </summary>
        private void LoadTodos()
        {
            ToDos.Clear();
            var todos = _DataContext.ToDos.Where(o => !o.IsDone).ToList();
            todos.ForEach(o =>
            {
                ToDos.Add(o);
            });
        }

        /// <summary>
        /// Register message to handle a new ToDo created message and adds to ToDos collection
        /// </summary>
        private void RegisterMessages()
        {
            WeakReferenceMessenger.Default.Register<ToDoCreatedMessage>(this, (o, e) =>
            {
                ToDos.Add(e.Value);
            });
        }
    }
}
