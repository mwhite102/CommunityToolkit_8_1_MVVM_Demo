using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkitDemo.WPF.DataAccess;
using CommunityToolkitDemo.WPF.Messages;
using CommunityToolkitDemo.WPF.Models;

namespace CommunityToolkitDemo.WPF.ViewModels
{
    public partial class NewToDoViewModel : ObservableObject
    {
        private readonly IToDoDataContext _DataContext;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddToDoCommand))]
        private string description = string.Empty;

        public NewToDoViewModel(IToDoDataContext dataContext)
        {
            _DataContext = dataContext;
        }

        [RelayCommand(CanExecute = nameof(AddToDoCanExecute))]
        private void AddToDo()
        {
            ToDo toDo = CreateToDo();
            _DataContext.ToDos.Add(toDo);
            _DataContext.SaveChanges();
            SendNewToDoCreatedMessage(toDo);
            ResetDescription();
        }

        private bool AddToDoCanExecute()
        {
            return !string.IsNullOrEmpty(Description);
        }

        private ToDo CreateToDo() 
        { 
            return new ToDo { Description = Description };
        }

        private void ResetDescription()
        {
            Description = string.Empty;
        }

        private void SendNewToDoCreatedMessage(ToDo toDo)
        {
            WeakReferenceMessenger.Default.Send(new ToDoCreatedMessage(toDo));
        }
    }
}
