using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.Model
{
    [Serializable]
    public enum EntityState
    {
        Unchanged = 1,
        Added = 2,
        Modified = 3,
        Deleted = 4,
        Detached = 5
    }
}
