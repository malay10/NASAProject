using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    public SteamVR_Action_Boolean teleportAction;
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose pose;

    //Laser Prefab
    public GameObject laserPrefab;
    //Instance of laser
    private GameObject laser;
    //Position of laster
    private Transform laserTransform;
    //Laser hitting the surface. 
    private Vector3 hitPoint;

    public Transform cameraTransform;
    //teleport location marker prefab
    public GameObject destLocationMarkerPrefab;
    //destination Marker 
    public GameObject destMarker;
    //marker position
    private Transform destPosition;
    //Camera track
    public Transform headPosition;
    //offset for dest Marker
    public Vector3 offset = new Vector3(0,0.5f,0);
    //Layer where teleport is allowed
    public LayerMask destinationMask;
    //Move possible
    private bool shouldTeleport;

    void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);

        //hitting at the center 
        laser.transform.position = Vector3.Lerp(pose.transform.position, hitPoint, .5f);
        //Change rotation with the camera
        laserTransform.LookAt(hitPoint);
        //Length of laser. 
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }

    void Teleport()
    {
        //cannot move after this
        shouldTeleport = false;
        //after moving remove from scene
        destMarker.SetActive(false);
        //distance calc
        Vector3 distance = cameraTransform.position - headPosition.position;
        //Stick to floor.
        distance.y = 0;
        //move to dest
        cameraTransform.position = hitPoint + distance;
    }


    // Start is called before the first frame update
    void Awake()
    {
        //spawning laser
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        //spawing destination marker
        destMarker = Instantiate(destLocationMarkerPrefab);
        destPosition = destMarker.transform;
    }


    // Update is called once per frame
    void Update()
    {
        if (teleportAction.GetState(handType))
        {
            //Raycast 
            RaycastHit hit;

            //Ray getting out from the controller in forward direction at max of 100 length.
            if (Physics.Raycast(pose.transform.position, transform.forward, out hit, 100, destinationMask))
            {
                //if it hits somehthing, set our hitpoint   
                hitPoint = hit.point;
                ShowLaser(hit);
                destMarker.SetActive(true);
                destPosition.position = hitPoint + offset;
                shouldTeleport = true;
            }
        }
        else
        {
            laser.SetActive(false);
            destMarker.SetActive(false);
        }
        if(teleportAction.GetStateUp(handType) && shouldTeleport)
        {
            //teleport when hit the right layermask
            Teleport();
        }
    }
}
