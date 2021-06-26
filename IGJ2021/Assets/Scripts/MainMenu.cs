using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public LevelData[] levels;
    public GameData gameData;

    //public TextMeshProUGUI[] levelBestMovesText;
    //public TextMeshProUGUI[] levelBestRatingText;
    public TextMeshProUGUI[] levelBestText;

    private void Awake()
    {
        if (levels != null)
        {
            //levelBestMovesText = new TextMeshProUGUI[levels.Length];
            //levelBestRatingText = new TextMeshProUGUI[levels.Length];
            //levelBestText = new TextMeshProUGUI[levels.Length];
            UpdateHighScores();            
        }
    }

    public void UpdateHighScores()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            bool completed;
            int checkIfCompleted = PlayerPrefs.GetInt($"Level_{levels[i].level}_Completed", 0);

            if (checkIfCompleted == 0)
            {
                completed = false;
            }
            else
            {
                completed = true;
            }

            levels[i].completed = completed;

            if (completed)
            {
                int bestMoves = PlayerPrefs.GetInt($"Level_{levels[i].level}_Best_Moves", levels[i].maxMoves);
                levels[i].bestMoves = bestMoves;
                //levelBestMovesText[i].text = $"{bestMoves}";
                int bestRating = PlayerPrefs.GetInt($"Level_{levels[i].level}_Best_Rating", 0);
                levels[i].bestRating = bestRating;
                levelBestText[i].text = $"Level {i} Best Moves: {bestMoves}   Best Rating: {bestRating}";
                //levelBestRatingText[i].text = $"{bestRating}";
            }

        }

    }

    public void SetLevel(int level)
    {
        gameData.currentLevel = level;
    }

    public void ResetHighScores()
    {
        PlayerPrefs.DeleteAll();
        
        foreach (TextMeshProUGUI text in levelBestText)
        {
            text.enabled = false;
        }

        UpdateHighScores();
    }
}
