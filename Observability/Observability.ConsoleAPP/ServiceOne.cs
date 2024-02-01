using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observability.ConsoleAPP
{
    internal class ServiceOne
    {
        static HttpClient httpClient = new HttpClient();
        internal async Task<int> MakeRequestToGoogle()
        {
            using var activity = ActivitySourceProvider.source.StartActivity();


            try
            {
                var tags = new ActivityTagsCollection
                {
                    { "userID", "1234" }
                };

                activity?.AddEvent(new ActivityEvent("Google request started", tags: tags));
                activity?.AddTag("requestLength", "100");
                activity?.AddTag("requestType", "GET");
                var request = new HttpRequestMessage(HttpMethod.Get, "https://www.google.com");
                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                tags.Add("responseLength", responseContent.Length.ToString());
                activity?.AddEvent(new ActivityEvent("Google request completed", tags: tags));

                var serviceTwo = new ServiceTwo();
                var fileLength = await serviceTwo.WriteToFile("Hello World!");

                return responseContent.Length;
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error,ex.Message);
                //throw;
                return -1;
            }

            
        }
    }
}
