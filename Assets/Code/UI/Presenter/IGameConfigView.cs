using System.Collections.Generic;
using UniRx;

namespace Code.UI.Presenter
{
    public interface IGameConfigView
    {
        void CreateBoxesFor(IDictionary<WeaponType, IWeapon> weapons);
        void Initialize(ISubject<IWeapon> applyChanges);
    }
}