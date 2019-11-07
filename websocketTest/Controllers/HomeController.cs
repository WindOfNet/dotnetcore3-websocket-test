using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace websocketTest.Controllers
{
    public class HomeController : Controller
    {
        [Route("/ws")]
        public async Task Index()
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                HttpContext.Response.StatusCode = 400;
                return;
            }

            var ws = await HttpContext.WebSockets.AcceptWebSocketAsync();

            while (!ws.CloseStatus.HasValue)
            {
                await ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))), System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);
                await Task.Delay(1000);
            }
        }
    }
}
