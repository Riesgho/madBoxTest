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
    [SerializeField] private InGameConfigView inGameConfigurationToolView;

    private PlayerPresenter _playerPresenter;
    private InMemoryWeapons _inMemoryWeapons;
    private IWeapon _selectedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        var moved = new Subject<Vector3>();
        var attacked = new Subject<Unit>();
        var stoppedAttack = new Subject<Unit>();
        var applyDamage = new Subject<Unit>();
        var weaponsUpdated = new Subject<Unit>();
        var inMemoryPlayer = new InMemoryPlayer(startSpeed);
        
        var joystickPresenter = new JoystickPresenter(joystickView, moved);
        _inMemoryWeapons = new InMemoryWeapons(weaponPrefabs, gameConfiguration);
        _selectedWeapon = _inMemoryWeapons.SelectRandom();
        _playerPresenter = new PlayerPresenter(playerView, inMemoryPlayer, _selectedWeapon, applyDamage);
        var playerAttackInputPresenter = new PlayerAttackInputPresenter(playerAttackInput, _selectedWeapon, attacked,stoppedAttack);
        var enemySpawnPresenter = new EnemySpawnPresenter(spawner, spawnerConfig, gameConfiguration);
        var inGameDebugMode = new InGameConfigPresenter(inGameConfigurationToolView, _inMemoryWeapons,weaponsUpdated);
        
        joystickPresenter.Initialize();
        _playerPresenter.Initialize();
        playerAttackInputPresenter.Initialize();
        enemySpawnPresenter.Initialize();
        enemySpawnPresenter.SpawnAll();
        inGameDebugMode.Initialize();
        
        moved.Subscribe(_playerPresenter.Move);
        attacked.Subscribe(_ =>_playerPresenter.Attack());
        stoppedAttack.Subscribe(_ =>_playerPresenter.StopAttack());
        applyDamage.Subscribe(_ => Debug.Log("AppliedDamage"));
        weaponsUpdated.Subscribe(_ => _playerPresenter.ChangeWeapon(_inMemoryWeapons.Get(_selectedWeapon.Type)));
    }

    public void ReRollWeapon()
    {
        _selectedWeapon = _inMemoryWeapons.SelectRandom();
        _playerPresenter.ChangeWeapon(_selectedWeapon);
    }
}