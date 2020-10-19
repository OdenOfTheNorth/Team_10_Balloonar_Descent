using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //manages game active and UI


    #region STATIC
    private static GameManager gameManagerCurrent;
    public static GameManager Get() { return gameManagerCurrent; }

    private void Awake()
    {
        gameManagerCurrent = this;
    }
    #endregion STATIC

    #region VARIABLES
    public bool gameActive=true;
    #endregion VARIABLES

    #region REFERENCES
    [SerializeField]
    private GameObject LoseScreen;
    [SerializeField]
    private GameObject HighscoreUI;
    [SerializeField]
    private GameObject HighScoreAddUI;
    [SerializeField]
    private Text HighScoreAddUIInfoText;


    public Text HighScoreAddUIInputText;
    #endregion REFERENCES

    public void SetGameActive()
    {
        gameActive = true;
        SetLoseUIActive(!gameActive);
    }

    public void SetGameInactive()
    {
        gameActive = false;
        SetLoseUIActive(!gameActive);
    }

    private void Start()
    {
        Highscore currentData = new Highscore();
        Highscore.Set(currentData);

        SetGameActive();
    }

    private void UpdateHighScoreAddUIInfoText()
    {
        string info = "";
        info = info + "Score: " + ScoreKeeper.Get().GetCurrentScore() + " points";
        HighScoreAddUIInfoText.text = info;
    }

    private void SetLoseUIActive(bool boolvalue)
    {
        SetHighscoreUIActive(boolvalue);
    }

    private void SetHighscoreUIActive(bool boolValue)
    {
        if (boolValue == true)
        {
            UpdateHighScoreAddUIInfoText();
            HighscoreUI.GetComponent<HighscoreTableUI>().UpdateHighscores();
            LoseScreen.SetActive(true);
        }
        else
        {
            LoseScreen.SetActive(false);
        }
    }

    public void AddScore()
    {
        Highscore.Get().SaveNewHighscore();
        HighscoreUI.GetComponent<HighscoreTableUI>().UpdateHighscores();
    }
}
