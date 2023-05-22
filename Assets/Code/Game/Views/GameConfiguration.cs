using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Game
{
    [CreateAssetMenu(menuName = "Game/Configuration", fileName = "Configuration")]
    public class GameConfiguration: ScriptableObject, IGameConfiguration
    {
        [SerializeField] private int amountOfMobs;
        [SerializeField] private List<WeaponProb> weaponProbs;
        public IEnumerable<IWeaponProb> GetWeaponProbs() => 
            weaponProbs;
    }

    [System.Serializable]
    public class WeaponProb: IWeaponProb
    {
        [Range(0, 1)] [SerializeField] private float startProb;
        [SerializeField] private WeaponType type;
        
        public float StartProb => startProb;
        public WeaponType Type => type;
    }
}