using System.Collections.Generic;
using System.Linq;
using Code.Game;
using UnityEngine;

public class InMemoryWeapons
{
    private readonly IGameConfiguration _gameConfiguration;
    IDictionary<WeaponType, IWeapon> weapons;

    public InMemoryWeapons(IWeaponInformation weaponInfo, IGameConfiguration gameConfiguration)
    {
        _gameConfiguration = gameConfiguration;
        weapons = new Dictionary<WeaponType, IWeapon>();
        foreach (var weapon in weaponInfo.GetAll())
        {
            weapons.Add(weapon.Type, weapon);
        }
    }
    
    public IWeapon SelectRandom() => 
        weapons[(WeaponType)Random.Range(0, weapons.Count)];

    private WeaponType SelectType()
    {
        var rndValue = Random.Range(0.0f, 1.0f);
        return _gameConfiguration.GetWeaponProbs().First(weapon => weapon.StartProb > rndValue).Type;
    }
}
