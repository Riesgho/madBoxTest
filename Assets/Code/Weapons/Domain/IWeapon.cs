public interface IWeapon
{
    float SpeedModifier { get; }
    WeaponType Type { get; }
    float AttackRange { get; }
    float AttackSpeedModifier { get; }
    float ApplyDamageSpeed { get; }
}