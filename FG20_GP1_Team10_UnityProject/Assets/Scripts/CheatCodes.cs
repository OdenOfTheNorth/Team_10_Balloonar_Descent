using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatCodes : MonoBehaviour
{
    private CharacterHealth _playerHealth;
    private CheatCodesInputScript _cheatCodesInput;
    private PolygonCollider2D[] _polygonCollider2D;

    private void Awake()
    {
        _cheatCodesInput = new CheatCodesInputScript();
        _playerHealth = GetComponent<CharacterHealth>();
        _polygonCollider2D = GetComponentsInChildren<PolygonCollider2D>();
    }

    private void OnEnable()
    {
        _cheatCodesInput.Enable();
		_cheatCodesInput.Cheats.ToggleChunkCheatVisibility.performed += ChunkManager.ToggleCheatVisibility;
    }

    public void OnDisable()
    {
		_cheatCodesInput.Cheats.ToggleChunkCheatVisibility.performed -= ChunkManager.ToggleCheatVisibility;
	    _cheatCodesInput.Disable();
    }

    private void Update()
    {
        
        
       float infiniteHealth = _cheatCodesInput.Cheats.InfiniteHealth.ReadValue<float>();
       float noclipmode = _cheatCodesInput.Cheats.NoclipMode.ReadValue<float>();

       if (infiniteHealth != 0f)
            InfiniteHealth();

       if (noclipmode != 0f)
            NoclipMode();
    }

    private void InfiniteHealth()
    {
        if (_playerHealth.canTakeDamage)
        {
            _playerHealth.canTakeDamage = false;
            _playerHealth.CurrentHealth = _playerHealth.maxHealth;
        }
        else
        {
            _playerHealth.canTakeDamage = true;
        }
    }

    private void NoclipMode()
    {
        if (_polygonCollider2D[0])
        {
            for (int i = 0; i < _polygonCollider2D.Length; i++)
            {
                _polygonCollider2D[i].enabled = !_polygonCollider2D[i].enabled;
            }
        }
    }
    

}
