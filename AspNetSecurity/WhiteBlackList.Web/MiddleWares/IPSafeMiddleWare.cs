using Microsoft.Extensions.Options;
using System.Net;

namespace WhiteBlackList.Web.MiddleWares
{
    public class IPSafeMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<IPSafeMiddleWare> _logger;
        private readonly IPList _ipList;

        public IPSafeMiddleWare(RequestDelegate next, ILogger<IPSafeMiddleWare> logger, IOptions<IPList> ipList)
        {
            _next = next;
            _logger = logger;
            _ipList = ipList.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var remoteIp = context.Connection.RemoteIpAddress;

            var isWhiteList = _ipList.WhiteList.Where(x=> IPAddress.Parse(x).Equals(remoteIp)).Any(); // Check if the IP is in the white list
            
            if (!isWhiteList)
            {
                _logger.LogWarning("Forbidden Request from IP: {RemoteIp}", remoteIp);
                context.Response.StatusCode = 403;
                return;
            }
            await _next(context);
        }
    }
}
