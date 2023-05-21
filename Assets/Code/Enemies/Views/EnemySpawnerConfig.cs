using Code.Enemies.Presenters;
using UnityEngine;

namespace Code.Enemies.Views
{
    [CreateAssetMenu(menuName = "Enemies/Spawner", fileName = "Spawner")]
    public class EnemySpawnerConfig: ScriptableObject, IEnemySpawnConfig
    {
        [SerializeField] private int amount;
        [SerializeField] private Vector2 area;
        public int Amount => amount;
        public Vector2 Area => area;
    }
}