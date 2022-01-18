using UnityEngine;

public class AppManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform cameraTransform;
    
    private void Awake()
    {
        float ratio = canvas.renderingDisplaySize.x / canvas.renderingDisplaySize.y;
        if (ratio > 3 / 4f)
        {
            cameraTransform.position = new Vector3(0, 28.1f, -21.1f);
            cameraTransform.localEulerAngles = new Vector3(58.227f, 0, 0);
        }
        else if (ratio > 10 / 16f)
        {
            cameraTransform.position = new Vector3(0, 31.3f, -23.1f);
            cameraTransform.localEulerAngles = new Vector3(58.227f, 0, 0);

        }
        else if (ratio > 9 / 16f)
        {
            cameraTransform.position = new Vector3(0, 34.6f, -22.6f);
            cameraTransform.localEulerAngles = new Vector3(61.298f, 0, 0);
        } else if (ratio > 1 / 2f) {} else if (ratio > 0.48)
        {
            cameraTransform.position = new Vector3(0, 39.7f, -22.5f);
            cameraTransform.localEulerAngles = new Vector3(63.556f, 0, 0);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
