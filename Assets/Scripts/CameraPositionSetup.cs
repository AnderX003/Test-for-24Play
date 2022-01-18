using UnityEngine;
using UnityEngine.Assertions;

public class CameraPositionSetup : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CameraSetup cameraSetup;

    private void Awake()
    {
        float ratio = Screen.width / (float)Screen.height;
        Assert.IsFalse(ratio >= 3/4f);
        Assert.IsFalse(ratio <= 1/2f);
        
        cameraTransform.position = Vector3.Lerp(
            cameraSetup.positionAt1by2, 
            cameraSetup.positionAt3by4, 
            (ratio - 0.5f) * 4); // 1/2 <-> 3/4 => 0 <-> 1

        cameraTransform.localEulerAngles = Vector3.Lerp(
            cameraSetup.rotationAt1by2, 
            cameraSetup.rotationAt3by4, 
            (ratio - 0.5f) * 4);
    }
}
