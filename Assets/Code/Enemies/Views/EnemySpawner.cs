using Code.Enemies.Presenters;
using UnityEngine;

public class EnemySpawner: MonoBehaviour, IEnemySpawnView
{
    [SerializeField] private GameObject enemyPrefab;
    public void Initialize()
    {
        //Do Init Stuff
    }

    public void SpawnAll(int amount, Vector2 area)
    {
        for (int i = 0; i < amount; i++)
        {
            var position = GetRandomPointWithinSquare(area);
            Instantiate(enemyPrefab, new Vector3(position.x, 0, position.y), Quaternion.identity);
        }
    }
    
    private Vector2 GetRandomPointWithinSquare(Vector2 squareSize)
    {
        var halfWidth = squareSize.x / 2f;
        var halfHeight = squareSize.y / 2f;

        var randomX = Random.Range(-halfWidth, halfWidth);
        var randomY = Random.Range(-halfHeight, halfHeight);

        var randomPoint = new Vector2(randomX, randomY);
        return randomPoint;
    }
}