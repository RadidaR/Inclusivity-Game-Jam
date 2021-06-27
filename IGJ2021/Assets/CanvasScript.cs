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
    public TextMeshProUGUI spaceBarText;
    public TextMeshProUGUI tutorialText;

    public Image[] stars;
    public Image[] bestStars;

    public GameObject levelCompletedScreen;
    public Slider revealSlider;
    public float revealValue;

    private void Start()
    {
        gameData = GetComponent<GameManager>().gameData;
        currentLevel = GetComponent<GameManager>().currentLevel;
        UpdateMoves();
        revealSlider.value = 0;
        revealSlider.maxValue = currentLevel.revealRate;
        UpdateMoves();

        if (currentLevel.level == 0)
        {
            tutorialText.text = gameData.tutorialText1;
            tutorialText.enabled = true;
        }
        else if (currentLevel.level == 1)
        {
            tutorialText.text = gameData.tutorialText2;
            tutorialText.enabled = true;

        }
    }

    //void SetLevel()
    //{
    //    currentLevel = GetComponent<GameManager>().currentLevel;
    //}
    // Update is called once per frame
    private void Update()
    {
        //revealValue = gameData.currentReveal;
        if (!gameData.revealed)
        {
            revealSlider.maxValue = currentLevel.revealRate;
            revealSlider.value = gameData.currentReveal;
        }
        else
        {
            revealSlider.maxValue = gameData.revealDuration;
            revealSlider.value = gameData.timer;
        }

        if (revealSlider.value == revealSlider.maxValue)
        {
            spaceBarText.enabled = true;
        }
        else
        {
            spaceBarText.enabled = false;
        }
    }

    public void UpdateMoves()
    {
        movesText.text = $"{gameData.currentMoves} / {currentLevel.maxMoves}";
    }

    public void UpdateRevealSlider()
    {
        revealSlider.maxValue = gameData.revealDuration;
        revealSlider.value = revealSlider.maxValue;
    }

    public void LevelCompleted()
    {
        endMovesText.text = $" {gameData.currentMoves} moves";
        int rating = gameData.currentRating;

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
        bestMovesText.text = $" Best: {currentLevel.bestMoves} moves";

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
