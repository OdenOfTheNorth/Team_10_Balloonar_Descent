using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreUI : MonoBehaviour
{
    private Text ScoreText;

    private ScoreKeeper scoreKeeper;

    void Setup()
    {
        scoreKeeper = ScoreKeeper.Get();
        ScoreText = this.GetComponent<Text>();
    }

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        ScoreText.text = scoreKeeper.GetCurrentScore().ToString();
    }
}
