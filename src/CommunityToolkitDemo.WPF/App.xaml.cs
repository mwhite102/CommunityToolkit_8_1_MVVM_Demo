using CommunityToolkitDemo.WPF.DataAccess;
using CommunityToolkitDemo.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace CommunityToolkitDemo.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // DataContext
            services.AddTransient<IToDoDataContext, ToDoDataContext>();

            // ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<NewToDoViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
