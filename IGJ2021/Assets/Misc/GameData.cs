using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Data", menuName ="Game/Data")]
public class GameData : ScriptableObject, ISerializationCallbackReceiver
{
    public Color activeNodeColor;
    public Color inactiveNodeColor;
    public Color reachableNodeColor;

    public Color highLightReachableColor;
    public Color highLightUnreachableColor;

    public Color goldStarsColor;
    public Color silverStarsColor;
    public Color bronzeStarsColor;

    public int currentLevel;
    public int currentMoves;
    public int currentRating;
    public float currentReveal;

    public float timer;
    public float revealDuration;
    public bool revealed;

    public string tutorialText1;
    public string tutorialText2;

    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        currentMoves = 0;
        currentRating = 0;
    }
}
