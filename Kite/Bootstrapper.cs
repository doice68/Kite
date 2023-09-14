using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kite.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Kite.Services;
using Kite.Views;
using Kite.Models;
using System.Collections.ObjectModel;
using Kite.ViewModels.Popups;

namespace Kite
{
    public class Bootstrapper
    {
        public ServiceCollection Register()
        {
            var services = new ServiceCollection();
            
            RegisterServices(services);
            RegisterViews(services);
            RegisterViewModels(services);
            
            return services;
        }

        public void RegisterServices(IServiceCollection services) 
        {
            services.AddSingleton<RefrenceSrevice>();
            services.AddSingleton<FileDialogService>();
        }

        public void RegisterViews(IServiceCollection services) 
        {
            GetType()
                .Assembly
                .GetTypes()
                .Where(x => x.Name.EndsWith("View") || x.Name.EndsWith("Window"))
                .Foreach((t) => services.TryAddTransient(t));
        }

        public void RegisterViewModels(IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();

            services.AddTransient<RefrenceViewModel>();
            services.AddTransient<MoreMenuViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<SeasionConfigViewModel>();
            services.AddTransient<FinishedViewModel>();

            //GetType()
            //    .Assembly
            //    .GetTypes()
            //    .Where(x => x.Name.EndsWith("ViewModel"))
            //    .Where(x => !x.IsAbstract && !x.IsInterface)
            //    .Foreach(services.TryAddTransient);
        }
    }
}
