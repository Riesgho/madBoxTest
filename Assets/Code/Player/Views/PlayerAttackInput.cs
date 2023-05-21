using Code.Player.Presenter;
using UniRx;
using UnityEngine;

namespace Code.Player.Views
{
    public class PlayerAttackInput: MonoBehaviour, IPlayerAttackInput
    {
        private float _distance;
        private ISubject<Unit> _attack;
        private ISubject<Unit> _stopAttack;
        [SerializeField] Transform startPosition;

        public void Initialize(float distance, ISubject<Unit> attack, ISubject<Unit> stopAttack)
        {
            _distance = distance;
            _attack = attack;
            _stopAttack = stopAttack;
        }
        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(startPosition.position, transform.forward, out hit, _distance))
            {
                _attack.OnNext(Unit.Default);
            }
            else
            {
                _stopAttack.OnNext(Unit.Default);
#if UNITY_EDITOR
                Debug.DrawRay(startPosition.position, transform.forward * _distance, Color.red);
#endif
            }
        }
        
    }
}