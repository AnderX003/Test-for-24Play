using UnityEngine;

public class CameraPositionSetup : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private void Awake()
    {
        float ratio = Screen.width / (float)Screen.height;

        cameraTransform.position = Vector3.Lerp(
            new Vector3(0, 40f, -17f),
            new Vector3(0, 29f, -20f),
            (ratio - 0.5f) * 4);

        cameraTransform.localEulerAngles = Vector3.Lerp(
            new Vector3(68f, 0, 0),
            new Vector3(58f, 0, 0),
            (ratio - 0.5f) * 4);
    }
}
