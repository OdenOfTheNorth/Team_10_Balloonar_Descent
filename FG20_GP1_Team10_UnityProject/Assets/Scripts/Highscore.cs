using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;
using System.IO;
using System.Text;

[System.Serializable]
public class HighScoreEntry
{
    public int Score { get; set; }
    public string PlayerName { get; set; }
}

public class Highscore
{
    private static Highscore CurrentData;
    public static Highscore Get() { return CurrentData; }
    public static void Set(Highscore newCurrentData) { CurrentData = newCurrentData; }

    private string highscorePath = "/highscores.dat";

    public List<HighScoreEntry> highscoreList = new List<HighScoreEntry>();

    public Highscore()
    {
        LoadHighscores();
    }

    public void SetNewHighscoreList(List<HighScoreEntry> newList)
    {
        highscoreList = newList;
    }

    public static int SortByScore(HighScoreEntry h1, HighScoreEntry h2)
    {
        return h1.Score.CompareTo(h2.Score) * -1;
    }

    public void AddHighScore(int newScore, string newPlayerName)
    {
        HighScoreEntry newScoreObject = new HighScoreEntry();
        newScoreObject.PlayerName = newPlayerName;
        newScoreObject.Score = newScore;


        highscoreList.Add(newScoreObject);

        SortAndRemove();

        //HighScore.Get().SetNewHighscoreList(highscoreList);
    }

    public void SortAndRemove()
    {
        highscoreList.Sort(SortByScore);

        while (highscoreList.Count > 10)
        {
            highscoreList.Remove(highscoreList[highscoreList.Count - 1]);
        }
    }

    public void SaveAllInformation()
    {
        Save(Application.persistentDataPath + highscorePath, highscoreList);
        Debug.Log(Application.persistentDataPath + highscorePath);
        LoadHighscores();
    }


    public void Save(string path, object information)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.OpenOrCreate);


        bf.Serialize(file, information);

        file.Close();
    }


    public void LoadHighscores()
    {

        if (File.Exists(Application.persistentDataPath + highscorePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + highscorePath, FileMode.Open);
            List<HighScoreEntry> highscoreListLoaded = (List<HighScoreEntry>)bf.Deserialize(file);
            highscoreList = highscoreListLoaded;
            file.Close();

        }
        else
        {
            AddInitialHighScores();
            LoadHighscores();
        }
    }

    public void AddInitialHighScores()
    {
        List<HighScoreEntry> InitialList = new List<HighScoreEntry>();

        HighScoreEntry HS1 = new HighScoreEntry();
        HighScoreEntry HS2 = new HighScoreEntry();
        HighScoreEntry HS3 = new HighScoreEntry();

        HS1.PlayerName = "Future Gamer";
        HS1.Score = 10000;
        HS2.PlayerName = "Current Gamer";
        HS2.Score = 2500;
        HS3.PlayerName = "Former Gamer";
        HS3.Score = 1000;

        InitialList.Add(HS1);
        InitialList.Add(HS2);
        InitialList.Add(HS3);

        Save(Application.persistentDataPath + highscorePath, InitialList);
    }

    public void SaveNewHighscore()
    {
        string playerName = "Anom";

        if (GameManager.Get().HighScoreAddUIInputText.text != "")
        {
            playerName = GameManager.Get().HighScoreAddUIInputText.text;
        }



        AddHighScore((int)ScoreKeeper.Get().GetCurrentScore(), playerName);

        SaveAllInformation();
    }

}
