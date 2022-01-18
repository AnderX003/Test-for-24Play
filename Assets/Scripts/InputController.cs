using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool IsDragging { get; private set; }
    public Vector2 StartTouch { get; private set; }
    public Vector2 DragDelta { get; private set; }
    public bool Enabled { get; set; }
    public Action ActionAfterReset { get; set; }

    private void Update()
    {
        if (!Enabled) return;

        //getting start touch position
        
#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            IsDragging = true;
            StartTouch = Input.mousePosition;
        }
        else if (IsDragging && Input.GetMouseButtonUp(0))
        {
            IsDragging = false;
            Reset();
            ActionAfterReset?.Invoke();
        }

#endif

#if UNITY_ANDROID

        if (Input.touches.Length != 0)
        {
            switch (Input.touches[0].phase)
            {
                case TouchPhase.Began:
                    IsDragging = true;
                    StartTouch = Input.touches[0].position;
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if(!IsDragging) break;
                    IsDragging = false;
                    Reset();
                    ActionAfterReset?.Invoke();
                    break;
            }
        }

#endif

        //calculationg the delta of the drag
        
        DragDelta = Vector2.zero;
        if (IsDragging)
        {
            if (Input.touches.Length > 0)
            {
                DragDelta = Input.touches[0].position - StartTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                DragDelta = (Vector2)Input.mousePosition - StartTouch;
            }
        }
    }
    
    private void Reset()
    {
        IsDragging = false;
        StartTouch = Vector2.zero;
        DragDelta = Vector2.zero;
    }
}