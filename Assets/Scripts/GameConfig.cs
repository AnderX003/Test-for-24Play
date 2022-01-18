using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Data/GameConfig", order = 51)]
public class GameConfig : ScriptableObject
{
    public float difficultyRise;
    public float startSensitivity;
    public float draggingSensitivity;
    public float ballSpeed;
    public float playerBoarder;
    public float arrowScaleRatio;
}
