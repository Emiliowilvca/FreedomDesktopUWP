using Freedom.UICore.Interface;
using Microsoft.Toolkit.Uwp.Connectivity;

namespace Freedom.UICore.Implement
{
    public class NetworkService : INetworkService
    {
        public bool CheckIfInternet()
        {
            return NetworkHelper.Instance.ConnectionInformation.IsInternetAvailable;
        }
    }

    // add project reference   - COM ---> Network List Manager 1.0 Type Library

    //Old
    //public class NetworkService : INetworkService
    //{
    //    private readonly INetworkListManager _networkListManager;

    //    public NetworkService()
    //    {
    //        _networkListManager = new NetworkListManager();
    //    }

    //    public bool CheckIfInternet()
    //    {
    //        return _networkListManager.IsConnectedToInternet;
    //    }
    //}
}