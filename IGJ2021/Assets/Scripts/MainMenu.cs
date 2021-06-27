using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public LevelData[] levels;
    public GameData gameData;

    //public TextMeshProUGUI[] levelBestMovesText;
    //public TextMeshProUGUI[] levelBestRatingText;
    public TextMeshProUGUI[] levelBestText;
    public GameObject[] completedStars;

    private void Awake()
    {
        if (levels != null)
        {
            //levelBestMovesText = new TextMeshProUGUI[levels.Length];
            //levelBestRatingText = new TextMeshProUGUI[levels.Length];
            //levelBestText = new TextMeshProUGUI[levels.Length];
            CheckForPreviousProgress();
            UpdateHighScores();
        }
    }

    private void Start()
    {

        UpdateHighScores();
    }

    public void CheckForPreviousProgress()
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
                int bestRating = PlayerPrefs.GetInt($"Level_{levels[i].level}_Best_Rating", 0);
                levels[i].bestRating = bestRating;
            }

        }
    }

    public void UpdateHighScores()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].completed)
            {
                completedStars[i].SetActive(true);
                
                for (int j = 0; j < levels[i].bestRating; j++)
                {
                    completedStars[i].transform.GetChild(j).gameObject.SetActive(true);

                    if (levels[i].bestRating == 3)
                    {
                        completedStars[i].transform.GetChild(j).gameObject.GetComponent<Image>().color = gameData.goldStarsColor;
                    }
                    else if (levels[i].bestRating == 2)
                    {
                        completedStars[i].transform.GetChild(j).gameObject.GetComponent<Image>().color = gameData.silverStarsColor;
                    }
                    else
                    {
                        completedStars[i].transform.GetChild(j).gameObject.GetComponent<Image>().color = gameData.bronzeStarsColor;
                    }
                }
            }
            else
            {
                completedStars[i].SetActive(false);
            }
        }
        //for (int i = 0; i < levels.Length; i++)
        //{
        //    bool completed;
        //    int checkIfCompleted = PlayerPrefs.GetInt($"Level_{levels[i].level}_Completed", 0);

        //    if (checkIfCompleted == 0)
        //    {
        //        completed = false;
        //    }
        //    else
        //    {
        //        completed = true;
        //    }

        //    levels[i].completed = completed;

        //    if (completed)
        //    {
        //        int bestMoves = PlayerPrefs.GetInt($"Level_{levels[i].level}_Best_Moves", levels[i].maxMoves);
        //        levels[i].bestMoves = bestMoves;
        //        //levelBestMovesText[i].text = $"{bestMoves}";
        //        int bestRating = PlayerPrefs.GetInt($"Level_{levels[i].level}_Best_Rating", 0);
        //        levels[i].bestRating = bestRating;
        //        levelBestText[i].text = $"Level {i} Best Moves: {bestMoves}   Best Rating: {bestRating}";
        //        //levelBestRatingText[i].text = $"{bestRating}";
        //    }

        //}

    }

    public void SetLevel(int level)
    {
        gameData.currentLevel = level;
    }

    public void ResetHighScores()
    {
        PlayerPrefs.DeleteAll();
        
        foreach (LevelData level in levels)
        {
            level.completed = false;
            level.bestMoves = level.maxMoves;
            level.bestRating = 0;
            //gameData.currentRating = 0;
        }

        foreach (TextMeshProUGUI text in levelBestText)
        {
            text.enabled = false;
        }

        UpdateHighScores();
    }
}
