using Metric.API.OpenTelemetrys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Metric.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        [HttpGet]
        public IActionResult CounterMetric()
        {
            OpenTelemetryMetric.CounterMeter.Add(1,new KeyValuePair<string, object?>("event","add")); // counter sayesinde bir değeri arttırabiliriz
            return Ok();
        }

        [HttpGet]
        public IActionResult CounterMetricUpDown()
        {
            OpenTelemetryMetric.CurrentStockCounter.Add(new Random().Next(-10,100)); // counter sayesinde bir değeri arttırabiliriz
            return Ok();
        }

        [HttpGet]
        public IActionResult ObservableCounterMetric()
        {
            //OpenTelemetryMetric.ObservableCounter.(1,new KeyValuePair<string, object?>("event","add")); // observable counter sayesinde bir değeri arttırabiliriz
            Counter.OrderCancelledCounter += new Random().Next(1, 100);
            return Ok();
        }

        [HttpGet]
        public IActionResult ObservableUpDownCounterMetric()
        {
            //OpenTelemetryMetric.ObservableUpDownCounter.Add(1,new KeyValuePair<string, object?>("event","add")); // observable up down counter sayesinde bir değeri arttırabiliriz ve azaltabiliriz
            Counter.UpDownCounterInt += new Random().Next(-100, 100);
            return Ok();
        }

        [HttpGet]
        public IActionResult ObservableGaugeMetric()
        {
            // observable up down counter sayesinde bir değeri arttırabiliriz ve azaltabiliriz farkı ise değerlerin sürekli olarak değişmesi
            Counter.GaugeInt += new Random().Next(-100, 100);
            return Ok();
        }

        [HttpGet]
        public IActionResult HistogramMetric()
        {
            // histogram sayesinde bir değeri arttırabiliriz ve azaltabiliriz veriyi gelen veriye göre gruplayabiliriz
            OpenTelemetryMetric.Histogram.Record(new Random().Next(500, 50000)); // histogram sayesinde bir değeri arttırabiliriz ve azaltabiliriz farkı ise değerlerin sürekli olarak değişmesi
            return Ok();
        }

    }
}
