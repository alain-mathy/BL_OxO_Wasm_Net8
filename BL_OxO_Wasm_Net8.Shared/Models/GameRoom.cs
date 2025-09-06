using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_OxO_Wasm_Net8.Shared.Models
{
    public class GameRoom
    {
        public string RoomId { get; set; }
        public string RoomName { get; set; }
        public List<Player> Players { get; set; } = new();
        public OxOGame Game { get; set; } = new OxOGame();
        public bool AddPlayer(Player player)
        {
            if (Players.Count < 2 && !Players.Any(p => p.ConnectionId == player.ConnectionId))
            {
                Players.Add(player);
                if (Players.Count == 1)
                {
                    Game.PlayerXId = player.ConnectionId;
                }
                else if (Players.Count == 2)
                {
                    Game.PlayerOId = player.ConnectionId;
                }

                return true;
            }

            return false;
        }

        public GameRoom(string roomId, string roomName)
        {
            RoomId = roomId;
            RoomName = roomName;
        }
    }
}
