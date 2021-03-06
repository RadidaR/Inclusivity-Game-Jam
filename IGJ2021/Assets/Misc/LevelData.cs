using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Game/Level")]
public class LevelData : ScriptableObject, ISerializationCallbackReceiver
{
    public int level;
    public Vector3[] points;
    public Vector2 startPoint;

    public int goldMoves;
    public int silverMoves;
    public int maxMoves;

    public int revealRate;

    public int bestRating;
    public int bestMoves;

    public bool completed;

    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {

    }
}
