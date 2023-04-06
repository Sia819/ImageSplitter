using System;
using ImageSpliter.ViewModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ImageSpliter
{
    public class ViewModelLocator
    {
        public IServiceProvider Services { private set; get; }

        public MainViewModel VM_MainViewModel => Ioc.Default.GetService<MainViewModel>()!;
        public Page1VM VM_Page1VM => Ioc.Default.GetService<Page1VM>()!;

        public ViewModelLocator()
        {

            // 서비스 콜렉션을 구성한 뒤, 제공할 서비스를 등록합니다.
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<Page1VM>();

            // 위에서 생성한 서비스 콜렉션을 통해 제공자를 등록합니다.
            this.Services = services.BuildServiceProvider();

            // 생성자에서 초기화 한 Services 데이터를 Ioc에 등록합니다.
            Ioc.Default.ConfigureServices(Services);
        }
    }
}
