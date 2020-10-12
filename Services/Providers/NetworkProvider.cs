using Electrolux.ShopFloor.Middleware.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Electrolux.ShopFloor.iOS.Services.Providers
{
    public class NetworkProvider : INetworkInfo
    {
        public bool HasInternetAccess
        {
            get
            {
                // todo implement iOS Reachability
                return true;
            }
        }
    }
}
