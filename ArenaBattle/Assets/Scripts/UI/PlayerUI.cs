using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private TextMeshProUGUI _initText;

    private void Awake()
    {
        _initText = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void HandleInitPlayer()
    {
        _initText.text = "Player Joined";
        StartCoroutine(ClearTextAfterDelay());
    }

    public void DisableInitText()
    {
        if (_initText != null) _initText.gameObject.SetActive(false);
    }

    private IEnumerator ClearTextAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        _initText.text = "";
    }
}
