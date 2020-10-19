using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class CharacterHealth : MonoBehaviour
{
    public bool UseUI = true;
    public bool canTakeDamage = true;

    // Health values
    public float maxHealth = 100.0f;
    public float CurrentHealth = 0.0f;
    public Renderer renderer = null;
    public List<Material> DamageMaterial = new List<Material>();
    
    private HealthUI _healthUI = null;

    private Joint2D[] balloonJoints;


    private void Awake()
    {
        CurrentHealth = maxHealth;
        _healthUI = GetComponent<HealthUI>();
        balloonJoints = GetComponentsInChildren<Joint2D>();
    }

    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            if(CurrentHealth > 0)
                CameraFollow.AddTrauma(damage);
            CurrentHealth -= damage;

            //Damage SFX
            SoundManager.instance.PlayPlayerDamage(damage);
        }

        if (CurrentHealth <= 0f)
        {
            Die();
        }

        float[] healthIntervals = new[] {1f, 0.75f, 0.5f, 0.25f};
        for (int i = 0; i < healthIntervals.Length; i++)
        {
            if (CurrentHealth < maxHealth * healthIntervals[i])
            {
                renderer.material = DamageMaterial[i];
            }
        }

        if (_healthUI)
        {
            _healthUI.SliderValue();
        }
    }

    private void Die()
    {
        foreach (Joint2D j in balloonJoints)
        {
            j.connectedBody = null;
            j.enabled = false;
        }

        ScoreKeeper _scoreKeeper = ScoreKeeper.Get();
        _scoreKeeper.updateScore = false; //Deactivate score

        GameManager.Get().SetGameInactive();

        //Stopping Sound
        //MusicController.musicEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // TODO commented out due to compiler error, uncomment when a fix is ready
    }
}