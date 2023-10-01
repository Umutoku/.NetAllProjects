using GettingData.Models;
using Microsoft.AspNetCore.Mvc;

namespace GettingData.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Uygulama çalışıyor";
        }
        public string UseQueryString(string name)
        {
            return "Uygulama çalışıyor";
        }

        //home/index/15/telefon?name=abdullah&surname=ola
    }
}
