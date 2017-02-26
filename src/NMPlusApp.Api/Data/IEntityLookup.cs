using NMPlusApp.Api.Models;
using System.Collections.Generic;

namespace NMPlusApp.Api.Data
{
    public interface IEntityLookup
    {
        IEnumerable<Pokemon> Pokemons { get; }
        IEnumerable<Player> Players { get; }
    }
}
