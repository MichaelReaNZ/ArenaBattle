using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayWinnerAndStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.winningPlayer != null)
            GetComponent<TextMeshProUGUI>().text = "Winner: Player " + GameManager.Instance.winningPlayer.PlayerNumber + 1;
        else
        {
            GetComponent<TextMeshProUGUI>().text = "Winner: Can't find winner.";
        }
    }
    
}
