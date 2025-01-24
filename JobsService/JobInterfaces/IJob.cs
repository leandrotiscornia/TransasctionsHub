using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.JobsService.JobInterfaces;

public interface IJob
{
    public string Name { get; }
    public Task Start();
    public Task Stop();

}
