using Microsoft.Extensions.DependencyInjection;
using MVCCoreDBFirst.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCoreDBFirst.IoC
{
    public static class IoC
    {
        public static ApplicationDBContext ApplicationDBContext => IoCContainer.provider.GetService<ApplicationDBContext>();
    }
    public static class IoCContainer
{
        public static ServiceProvider provider;
    }
}
