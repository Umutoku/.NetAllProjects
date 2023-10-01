using System.Diagnostics;

namespace Hangfire.Web.BackgroundJobs
{
    public class ContinuationsJobs
    {
        public static void WriteWatermarkStatusJob(string id, string filename)
        {
            Hangfire.BackgroundJob
                .ContinueJobWith(id, () => Debug.WriteLine($"{filename} : resime watermark eklenmiştir."));
        }
    }
}
