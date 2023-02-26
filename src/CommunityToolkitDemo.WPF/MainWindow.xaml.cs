using CommunityToolkitDemo.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CommunityToolkitDemo.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Get the MainViewModel
            this.DataContext = App.Current.Services.GetService<MainViewModel>();
        }
    }
}
