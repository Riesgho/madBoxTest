public class WeaponDto : IWeapon
{
    public WeaponDto(float speed, WeaponType type, float attackSpeed, float attackRange, float applyDamageSpeed)
    {
        SpeedModifier = speed;
        Type = type;
        AttackSpeedModifier = attackSpeed;
        AttackRange = attackRange;
        ApplyDamageSpeed = applyDamageSpeed;
    }

    public float SpeedModifier { get; }
    public WeaponType Type { get; }
    public float AttackRange { get; }
    public float AttackSpeedModifier { get; }
    public float ApplyDamageSpeed { get; }
}