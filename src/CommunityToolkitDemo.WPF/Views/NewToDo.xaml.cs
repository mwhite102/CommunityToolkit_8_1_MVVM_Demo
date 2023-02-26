using CommunityToolkitDemo.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace CommunityToolkitDemo.WPF.Views
{
    /// <summary>
    /// Interaction logic for NewToDo.xaml
    /// </summary>
    public partial class NewToDo : UserControl
    {
        public NewToDo()
        {
            InitializeComponent();

            // Get the NewToDoModel
            this.DataContext = App.Current.Services.GetService<NewToDoViewModel>();
        }
    }
}
