using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.JobsService.Services
{
    public interface IServiceCollectionProvider
    {
        IServiceCollection ServiceCollection { get; }
    }
}
