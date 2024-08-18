using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontroller : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;

    public void SwitchPauseMenu()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
    }
}
