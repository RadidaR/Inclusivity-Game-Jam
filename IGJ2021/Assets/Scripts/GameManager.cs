using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    public LevelData[] levels;

    public LevelData currentLevel;
    public GameObject point;
    public GameObject player;

    public List<Transform> currentPoints;

    //public int currentMoves = 0;
    public GameEvent eReachedPoint;
    public GameEvent eReset;
    public GameEvent eLevelCompleted;
    public GameEvent eNewHighScore;

    void Awake()
    {
        gameData.currentMoves = 0;
        gameData.timer = 0;
        gameData.revealed = false;

        foreach (LevelData level in levels)
        {
            if (gameData.currentLevel == level.level)
            {
                currentLevel = level;
            }
        }
        
        if (currentLevel == null)
        {
            currentLevel = levels[levels.Length - 1];
        }

        gameData.currentReveal = currentLevel.revealRate;

        player.transform.position = currentLevel.startPoint;
        SetPoints();
    }

    void SetPoints()
    {
        for (int i=0; i < currentLevel.points.Length; i++)
        {
            currentPoints.Add(Instantiate(point, currentLevel.points[i], Quaternion.identity).transform);

            Collider2D checkForNode = Physics2D.OverlapCircle(currentPoints[i].position, 0.75f);

            if (checkForNode != null)
            {
                currentPoints[i].transform.position = checkForNode.gameObject.transform.position;
            }

            currentPoints[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
        }
    }

    public void MakeMove()
    {
        gameData.currentMoves++;

        if (gameData.currentMoves >= currentLevel.maxMoves)
        {
            eReset.Raise();
        }

        //currentMoves = gameData.currentMoves;
    }

    public void CheckIfReachedPoint()
    {
        for (int i = 0; i < currentPoints.Count; i++)
        {
            Collider2D checkForNode = Physics2D.OverlapCircle(currentPoints[i].position, 0.75f);

            if (checkForNode != null)
            {
                if (checkForNode.gameObject.GetComponent<NodeScript>().isOccupied)
                {
                    ReachedPoint(currentPoints[i]);
                    //Destroy(currentPoints[i].gameObject, 1);
                    //currentPoints.Remove(currentPoints[i]);
                    
                }
            }
        }
    }

    public void ReachedPoint(Transform point)
    {
        if (point == currentPoints[0])
        {
            eReachedPoint.Raise();
            Destroy(point.gameObject);
            currentPoints.Remove(point);

            if (currentPoints.Count == 0)
            {
                eLevelCompleted.Raise();
            }
        }
        else
        {
            eReset.Raise();
            //GameOver
        }
    }

    public void LevelCompleted()
    {

        if (gameData.currentMoves <= currentLevel.goldMoves)
        {
            gameData.currentRating = 3;
        }
        else if(gameData.currentMoves <= currentLevel.silverMoves)
        {
            gameData.currentRating = 2;
        }
        else
        {
            gameData.currentRating = 1;
        }

        if (gameData.currentMoves <= currentLevel.bestMoves)
        {
            currentLevel.bestMoves = gameData.currentMoves;
            currentLevel.bestRating = gameData.currentRating;
            eNewHighScore.Raise();
        }

        PlayerPrefs.SetInt($"Level_{currentLevel.level}_Best_Moves", currentLevel.bestMoves);
        PlayerPrefs.SetInt($"Level_{currentLevel.level}_Best_Rating", currentLevel.bestRating);
        PlayerPrefs.SetInt($"Level_{currentLevel.level}_Completed", 1);

        //int levelBestMoves = PlayerPrefs.GetInt($"Level_{currentLevel.level}_Best_Moves", currentLevel.maxMoves);

        //if (gameData.currentMoves < levelBestMoves)
        //{
        //    PlayerPrefs.SetInt($"Level_{currentLevel.level}_Best_Rating", currentLevel.currentRating);
        //    PlayerPrefs.SetInt($"Level_{currentLevel.level}_Best_Moves", gameData.currentMoves);
        //    eNewHighScore.Raise();
        //}

        //int levelBestRating = PlayerPrefs.GetInt($"Level_{currentLevel.level}_Best_Rating", 0);
        //int levelBestMoves = PlayerPrefs.GetInt("Level " + currentLevel.level.ToString(), 0);

        //if (levelBestRating < currentLevel.currentRating)
        //{
        //}

        //Debug.Log(PlayerPrefs.GetInt("Level " + currentLevel.level.ToString()).ToString());

        currentLevel.completed = true;
    }

    public void NextLevel()
    {
        gameData.currentLevel++;
        eReset.Raise();
    }


}
