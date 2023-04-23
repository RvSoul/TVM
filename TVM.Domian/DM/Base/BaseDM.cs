using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVM.Model;

namespace TVM.Domian.DM.Base
{
    public class BaseDM
    {
        public static IServiceProvider service = null;
        public TVMContext c;

        public BaseDM()
        {
            var scope = service.GetRequiredService<IServiceScopeFactory>().CreateScope();
            c = scope.ServiceProvider.GetService<TVMContext>();
        }
    }
}
