using Code.Player.Presenter;
using UnityEngine;

namespace Code.Player.Views
{
    public class PlayerAttackInput: MonoBehaviour, IPlayerAttackInput
    {
        private float _distance;
        [SerializeField] Transform startPosition;

        public void Initialize(float distance)
        {
            _distance = distance;
        }
        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(startPosition.position, transform.forward, out hit, _distance))
            {
                Debug.DrawRay(startPosition.position, transform.forward * hit.distance, Color.green);
            }
            else
            {
                Debug.DrawRay(startPosition.position, transform.forward * _distance, Color.red);
            }
        }
        
        private void Attack()
        {
        }
    }
}