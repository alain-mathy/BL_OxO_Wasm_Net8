using BL_OxO_Wasm_Net8.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace BL_OxO_Wasm_Net8.Hubs
{
    public class GameHub : Hub
    {
        private static readonly List<GameRoom> _rooms = new();
        public override  async Task OnConnectedAsync()
        {
            Console.WriteLine($"Player with id '{Context.ConnectionId}' connected");

            await Clients.Caller.SendAsync("Rooms", _rooms.OrderBy(r => r.RoomName));
        }

        public async Task<GameRoom> CreateRoom(string name, string playerName)
        {
            var roomId = Guid.NewGuid().ToString();
            var room = new GameRoom(roomId, name);
            _rooms.Add(room);
            await Clients.All.SendAsync("Rooms", _rooms.OrderBy(r => r.RoomName));

            return room;
        }
    }
}
