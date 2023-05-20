using UniRx;
using UnityEngine;

namespace Code.UI.Presenter
{
    public class JoystickPresenter
    {
        private readonly IJoystickView _view;
        private readonly ISubject<Vector3> _moved;

        public JoystickPresenter(IJoystickView view, ISubject<Vector3> moved)
        {
            _view = view;
            _moved = moved;
        }

        public void Initialize()
        {
            _view.Initialize(_moved);
        }
    }

    public interface IJoystickView
    {
        void Initialize(ISubject<Vector3> moved);
    }
}