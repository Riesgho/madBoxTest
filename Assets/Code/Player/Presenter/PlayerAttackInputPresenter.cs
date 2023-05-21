using UniRx;
using UnityEngine.UI;

namespace Code.Player.Presenter
{
    public class PlayerAttackInputPresenter
    {
        private readonly IPlayerAttackInput _view;
        private readonly IWeapon _weapon;
        private readonly ISubject<Unit> _attacked;
        private readonly ISubject<Unit> _stoppedAttack;

        public  PlayerAttackInputPresenter(IPlayerAttackInput view, IWeapon weapon, ISubject<Unit> attacked, ISubject<Unit> stoppedAttack)
        {
            _view = view;
            _weapon = weapon;
            _attacked = attacked;
            _stoppedAttack = stoppedAttack;
        }

        public void Initialize()
        {
            _view.Initialize(_weapon.AttackRange, _attacked,_stoppedAttack);
        }
    }

    public interface IPlayerAttackInput
    {
        void Initialize(float weaponAttackRange, ISubject<Unit> attacked, ISubject<Unit> stopAttack);
    }
}