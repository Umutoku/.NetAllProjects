using Polly;
using Polly.Extensions.Http;
using System.Diagnostics;
using System.Net;

namespace ServiceA.API.Extensions
{
    public static class PollyExtension
    {

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30),onBreak: (args1,args2) =>
                {
                    Debug.WriteLine("Circuit is open");
                }, 
                onReset: () => 
                { 
                    Debug.WriteLine("Circuit is closed");
                }, 
                onHalfOpen: () =>
                {
                    Debug.WriteLine("Circuit is half open");
                }); // Break the circuit after the specified number of consecutive exceptions
        }

        public static IAsyncPolicy<HttpResponseMessage> GetAdvancedCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                .AdvancedCircuitBreakerAsync(0.5, // Break the circuit if the action fails 50% of the time
                TimeSpan.FromSeconds(30), 5, TimeSpan.FromSeconds(30), onBreak: (args1, args2) =>
                {
                    Debug.WriteLine("Circuit is open");
                },
                onReset: () =>
                {
                    Debug.WriteLine("Circuit is closed");
                },
                onHalfOpen: () =>
                {
                    Debug.WriteLine("Circuit is half open");
                }); // Break the circuit after the specified number of consecutive exceptions
        }

        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
