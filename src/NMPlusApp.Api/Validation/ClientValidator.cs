using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMPlusApp.Api
{
    public class ClientValidator : IClientValidator
    {
        private IDictionary<string, string> appRegistration = new Dictionary<string, string>()
        {
            { "nmplusapp123" ,"nmplusappsecret123"}
        };
        bool IClientValidator.Verify(string appId, string secret)
        {
            return (appRegistration.ContainsKey(appId) && appRegistration[appId] == secret);
        }
    }
}
