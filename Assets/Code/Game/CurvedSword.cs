public class CurvedSword : IWeapon
{
    public float SpeedModifier => 4;
    public WeaponType Type => WeaponType.CurvedSword;
    public float AttackRange => 2;
    public float AttackSpeedModifier => 1.5f;
    public float ApplyDamageSpeed => 0.25f;
}