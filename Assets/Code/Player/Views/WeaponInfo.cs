using UnityEngine;

namespace Code.Player.Views
{
    [CreateAssetMenu(menuName = "Player/Weapons/WeaponInfo")]
    public class WeaponInfo: ScriptableObject, IWeapon
    {
        [SerializeField] private float speedModifier;
        [SerializeField] private WeaponType type;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackSpeedModifier;
        [SerializeField] private float applyDamageSpeed;
        
        public float SpeedModifier => speedModifier;
        public WeaponType Type => type;
        public float AttackRange => attackRange;
        public float AttackSpeedModifier => attackSpeedModifier;
        public float ApplyDamageSpeed => applyDamageSpeed;
    }
}