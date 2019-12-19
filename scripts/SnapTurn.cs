using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapTurn : MonoBehaviour
{
    public enum Controller { Left, Right, Both };
    public Controller controller = Controller.Both;

    bool    thumbstickTrigger;         // for snap rotation
    bool    triggered = true;
    public float threshold = 0.75f;
    public float turnSpeed = 45.0f;
    public float deadzone = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 thumbstickVector = new Vector2();
        Vector2 thumbstickVectorLeft = new Vector2();

        thumbstickVector = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
        thumbstickVectorLeft = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);

        // rotation of the user
        if (thumbstickVector.x > deadzone || thumbstickVector.x < -deadzone)
        {
            transform.Rotate(0.0f, thumbstickVector.x * turnSpeed * Time.deltaTime, 0.0f);
        }

        // if the Left thumbstick clicks, it triggers.
        if (thumbstickVectorLeft.x > threshold || thumbstickVectorLeft.x < -threshold)
        {
            thumbstickTrigger = true;
            triggered = false;
        }
        else
        {
            thumbstickTrigger = false;
        }

        if (!thumbstickTrigger && !triggered)
        {
            if (thumbstickVectorLeft.x > 0)
            {
                transform.Rotate(0.0f, 45.0f, 0.0f);
            }
            else if (thumbstickVectorLeft.x < 0)
            {
                transform.Rotate(0.0f, -45.0f, 0.0f);
            }
            triggered = true;
        }
        // if the trigger hits it wait until it come backs to original place
        
    }
}
