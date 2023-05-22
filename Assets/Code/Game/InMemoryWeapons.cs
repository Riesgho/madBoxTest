using System.Collections.Generic;
using UnityEngine;

public class InMemoryWeapons
{
    IDictionary<WeaponType, IWeapon> weapons;

    public InMemoryWeapons(IWeaponInformation weaponInfo)
    {
        weapons = new Dictionary<WeaponType, IWeapon>();
        foreach (var weapon in weaponInfo.GetAll())
        {
            weapons.Add(weapon.Type, weapon);
        }
    }
    
    public IWeapon SelectRandom() => 
        weapons[(WeaponType)Random.Range(0, weapons.Count)];
}