public class LongSword : IWeapon
{
    public float SpeedModifier => 2;
    public WeaponType Type => WeaponType.LongSword;
    public float AttackRange => 2.5f;
    public float AttackSpeedModifier => 1;
    public float ApplyDamageSpeed => 0.5f;
}