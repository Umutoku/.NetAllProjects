using CovidChart.API.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CovidChart.API.Models
{
    public class CovidService
    {
        private readonly IHubContext<CovidHubs> _hubContext;
        private readonly AppDbContext _appDbContext;

        public CovidService(IHubContext<CovidHubs> hubContext, AppDbContext appDbContext)
        {
            _hubContext = hubContext;
            _appDbContext = appDbContext;
        }

        public IQueryable<Covid> GetList()
        {
            return _appDbContext.Covids.AsQueryable();
        }
        public async Task SaveCovid(Covid covid)
        {
            await _appDbContext.Covids.AddAsync(covid);
            await _appDbContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceivedCovidList",GetCovidList());
        }

        public List<CovidChart> GetCovidList()
        {
            List<CovidChart> covidCharts = new List<CovidChart>();
            using(var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "Select tarih, [1],[2],[3],[4],[5] FROM\r\n(select [City],[Count],Cast([CovidDate] as date) as tarih from Covids) as CovidT\r\nPIVOT\r\n(SUM(Count) For City IN([1],[2],[3],[4],[5])) as PTable\r\norder by tarih asc";

                command.CommandType = System.Data.CommandType.Text;

                _appDbContext.Database.OpenConnection();

                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CovidChart cc = new CovidChart();
                        cc.CovidDate = reader.GetDateTime(0).ToShortDateString();
                        Enumerable.Range(1, 5).ToList().ForEach(Range =>
                        {
                            if (System.DBNull.Value.Equals(reader[Range]))
                            {
                                cc.Counts.Add(0);
                            }
                            else
                            {
                                cc.Counts.Add(reader.GetInt32(Range));
                            }
                        });

                        covidCharts.Add(cc);
                    }
                }

                _appDbContext.Database.CloseConnection();
                return covidCharts;
            }
        }
    }
}
