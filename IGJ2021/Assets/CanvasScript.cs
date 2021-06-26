using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    public GameData gameData;
    public LevelData currentLevel;

    public TextMeshProUGUI movesText;
    public TextMeshProUGUI endMovesText;
    public TextMeshProUGUI bestMovesText;

    public Image[] stars;
    public Image[] bestStars;

    public GameObject levelCompletedScreen;

    private void Start()
    {
        gameData = GetComponent<GameManager>().gameData;
        currentLevel = GetComponent<GameManager>().currentLevel;
        UpdateMoves();

    }

    //void SetLevel()
    //{
    //    currentLevel = GetComponent<GameManager>().currentLevel;
    //}
    // Update is called once per frame
    public void UpdateMoves()
    {
        movesText.text = $"Moves {gameData.currentMoves} / {currentLevel.maxMoves}";
    }

    public void LevelCompleted()
    {
        endMovesText.text = $" Moves: {gameData.currentMoves}";
        int rating = currentLevel.currentRating;

        for (int i = 0; i < rating; i++)
        {
            stars[i].enabled = true;
        }

        foreach (Image star in stars)
        {
            if (rating == 1)
            {
                star.color = gameData.bronzeStarsColor;
            }
            if (rating == 2)
            {
                star.color = gameData.silverStarsColor;
            }
            if (rating == 3)
            {
                star.color = gameData.goldStarsColor;
            }
        }   

        levelCompletedScreen.SetActive(true);
    }

    public void HighScore()
    {
        bestMovesText.text = $" Best: {currentLevel.bestMoves}";

        for (int i = 0; i < currentLevel.bestRating; i++)
        {
            bestStars[i].enabled = true;
        }

        foreach (Image star in bestStars)
        {
            if (currentLevel.bestMoves <= currentLevel.goldMoves)
            {
                star.color = gameData.goldStarsColor;
            }
            else if (currentLevel.bestMoves <= currentLevel.silverMoves)
            {
                star.color = gameData.silverStarsColor;
            }
            else 
            {
                star.color = gameData.bronzeStarsColor;
            }
        }
        


        //bestMovesText.text = $" Best: {PlayerPrefs.GetInt($"Level_{currentLevel.level}_Best_Moves", 0)}";
        //int bestRating = PlayerPrefs.GetInt($"Level_{currentLevel.level}_Best_Rating", 0);

        //foreach (Image star in bestStars)
        //{

        //    if (bestRating == 1)
        //    {
        //        star.color = gameData.bronzeStarsColor;
        //    }
        //    if (bestRating == 2)
        //    {
        //        star.color = gameData.silverStarsColor;
        //    }
        //    if (bestRating == 3)
        //    {
        //        star.color = gameData.goldStarsColor;
        //    }
        //}
    }
}
