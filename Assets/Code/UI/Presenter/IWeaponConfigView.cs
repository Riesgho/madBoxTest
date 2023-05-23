namespace Code.UI.Presenter
{
    public interface IWeaponConfigView
    {
        void Initialize(IWeapon weaponValue);
        IWeapon GetNewValues();
    }
}