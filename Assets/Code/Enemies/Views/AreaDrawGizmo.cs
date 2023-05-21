using System;
using UnityEngine;

namespace Code.Enemies.Views
{
    public class AreaDrawGizmo: MonoBehaviour
    {
        [SerializeField] private EnemySpawnerConfig _config;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(_config.Area.x, 0,_config.Area.y));
        }
    }
}