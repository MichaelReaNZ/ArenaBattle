using UnityEngine;

public class LevelController : MonoBehaviour
{
    private SpawnPoint[] _spawnPoints;
    

    private void Awake()
    {
        _spawnPoints = FindObjectsOfType<SpawnPoint>();
    }

    private void Start()
    {
        foreach (var player in PlayerManager.Instance.Players)
        {
            foreach (var spawnPoint in _spawnPoints)
            {
                if (!spawnPoint.IsSpawning)
                {
                    spawnPoint.IsSpawning = true;
                    spawnPoint.SpawnPlayer(player);
                }
            }
        }
    }
}