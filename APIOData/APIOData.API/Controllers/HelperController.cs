using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace OData.API.Controllers
{

    public class HelperController : ODataController
    {
        [ODataRouteComponent("GetKDV")]
        public IActionResult GetKDV()
        {
            return Ok(18);
        }
    }
}
