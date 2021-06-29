using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChecker : MonoBehaviour
{
    public LevelData level;

    public Transform playerPosition;
    public List<Transform> levelPoints;

    private void OnValidate()
    {
        if (level != null)
        {
            if (gameObject.activeInHierarchy)
            {
                levelPoints.Clear();

                for (int i = 0; i < level.points.Length; i++)
                {
                    gameObject.transform.GetChild(i).gameObject.SetActive(true);
                    levelPoints.Add(gameObject.transform.GetChild(i));
                    levelPoints[i].position = level.points[i];
                }

                for (int i = gameObject.transform.childCount - 1; i > level.points.Length - 1; i--)
                {
                    gameObject.transform.GetChild(i).gameObject.SetActive(false);
                }

                playerPosition.gameObject.SetActive(true);
                playerPosition.position = level.startPoint;
            }
            else
            {
                levelPoints.Clear();
                playerPosition.gameObject.SetActive(false);
                for (int i = 0; i < gameObject.transform.childCount - 1; i++)
                {
                    gameObject.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }


    }
}
