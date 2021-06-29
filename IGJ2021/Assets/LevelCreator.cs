using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public LevelData level;
    public Vector3[] points;
    public Transform playerPosition;


    private void OnValidate()
    {
        if (level != null)
        {
            if (gameObject.activeInHierarchy)
            {
                points = new Vector3[gameObject.transform.childCount];
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = gameObject.transform.GetChild(i).position;
                }

                level.points = new Vector3[points.Length];
                for (int i = 0; i < points.Length; i++)
                {
                    level.points[i] = points[i];
                }

                level.startPoint = playerPosition.position;
            }
            else
            {
                points = null;
                level.points = null;
            }
        }  


    }
}
