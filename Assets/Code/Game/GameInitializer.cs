using System.Collections;
using System.Collections.Generic;
using Code.Enemies.Presenters;
using Code.Enemies.Views;
using Code.Player.Presenter;
using Code.Player.Views;
using Code.UI.Presenter;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private JoystickView joystickView;
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerAttackInput playerAttackInput;
    [SerializeField] private float startSpeed = 5;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private EnemySpawnerConfig spawnerConfig;
    // Start is called before the first frame update
    void Start()
    {
        var moved = new Subject<Vector3>();
        var attacked = new Subject<Unit>();
        var stoppedAttack = new Subject<Unit>();
        var applyDamage = new Subject<Unit>();
        
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
        var playerPresenter = new PlayerPresenter(playerView, inMemoryPlayer, selectedWeapon, applyDamage);
        var playerAttackInputPresenter = new PlayerAttackInputPresenter(playerAttackInput, selectedWeapon, attacked,stoppedAttack);
        var enemySpawnPresenter = new EnemySpawnPresenter(spawner, spawnerConfig);
        
        joystickPresenter.Initialize();
        playerPresenter.Initialize();
        playerAttackInputPresenter.Initialize();
        enemySpawnPresenter.Initialize();
        enemySpawnPresenter.SpawnAll();

        moved.Subscribe(playerPresenter.Move);
        attacked.Subscribe(_ =>playerPresenter.Attack());
        stoppedAttack.Subscribe(_ =>playerPresenter.StopAttack());
        applyDamage.Subscribe(_ => Debug.Log("AppliedDamage"));
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
    public float AttackSpeedModifier => 1;
    public float ApplyDamageSpeed => 0.5f;
}

public class CurvedSword : IWeapon
{
    public float SpeedModifier => 4;
    public WeaponType Type => WeaponType.CurvedSword;
    public float AttackRange => 2;
    public float AttackSpeedModifier => 1.5f;
    public float ApplyDamageSpeed => 0.25f;
}

public class GreatSword : IWeapon
{
    public float SpeedModifier => 1;
    public WeaponType Type => WeaponType.GreatSword;
    public float AttackRange => 4;
    public float AttackSpeedModifier => 0.5f;
    public float ApplyDamageSpeed => 0.75f;
}

