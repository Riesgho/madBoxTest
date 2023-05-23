using UniRx;

namespace Code.UI.Presenter
{
    public class InGameConfigPresenter
    {
        private readonly IGameConfigView _view;
        private readonly IWeaponRepository _inMemoryWeapons;
        private readonly ISubject<Unit> _weaponsUpdated;
        private readonly CompositeDisposable _diposables;

        public InGameConfigPresenter(IGameConfigView view, IWeaponRepository inMemoryWeapons, ISubject<Unit> weaponsUpdated)
        {
            _view = view;
            _inMemoryWeapons = inMemoryWeapons;
            _weaponsUpdated = weaponsUpdated;
            _diposables = new CompositeDisposable();
        }

        public void Initialize()
        {
            var applyChanges = new Subject<IWeapon>();
            _view.CreateBoxesFor(_inMemoryWeapons.GetAll());
            _view.Initialize(applyChanges);
            applyChanges.Subscribe(weapon =>
            {
                _weaponsUpdated.OnNext(Unit.Default);
                _inMemoryWeapons.Update(weapon);
            }).AddTo(_diposables);
        }
    }
}