using System.Collections;
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

    [SerializeField] private WeaponPrefabs weaponPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        var moved = new Subject<Vector3>();
        var attacked = new Subject<Unit>();
        var stoppedAttack = new Subject<Unit>();
        var applyDamage = new Subject<Unit>();
        
        var inMemoryPlayer = new InMemoryPlayer(startSpeed);
        
        var joystickPresenter = new JoystickPresenter(joystickView, moved);
        var inMemoryWeapons = new InMemoryWeapons(weaponPrefabs);
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