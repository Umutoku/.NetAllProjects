using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWebApp.API.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger= logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetContentAsync(CancellationToken token)
        {
            try
            {
                //Async olmayan veride cancallation token
                Enumerable.Range(1, 10).ToList().ForEach(x =>
                {
                    Thread.Sleep(1000);
                    token.ThrowIfCancellationRequested();
                }

                );
                _logger.LogInformation("İstek bitti");
                return Ok("işler bitti");

                /*
                _logger.LogInformation("istek başladı");
                await Task.Delay(5000, token);
                var mytext = new HttpClient().GetStringAsync("https://www.google.com");

                var data = await mytext;
                _logger.LogInformation("İstek bitti");
                return Ok(data);
                */
            }
            catch (Exception ex)
            {

                _logger.LogInformation("istek iptal edildi "+ex.Message);
                return BadRequest();
            }
            
        }
    }
}
