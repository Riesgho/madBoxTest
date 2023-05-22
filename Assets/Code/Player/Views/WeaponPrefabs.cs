using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Weapons/Weapon Config", fileName = "Weapon Config")]
public class WeaponPrefabs: ScriptableObject
{
    [SerializeField] private List<WeaponPrefab> weaponConfig;

    public GameObject GetPrefabFor(WeaponType weaponType) => 
        weaponConfig.First(weapon => weapon.WeaponType == weaponType).Prefab;
}