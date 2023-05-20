using System.Collections;
using System.Collections.Generic;
using Code.UI.Presenter;
using UniRx;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private JoystickView joystickView;
    [SerializeField] private PlayerView playerView;

    [SerializeField] private float startSpeed = 12;
    // Start is called before the first frame update
    void Start()
    {
        var moved = new Subject<Vector3>();
        var inMemoryPlayer = new InMemoryPlayer(startSpeed);
        var longSword = new LongSword();
        var joystickPresenter = new JoystickPresenter(joystickView, moved);
        var playerPresenter = new PlayerPresenter(playerView, inMemoryPlayer, longSword);
        
        joystickPresenter.Initialize();
        playerPresenter.Initialize();

        moved.Subscribe(playerPresenter.Move);
    }
}

public class InMemoryPlayer: IPlayerRepository
{
    public InMemoryPlayer(float startSpeed)
    {
        Speed = startSpeed;
    }

    public float Speed { get; }
}

public class LongSword : IWeapon
{
    public float SpeedModifier => 3;
    public WeaponType Type => WeaponType.LongSword;
}
