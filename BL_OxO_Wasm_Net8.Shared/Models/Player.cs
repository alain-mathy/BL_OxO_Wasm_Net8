using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_OxO_Wasm_Net8.Shared.Models
{
    public class Player
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public Player(string connectionId, string name)
        {
            ConnectionId = connectionId;
            Name = name;
        }
    }
}
