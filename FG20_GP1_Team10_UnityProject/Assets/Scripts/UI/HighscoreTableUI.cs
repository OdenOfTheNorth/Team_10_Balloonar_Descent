using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTableUI : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private float templateHeight = 20f;

    private void Awake()
    {
        entryContainer = transform.Find("highScoreEntryContainer");
        entryTemplate = entryContainer.Find("highScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
    }

    public void UpdateHighscores()
    {
        Highscore.Get().SortAndRemove();

        foreach (Transform child in entryContainer)
        {
            if (child != entryTemplate)
            {
                Destroy(child.gameObject);
            }
        }



        List<HighScoreEntry> highscoreList = new List<HighScoreEntry>(Highscore.Get().highscoreList);
        //Debug.Log("Update Highscores. List length: "+highscoreList.Count);
        for (int i = 0; i < highscoreList.Count; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer, true);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            Text position = entryRectTransform.Find("TextPosition").GetComponent<Text>();
            Text score = entryRectTransform.Find("TextScore").GetComponent<Text>();
            Text name = entryRectTransform.Find("TextName").GetComponent<Text>();

            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * (i));

            entryTransform.name = "HighscoreEntry_" + i;
            position.text = (i + 1).ToString();
            name.text = highscoreList[i].PlayerName;
            score.text = highscoreList[i].Score.ToString();

            entryTransform.gameObject.SetActive(true);
        }
    }



}


