using CovidChart.API.Models;
using Microsoft.AspNetCore.SignalR;

namespace CovidChart.API.Hubs
{
    public class CovidHubs:Hub
    {
        private readonly CovidService _covidService;

        public CovidHubs(CovidService covidService)
        {
            _covidService = covidService;
        }

        public async Task GetCovidList()
        {
            await Clients.All.SendAsync("ReceivedCovidList", _covidService.GetCovidList());
        }
    }
}
