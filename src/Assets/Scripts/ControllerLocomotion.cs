using UnityEngine;

public class ControllerLocomotion : MonoBehaviour
{
    public enum Controller { Left, Right, Both };
    public Controller controller = Controller.Both;

    public GameObject mainController;
    public GameObject subController;

    public float maxSpeed = 3.0f;
    public float deadzone = 0.2f;

    // for snap turn
    bool handDirected          = false;         // hand direction true
    
    public bool verticalFlying = false;

    OVRCameraRig cameraRig = null;


    // Start is called before the first frame update
    void Start()
    {
        cameraRig = GetComponent<OVRCameraRig>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 thumbstickVector = new Vector2();
        Vector2 thumbstickVectorLeft = new Vector2();

        thumbstickVector = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
        thumbstickVectorLeft = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);

        if (controller == Controller.Left)
        {
            thumbstickVector = thumbstickVectorLeft;
        }
        else if (controller == Controller.Both)
        {
            if (thumbstickVectorLeft.x >= deadzone || thumbstickVectorLeft.x <= -deadzone || thumbstickVectorLeft.y >= deadzone || thumbstickVectorLeft.y <= -deadzone)
            {
                thumbstickVector = thumbstickVectorLeft;
            }
        }

        // enables hand direction
        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            if (!handDirected)
            {
                handDirected = true;
            }
            else
            {
                handDirected = false;
            }
        }

        // direction and movement of the player
        if (thumbstickVector.y > deadzone || thumbstickVector.y < -deadzone)
        {
            Vector3 movementVector = new Vector3(0, 0, Time.deltaTime * maxSpeed * thumbstickVector.y);
            Quaternion travelDirection = cameraRig.centerEyeAnchor.rotation;
            if (!handDirected)
            {
                travelDirection = cameraRig.centerEyeAnchor.rotation;
            }
            else if (handDirected)
            {
                travelDirection = mainController.transform.rotation;
            }


            if (!verticalFlying)
            {
                Vector3 restrictedTravelVector = travelDirection * new Vector3(0, 0, 1.0f);
                restrictedTravelVector[1] = 0.0f;
                restrictedTravelVector.Normalize();
                travelDirection = Quaternion.LookRotation(restrictedTravelVector);
            }

            transform.localPosition = transform.localPosition + travelDirection * movementVector;
        }
    }
}
