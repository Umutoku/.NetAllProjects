using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionAndInterface
{
    public class VendorBusiness
    {
        public DataSource DataSource { get; set; }

        
    }

    public class Recorder
    {
        public void RecordData(DataSource datasoruce)
        {
            datasoruce.SaveData("savedata");
        }
    }
}
