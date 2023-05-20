using System.Collections;
using System.Collections.Generic;
using Code.Player.Presenter;
using Code.Player.Views;
using Code.UI.Presenter;
using UniRx;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private JoystickView joystickView;
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerAttackInput playerAttackInput;
    [SerializeField] private float startSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        var moved = new Subject<Vector3>();
        var inMemoryPlayer = new InMemoryPlayer(startSpeed);
        
        var joystickPresenter = new JoystickPresenter(joystickView, moved);
        var inMemoryWeapons = new InMemoryWeapons();
        var longSword = new LongSword();
        var greatSwordSword = new GreatSword();
        var curvedSword = new CurvedSword();
        inMemoryWeapons.Add(longSword);
        inMemoryWeapons.Add(greatSwordSword);
        inMemoryWeapons.Add(curvedSword);
        var selectedWeapon = inMemoryWeapons.SelectRandom();
        var playerPresenter = new PlayerPresenter(playerView, inMemoryPlayer, selectedWeapon);
        var playerAttackInputPresenter = new PlayerAttackInputPresenter(playerAttackInput, selectedWeapon);
        
        joystickPresenter.Initialize();
        playerPresenter.Initialize();
        playerAttackInputPresenter.Initialize();

        moved.Subscribe(playerPresenter.Move);
    }
}


public class InMemoryWeapons
{
    IDictionary<WeaponType, IWeapon> weapons;

    public InMemoryWeapons()
    {
        weapons = new Dictionary<WeaponType, IWeapon>();
    }
    public void Add(IWeapon weapon)
    {
        weapons.Add(weapon.Type, weapon);
    }

    public IWeapon SelectRandom() => 
        weapons[(WeaponType)Random.Range(0, weapons.Count)];
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
    public float SpeedModifier => 2;
    public WeaponType Type => WeaponType.LongSword;
    public float AttackRange => 2.5f;
}

public class CurvedSword : IWeapon
{
    public float SpeedModifier => 4;
    public WeaponType Type => WeaponType.CurvedSword;
    public float AttackRange => 2;
}

public class GreatSword : IWeapon
{
    public float SpeedModifier => 1;
    public WeaponType Type => WeaponType.GreatSword;
    public float AttackRange => 4;
}

