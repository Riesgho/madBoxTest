namespace Code.Player.Presenter
{
    public class PlayerAttackInputPresenter
    {
        private readonly IPlayerAttackInput _view;
        private readonly IWeapon _weapon;

        public  PlayerAttackInputPresenter(IPlayerAttackInput view, IWeapon weapon)
        {
            _view = view;
            _weapon = weapon;
        }

        public void Initialize()
        {
            _view.Initialize(_weapon.AttackRange);
        }
    }

    public interface IPlayerAttackInput
    {
        void Initialize(float weaponAttackRange);
    }
}