
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using UdemySignalR.API.Models;

namespace UdemySignalR.API.Hubs
{
    public class MyHub:Hub
    {
        private readonly AppDbContext _context;

        public MyHub(AppDbContext context)
        {
            _context = context;
        }

        private static List<string> Names { get; set; } = new List<string>();
        private static int _counter = 0;
        public static int _teamCounter = 7;

        public async Task SendProduct(Product product)
        {
            Clients.All.SendAsync("ReceiveProduct", product);
        }
        public async Task SendName(string name)
        {
            if(Names.Count>=_teamCounter)
            {
                await Clients.Caller.SendAsync("Error", $"Takım en fazla {_teamCounter}  kişi olabilir.");
            }
            else
            {
                Names.Add(name);
                await Clients.All.SendAsync("ReceiveName", name);
            }

        }
        public async Task GetNames()
        {
            await Clients.All.SendAsync("ReceiveNames",Names);
        }

        public async Task AddToGroup(string teamName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, teamName);
        }
        public async Task RemoveToGroup(string teamName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, teamName);
        }

        public async Task SendNameByGroup(string name,string teamName)
        {
            var team = _context.Teams.Where(x=>x.Name==teamName).FirstOrDefault();
            if(team!=null)
            {
                team.Users.Add(new User { Name = name });
                
            }
            else
            {
                var newTeam = new Team { Name = name };
                newTeam.Users.Add(new User { Name = name });
                _context.Teams.Add(newTeam);
            }
            await _context.SaveChangesAsync();
            await Clients.Group(teamName).SendAsync("ReceiveMessageByGroup", name, team.Id);
        }

        public async Task GetNamesByGroup()
        {
            var teams = _context.Teams.Include(x=>x.Users).Select(x=> new
            {
                teamName= x.Name,
                Users = x.Users.ToList()
            });
            await Clients.All.SendAsync("ReceiveNamesByGroup",teams);
        }

        public async override Task OnConnectedAsync()
        {
            _counter++;
            await Clients.All.SendAsync("ReceiveClientCount", _counter);
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            _counter--;
            await Clients.All.SendAsync("ReceiveClientCount", _counter);
            await base.OnDisconnectedAsync(exception);
        }

    }

    }

