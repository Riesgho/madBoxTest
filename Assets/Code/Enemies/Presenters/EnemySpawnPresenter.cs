using UnityEngine;

namespace Code.Enemies.Presenters
{
    public class EnemySpawnPresenter
    {
        private readonly IEnemySpawnView _view;
        private readonly IEnemySpawnConfig _config;

        public EnemySpawnPresenter(IEnemySpawnView view, IEnemySpawnConfig config)
        {
            _view = view;
            _config = config;
        }

        public void Initialize()
        {
            _view.Initialize();
        }

        public void SpawnAll()
        {
            _view.SpawnAll(_config.Amount, _config.Area);
        }
    }

    public interface IEnemySpawnConfig
    {
        int Amount { get; }
        Vector2 Area { get; }
    }

    public interface IEnemySpawnView
    {
        void Initialize();
        void SpawnAll(int amount, Vector2 area);
    }
}