using System.Collections.Generic;

public interface IWeaponInformation
{
    public IWeapon GetInfoFor(WeaponType weaponType);
    IEnumerable<IWeapon> GetAll();
}