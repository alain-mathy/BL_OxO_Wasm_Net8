using Microsoft.AspNetCore.SignalR;

namespace BL_OxO_Wasm_Net8.Hubs
{
    public class GameHub : Hub
    {
        public override  Task OnConnectedAsync()
        {
            Console.WriteLine($"Player with id '{Context.ConnectionId}' connected");
            return  base.OnConnectedAsync();
        }
    }
}
