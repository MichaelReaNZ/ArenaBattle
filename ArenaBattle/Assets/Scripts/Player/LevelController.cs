using UnityEngine;

public class LevelController : MonoBehaviour
{
    private SpawnPoint[] _spawnPoints;
    

    private void Awake()
    {
        _spawnPoints = FindObjectsOfType<SpawnPoint>();
    }

    public void SpawnPlayers()
    {
        foreach (var player in PlayerManager.Instance.Players)
        {
            foreach (var spawnPoint in _spawnPoints)
            {
                if (!spawnPoint.IsSpawning)
                {
                    if (player.HasController && player.Character != null)
                    {
                        spawnPoint.IsSpawning = true;
                        spawnPoint.SpawnPlayer(player);
                    }
                }
            }
        }
    }
}