using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkitDemo.WPF.DataAccess;
using CommunityToolkitDemo.WPF.Messages;
using CommunityToolkitDemo.WPF.Models;
using System.ComponentModel.DataAnnotations;

namespace CommunityToolkitDemo.WPF.ViewModels
{
    public partial class NewToDoViewModel : ObservableValidator
    {
        private readonly IToDoDataContext _DataContext;

        private string description = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataContext">The DataContext injected with dependency injection</param>
        public NewToDoViewModel(IToDoDataContext dataContext)
        {
            _DataContext = dataContext;
        }

        /// <summary>
        /// Gets or sets the Description field for a new ToDo
        /// </summary>
        [Required]
        public string Description
        {
            get => description;
            set
            {
                // Set property and validate it
                SetProperty(ref description, value, true);
                // This will cause call to AddToDoCanExecute
                AddToDoCommand.NotifyCanExecuteChanged();
            }
        }

        /// <summary>
        /// AddToDoCommand RelayCommand
        /// </summary>
        [RelayCommand(CanExecute = nameof(AddToDoCanExecute))]
        private void AddToDo()
        {
            ToDo toDo = CreateToDo();
            _DataContext.ToDos.Add(toDo);
            _DataContext.SaveChanges();
            SendNewToDoCreatedMessage(toDo);
            ResetDescription();
            ClearErrors();
        }

        /// <summary>
        /// Can the AddToDoCommand execute?
        /// </summary>
        /// <returns></returns>
        private bool AddToDoCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Description);
        }

        /// <summary>
        /// Creates a new ToDo
        /// </summary>
        /// <returns></returns>
        private ToDo CreateToDo()
        {
            return new ToDo { Description = Description };
        }

        /// <summary>
        /// Clears the Description field after a new ToDo is created
        /// </summary>
        private void ResetDescription()
        {
            Description = string.Empty;
        }

        /// <summary>
        /// Sends message to MainViewModel with newly created ToDo that adds it to the list
        /// </summary>
        /// <param name="toDo"></param>
        private void SendNewToDoCreatedMessage(ToDo toDo)
        {
            WeakReferenceMessenger.Default.Send(new ToDoCreatedMessage(toDo));
        }
    }
}
