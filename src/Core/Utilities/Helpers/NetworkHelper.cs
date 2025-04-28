using System.Net;
using System.Net.Sockets;

namespace Core.Utilities.Helpers;

public class NetworkHelper
{
    public static string? GetLocalIpAddress()
    {
        try
        {
            var hostName = Dns.GetHostName();
            var hostEntry = Dns.GetHostEntry(hostName);
            var ipv4Addresses = hostEntry.AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork);

            return ipv4Addresses != null ? ipv4Addresses.ToString() : "127.0.0.1";
        }
        catch (Exception ex)
        {
            return "127.0.0.1";
        }
    }
}