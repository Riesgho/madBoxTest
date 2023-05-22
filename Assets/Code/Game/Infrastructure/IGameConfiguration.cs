using System.Collections.Generic;

namespace Code.Game
{
    public interface IGameConfiguration
    {
        IEnumerable<IWeaponProb> GetWeaponProbs();
        int AmountOfMobs { get; }
    }
}