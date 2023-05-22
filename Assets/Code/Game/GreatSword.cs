public class GreatSword : IWeapon
{
    public float SpeedModifier => 1;
    public WeaponType Type => WeaponType.GreatSword;
    public float AttackRange => 4;
    public float AttackSpeedModifier => 0.5f;
    public float ApplyDamageSpeed => 0.75f;
}