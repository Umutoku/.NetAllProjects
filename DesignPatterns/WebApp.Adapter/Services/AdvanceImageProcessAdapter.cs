
using System.Drawing;

namespace WebApp.Adapter.Services
{
    public class AdvanceImageProcessAdapter : IImageProcess
    {
        private readonly IAdvanceImageProcess _advanceImageProcess;

        public AdvanceImageProcessAdapter(IAdvanceImageProcess advanceImageProcess)
        {
            _advanceImageProcess = advanceImageProcess;
        }

        public void AddWatermark(string text, string filename, Stream imageStream)
        {
            _advanceImageProcess.AddWatermarkImage(imageStream, text, filename, Color.FromArgb(128, 255, 255, 255), Color.FromArgb(0, 0, 0, 0));
        }
    }
}
