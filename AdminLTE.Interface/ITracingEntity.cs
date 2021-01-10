using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLTE.Interface
{
    public interface ITracingEntity : IEntity
    {
        string CreatedUser { get; set; }
        DateTime CreatedDate { get; set; }
        string ModifiedUser { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}
