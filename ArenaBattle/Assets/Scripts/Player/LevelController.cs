using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private SpawnPoint[] _spawnPoints;
    

    private void Awake()
    {
        _spawnPoints = FindObjectsOfType<SpawnPoint>();
    }
//spawns players into arena
    public void SpawnPlayers()
    {
        var availableSpawns = _spawnPoints.ToList();
        foreach (var player in GameManager.Instance.Players)
        {
            if (player.HasController && player.character != null)
            {
                var spawn = availableSpawns[0];
                spawn.IsSpawning = true;
                spawn.SpawnPlayer(player);
                availableSpawns.Remove(spawn);
            }
        }
    }
}