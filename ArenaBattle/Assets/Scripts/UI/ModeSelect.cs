using UnityEngine;

public class ModeSelect : MonoBehaviour
{
    
    [SerializeField] private GameModeEnum mode;
    [SerializeField] private GameObject marker;
    public GameModeEnum GetGameMode => mode;
    public void SetSelected(bool value)
    {
        marker.SetActive(value);
    }
}