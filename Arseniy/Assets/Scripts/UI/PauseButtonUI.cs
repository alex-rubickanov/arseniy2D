using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PauseButtonUI : MonoBehaviour
{
    [SerializeField] private bool isPauseActive = false;
    [SerializeField] private GameObject pauseObject;
    [SerializeField] private GameObject crossbowAbilityButton;    
    [SerializeField] private GameObject mortarAbilityButton;
    [SerializeField] private GameObject fireGunAbilityButton;
    public void Pause()
    {
        if (!isPauseActive) {
            pauseObject.SetActive(true);
            isPauseActive = true;
            crossbowAbilityButton.SetActive(false);
            mortarAbilityButton.SetActive(false);
            fireGunAbilityButton.SetActive(false);
            Time.timeScale = 0;
        } else {
            pauseObject.SetActive(false);
            isPauseActive = false;
            crossbowAbilityButton.SetActive(true);
            mortarAbilityButton.SetActive(true);
            fireGunAbilityButton.SetActive(true);
            Time.timeScale = 1;
        }
    }
}
