using System.Linq;
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
        var availableSpawns = _spawnPoints.ToList();
        foreach (var player in PlayerManager.Instance.Players)
        {
            if (player.HasController && player.Character != null)
            {
                var spawn = availableSpawns[0];
                spawn.IsSpawning = true;
                spawn.SpawnPlayer(player);
                availableSpawns.Remove(spawn);
            }
        }
    }
}