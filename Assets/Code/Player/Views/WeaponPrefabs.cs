using System.Collections.Generic;
using System.Linq;
using Code.Player.Views;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Weapons/Weapon Config", fileName = "Weapon Config")]
public class WeaponPrefabs: ScriptableObject, IWeaponInformation
{
    [SerializeField] private List<WeaponPrefab> weaponConfig;

    public GameObject GetPrefabFor(WeaponType weaponType) => 
        weaponConfig.First(weapon => weapon.WeaponType == weaponType).Prefab;

    public IWeapon GetInfoFor(WeaponType weaponType) => 
        weaponConfig.First(weapon => weapon.WeaponType == weaponType).WeaponInfo;

    public IEnumerable<IWeapon> GetAll()
    {
        return weaponConfig.Select(weaponConfig => weaponConfig.WeaponInfo);
    }
}

public interface IWeaponInformation
{
    public IWeapon GetInfoFor(WeaponType weaponType);
    IEnumerable<IWeapon> GetAll();
}