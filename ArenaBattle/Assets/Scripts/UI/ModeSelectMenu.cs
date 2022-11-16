using System;
using System.Collections;
using UnityEngine;

public class ModeSelectMenu : MonoBehaviour
{
    public static event Action<GameModeEnum> OnSelectMode;
    [SerializeField] private ModeSelect[] modes;
    private ModeSelect currentMode;
    private bool canGoLeft = true;
    private bool canGoRight = true;
    private void Start()
    {
        currentMode = modes[0];
        currentMode.SetSelected(true);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Shoot1"))
        {
            OnSelectMode?.Invoke(currentMode.GetGameMode);
        }
        else if (Input.GetAxis("Horizontal1") < -0.8f)
        {
            if (canGoLeft)
            {
                StartCoroutine(SelectLeftRoutine());
                canGoLeft = false;
            }
        }
        else if (Input.GetAxis("Horizontal1") > 0.8f)
        {
            if (canGoRight)
            {
                StartCoroutine(SelectRightRoutine());
                canGoRight = false;
            }
        }
    }

    private IEnumerator SelectRightRoutine()
    {
        SetToNext();
        yield return new WaitForSeconds(0.5f);
        canGoRight = true;
    }

    private IEnumerator SelectLeftRoutine()
    {
        SetToPrevious();
        yield return new WaitForSeconds(0.5f);
        canGoLeft = true;
    }

    private void SetToPrevious()
    {
        currentMode.SetSelected(false);
        for (int i = 0; i < modes.Length; i++)
        {
            if (modes[i] == currentMode)
            {
                if (i-1 > -1)
                {
                    currentMode = modes[i - 1];
                    
                }
                else
                {
                    currentMode = modes[modes.Length - 1];
                }
                currentMode.SetSelected(true);
                return;
            }
        }
    }

    private void SetToNext()
    {
        currentMode.SetSelected(false);
        for (int i = 0; i < modes.Length; i++)
        {
            if (modes[i] == currentMode)
            {
                if (i+1 < modes.Length)
                {
                    currentMode = modes[i + 1];
                }
                else
                {
                    currentMode = modes[0];
                }
                currentMode.SetSelected(true);
                return;
            }
        }
        
    }
}