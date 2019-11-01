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


    // Start is called before the first frame update
    void Start()
    {
        //spawning laser
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
    }


    // Update is called once per frame
    void Update()
    {
        if (teleportAction.GetState(handType))
        {
            //Raycast 
            RaycastHit hit;

            //Ray getting out from the controller in forward direction at max of 100 length.
            if(Physics.Raycast(pose.transform.position, transform.forward, out hit, 100))
            {
                //if it hits somehthing, set our hitpoint   
                hitPoint = hit.point;
                ShowLaser(hit);
            }
        }
        else
        {
            laser.SetActive(false);
        }
    }
}
