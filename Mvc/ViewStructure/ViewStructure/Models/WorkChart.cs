using System.Collections.Generic;

namespace ViewStructure.Models
{
    public class WorkChart
    {
        public WorkChart(string _teachername,List<bool> _chart)
        {
            TeacherName = _teachername;
            Chart = _chart;
        }
        public string TeacherName { get; set; }
        public List<bool> Chart { get; set; }
    }
}
