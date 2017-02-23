using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMPlusApp.Api
{
    public interface IClientValidator
    {
        bool Verify(string appId, string secret);
    }
}
