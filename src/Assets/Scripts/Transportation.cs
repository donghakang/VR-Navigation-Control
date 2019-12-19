using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transportation : MonoBehaviour
{
    // for ray cast
    Color lineColor = Color.black;
    float lineWidth = 0.01f;
    private LineRenderer lineRenderer;

    public GameObject mainController;
    public GameObject subController;

    bool transportTrigger = false;
    bool buttonReleased = true;

    RaycastHit hitInfo;
    Vector3 currentPosition;
    Vector3 endPosition;
    Vector3 transportLocation = new Vector3(0, 0, 0);

    public float transitionTime = 0.1f;
    float transitionProgress = 1.0f;

    Vector3 currentLocation = Vector3.zero;
    Vector3 endLocation = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = mainController.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.positionCount = 2;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // set the selectable object to null initially
        Selectable selectable = null;

        // get the current position


        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstick))
        {
            buttonReleased = false;
            currentPosition = mainController.transform.position;
            endPosition = currentPosition + (1000 * mainController.transform.TransformDirection(Vector3.forward));
            if (Physics.Raycast(currentPosition, mainController.transform.TransformDirection(Vector3.forward), out hitInfo, Mathf.Infinity))
            {
                selectable = hitInfo.transform.GetComponent<Selectable>();
            }

            lineRenderer.enabled = true;
            if (selectable != null)
            {
                lineRenderer.startColor = Color.blue;
                lineRenderer.SetPosition(0, currentPosition);
                lineRenderer.SetPosition(1, hitInfo.point);
                transportTrigger = true;
                transportLocation = hitInfo.point;
            }
            else
            {
                lineRenderer.startColor = Color.black;
                lineRenderer.SetPosition(0, currentPosition);
                lineRenderer.SetPosition(1, endPosition);
            }
        }
        else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick))
        {
            buttonReleased = false;
            currentPosition = subController.transform.position;
            endPosition = currentPosition + (1000 * subController.transform.TransformDirection(Vector3.forward));
            if (Physics.Raycast(currentPosition, subController.transform.TransformDirection(Vector3.forward), out hitInfo, Mathf.Infinity))
            {
                selectable = hitInfo.transform.GetComponent<Selectable>();
            }

            lineRenderer.enabled = true;
            if (selectable != null)
            {
                lineRenderer.startColor = Color.red;
                lineRenderer.SetPosition(0, currentPosition);
                lineRenderer.SetPosition(1, hitInfo.point);
                transportTrigger = true;
                transportLocation = hitInfo.point;
            }
            else
            {
                lineRenderer.startColor = Color.black;
                lineRenderer.SetPosition(0, currentPosition);
                lineRenderer.SetPosition(1, endPosition);
            }
        }
        else
        {
            buttonReleased = true;
            lineRenderer.enabled = false;
        }

        if (transportTrigger && buttonReleased)
        {
            transitionProgress = 0;
            currentLocation = transform.position;
            endLocation = transportLocation;
            transportTrigger = false;
        }

        if (transitionProgress < 1)
        {
            transitionProgress += Time.deltaTime / transitionTime;
            transform.position = Vector3.Lerp(currentLocation, endLocation, transitionProgress);
        }
    }


    //void transport (Vector3 transportPosition)
    //{ 
    //    transform.position = transportPosition;
    //}

}
