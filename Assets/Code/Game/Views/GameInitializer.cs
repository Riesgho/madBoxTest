using Code.Enemies.Presenters;
using Code.Enemies.Views;
using Code.Game;
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
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private EnemySpawnerConfig spawnerConfig;
    [SerializeField] private WeaponPrefabs weaponPrefabs;
    [SerializeField] private GameConfiguration gameConfiguration;

    private PlayerPresenter _playerPresenter;
    private InMemoryWeapons _inMemoryWeapons;

    // Start is called before the first frame update
    void Start()
    {
        var moved = new Subject<Vector3>();
        var attacked = new Subject<Unit>();
        var stoppedAttack = new Subject<Unit>();
        var applyDamage = new Subject<Unit>();
        
        var inMemoryPlayer = new InMemoryPlayer(startSpeed);
        
        var joystickPresenter = new JoystickPresenter(joystickView, moved);
        _inMemoryWeapons = new InMemoryWeapons(weaponPrefabs, gameConfiguration);
        var selectedWeapon = _inMemoryWeapons.SelectRandom();
        _playerPresenter = new PlayerPresenter(playerView, inMemoryPlayer, selectedWeapon, applyDamage);
        var playerAttackInputPresenter = new PlayerAttackInputPresenter(playerAttackInput, selectedWeapon, attacked,stoppedAttack);
        var enemySpawnPresenter = new EnemySpawnPresenter(spawner, spawnerConfig, gameConfiguration);
        
        joystickPresenter.Initialize();
        _playerPresenter.Initialize();
        playerAttackInputPresenter.Initialize();
        enemySpawnPresenter.Initialize();
        enemySpawnPresenter.SpawnAll();

        moved.Subscribe(_playerPresenter.Move);
        attacked.Subscribe(_ =>_playerPresenter.Attack());
        stoppedAttack.Subscribe(_ =>_playerPresenter.StopAttack());
        applyDamage.Subscribe(_ => Debug.Log("AppliedDamage"));
    }

    public void ReRollWeapon()
    {
        _playerPresenter.ChangeWeapon(_inMemoryWeapons.SelectRandom());
    }
}