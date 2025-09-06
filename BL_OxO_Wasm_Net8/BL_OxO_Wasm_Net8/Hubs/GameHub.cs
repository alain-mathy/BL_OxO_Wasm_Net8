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

            var newPlayer = new Player(Context.ConnectionId, playerName);
            room.AddPlayer(newPlayer);

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.All.SendAsync("Rooms", _rooms.OrderBy(r => r.RoomName));

            return room;
        }

        public async Task<GameRoom?> JoinRoom(string roomId, string playerName)
        {
            var room = _rooms.FirstOrDefault(r => r.RoomId == roomId);
            if (room is not null)
            {
                var newPlayer = new Player(Context.ConnectionId, playerName);
                if (room.AddPlayer(newPlayer))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                    await Clients.Group(roomId).SendAsync("PlayerJoined", newPlayer);
                    await Clients.All.SendAsync("Rooms", _rooms.OrderBy(r => r.RoomName));
                    await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                    return room;
                }
            }

            return null;
        }
    }
}
