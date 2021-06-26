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

    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        currentMoves = 0;
    }
}
