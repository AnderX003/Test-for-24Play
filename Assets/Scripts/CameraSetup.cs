using UnityEngine;

[CreateAssetMenu(fileName = "CameraSetup", menuName = "Data/CameraSetup", order = 51)]
public class CameraSetup : ScriptableObject
{
    public Vector3 positionAt3by4; 
    public Vector3 rotationAt3by4; 
    public Vector3 positionAt1by2; 
    public Vector3 rotationAt1by2; 
}
