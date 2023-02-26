using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkitDemo.WPF.Models;

namespace CommunityToolkitDemo.WPF.Messages
{
    public class ToDoCreatedMessage : ValueChangedMessage<ToDo>
    {
        public ToDoCreatedMessage(ToDo toDo) : base(toDo)
        {
        }
    }
}
