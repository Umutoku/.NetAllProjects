using System.Diagnostics.Metrics;

namespace Metric.API.OpenTelemetrys
{
    public static class OpenTelemetryMetric
    {
        private static readonly Meter meter = new Meter("Metric.API"); // sayesinde metric'leri gruplayabiliriz
        public static readonly Counter<int> CounterMeter = meter.CreateCounter<int>("Metric.API.Counter"); // counter sayesinde bir değeri arttırabiliriz

        public static ObservableCounter<int> ObservableCounter = meter.CreateObservableCounter("Metric.API.ObservableCounter",()=> new Measurement<int>(Counter.OrderCancelledCounter)); // observable counter sayesinde bir değeri arttırabiliriz

        public static ObservableUpDownCounter<int> ObservableUpDownCounter = meter.CreateObservableUpDownCounter("Metric.API.ObservableUpDownCounter", () => new Measurement<int>(Counter.UpDownCounterInt)); // observable up down counter sayesinde bir değeri arttırabiliriz ve azaltabiliriz

        public static UpDownCounter<int> CurrentStockCounter = meter.CreateUpDownCounter<int>("current.stock.count");

        public static ObservableGauge<int> ObservableGauge = meter.CreateObservableGauge("Metric.API.ObservableGauge", () => new Measurement<int>(Counter.GaugeInt)); // observable up down counter sayesinde bir değeri arttırabiliriz ve azaltabiliriz. Farkı ise değerlerin sürekli olarak değişmesi

        public static Histogram<int> Histogram = meter.CreateHistogram<int>("Metric.API.Histogram",unit:"milliseconds"); // histogram sayesinde bir değeri arttırabiliriz ve azaltabiliriz. Farkı ise değerlerin sürekli olarak değişmesi

    }
}
