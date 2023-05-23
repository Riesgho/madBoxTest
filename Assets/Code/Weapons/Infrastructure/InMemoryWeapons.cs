using System.Collections.Generic;
using System.Linq;
using Code.Game;
using UnityEngine;

public class InMemoryWeapons: IWeaponRepository
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
        weapons[SelectType()];

    private WeaponType SelectType()
    {
        var weaponProbs = _gameConfiguration.GetWeaponProbs().ToList();
        
        var totalProbability = weaponProbs.Sum(wp => wp.StartProb);
        
        var randomValue = Random.Range(0f, totalProbability);
        var cumulativeProbability = 0f;
        foreach (var weaponProb in weaponProbs)
        {
            cumulativeProbability += weaponProb.StartProb;
            if (randomValue < cumulativeProbability)
                return weaponProb.Type;
        }
        return WeaponType.LongSword;
    }

    public IDictionary<WeaponType, IWeapon> GetAll() => 
        weapons;

    public void Update(IWeapon weapon)
    {
        weapons[weapon.Type] = weapon;
    }

    public IWeapon Get(WeaponType weaponType) => 
        weapons[weaponType];
}