using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ViewStructure.Models;

namespace ViewStructure.ViewComponents
{
    public class WorkChartViewComponent:ViewComponent
    {
        List<WorkChart> workChart = new List<WorkChart>()
        {
            new WorkChart("Talha",new List<bool> { true,false, false,true,false }),
            new WorkChart("Umut",new List<bool> { false,true, true,true,false }),
            new WorkChart("Turna",new List<bool> { true,false, true,true,false }),
            new WorkChart("Pelin",new List<bool> { false,true, false,true,true }),
        };

        public IViewComponentResult Invoke()
        {
            return View(workChart);
        }
    }
}
