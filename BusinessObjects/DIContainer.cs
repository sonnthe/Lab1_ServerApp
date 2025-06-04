using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class  DIContainer
    {
        public static IServiceProvider? ServiceProvider { get; set; }

        public static T Resolve<T>() where T : notnull
        {
            if (ServiceProvider == null)
                throw new InvalidOperationException("ServiceProvider is not initialized");
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}
