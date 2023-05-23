using System;
using System.Collections.Generic;

public interface IWeaponRepository
{
    IDictionary<WeaponType,IWeapon> GetAll();
    void Update(IWeapon weapon);
}