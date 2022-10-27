using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool IsSpawning = false;

    public void SpawnPlayer(Player player)
    {
        player.SpawnCharacter(transform.position);
        IsSpawning = false;
    }
}