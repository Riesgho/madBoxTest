using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Weapons", fileName = "Weapon Config")]
public class WeaponPrefabs: ScriptableObject
{
    [SerializeField] private List<WeaponPrefab> _weaponConfig;

    public GameObject GetPrefabFor(WeaponType weaponType) => 
        _weaponConfig.First(weapon => weapon.WeaponType == weaponType).Prefab;
}