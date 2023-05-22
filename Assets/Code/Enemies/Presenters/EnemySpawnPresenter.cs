using Code.Game;
using UnityEngine;

namespace Code.Enemies.Presenters
{
    public class EnemySpawnPresenter
    {
        private readonly IEnemySpawnView _view;
        private readonly IEnemySpawnConfig _enemySpawnConfig;
        private readonly IGameConfiguration _gameConfiguration;

        public EnemySpawnPresenter(IEnemySpawnView view, IEnemySpawnConfig enemySpawnConfig, IGameConfiguration gameConfiguration)
        {
            _view = view;
            _enemySpawnConfig = enemySpawnConfig;
            _gameConfiguration = gameConfiguration;
        }

        public void Initialize()
        {
            _view.Initialize();
        }

        public void SpawnAll()
        {
            _view.SpawnAll(_gameConfiguration.AmountOfMobs, _enemySpawnConfig.Area);
        }
    }

    public interface IEnemySpawnConfig
    {
        Vector2 Area { get; }
    }

    public interface IEnemySpawnView
    {
        void Initialize();
        void SpawnAll(int amount, Vector2 area);
    }
}