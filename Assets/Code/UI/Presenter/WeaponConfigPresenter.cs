namespace Code.UI.Presenter
{
    public class WeaponConfigPresenter
    {
        private readonly IWeaponConfigView _view;
        private readonly IWeapon _weaponValue;

        public WeaponConfigPresenter(IWeaponConfigView view, IWeapon weaponValue)
        {
            _view = view;
            _weaponValue = weaponValue;
        }

        public void Initialize()
        {
            _view.Initialize(_weaponValue);
        }

        public IWeapon GetWeaponChanges()
        {
            return _view.GetNewValues();
        }
    }
}