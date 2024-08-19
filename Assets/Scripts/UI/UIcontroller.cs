using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIcontroller : MonoBehaviour
{
    [Header("Pause Menu Settings")]
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private Image[] volumeBars;
    [SerializeField] private Sprite fullBar;
    [SerializeField] private Sprite emptyBar;
    private int volume = 10;

    [Header("Things To Be Disabled When Paused")]
    [SerializeField] private CameraDrag cameraDrag;
    [SerializeField] private Button deckButton;

    public void SwitchPauseMenu()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        Globals.isPausing = PauseMenu.activeSelf;
        Time.timeScale = PauseMenu.activeSelf ? 0f : 1f;
        cameraDrag.enabled = !PauseMenu.activeSelf;
        deckButton.interactable = !PauseMenu.activeSelf;
    }

    public void ChangeVolume(bool isAdd)
    {
        if (isAdd)
        {
            AudioListener.volume = Mathf.Clamp(AudioListener.volume + 0.1f, 0, 1.0f);
            volume = Mathf.Clamp(volume + 1, 0, 10);
        }
        else
        {
            AudioListener.volume = Mathf.Clamp(AudioListener.volume - 0.1f, 0, 1.0f);
            volume = Mathf.Clamp(volume - 1, 0, 10);
        }
        UpdateVolumeDisplay();
    }

    public void UpdateVolumeDisplay()
    {
        for (int i = 0; i < volumeBars.Length; i++)
        {
            if (i < volume)
            {
                volumeBars[i].sprite = fullBar;
            }
            else
            {
                volumeBars[i].sprite = emptyBar;
            }
        }
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
