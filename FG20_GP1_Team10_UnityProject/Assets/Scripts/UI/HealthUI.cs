using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider = null;
    [SerializeField] private Button gameOverButton = null;

    private CharacterHealth _characterHealth = null;

    private void Awake()
    {
        _characterHealth = GetComponent<CharacterHealth>();
    }

    private void Update()
    {
        CheakIfUseUI();
        SliderValue();
    }
    
    public void SliderValue()
    {
        healthSlider.value = _characterHealth.CurrentHealth / _characterHealth.maxHealth;
        if (_characterHealth.CurrentHealth <= 0f)
        {
            gameOverButton.gameObject.SetActive(true);
        }
        else
        {
            gameOverButton.gameObject.SetActive(false);
        }
    }

    void CheakIfUseUI()
    {
        if (_characterHealth.UseUI)
        {
            healthSlider.gameObject.SetActive(true);
        }
        else
        {
            healthSlider.gameObject.SetActive(false);
        }
    }
}
