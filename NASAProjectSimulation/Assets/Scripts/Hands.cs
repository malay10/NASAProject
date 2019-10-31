using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Hands : MonoBehaviour
{
    //Picking up and droping object
    public SteamVR_Action_Boolean grabAction = null;
    //
    public SteamVR_Behaviour_Pose pose = null;
    //
    public FixedJoint joint = null;

    private CanInteract currentInteractable = null;
    [SerializeField]
    private List<CanInteract> interactableObjects = new List<CanInteract>();


    // Start is called before the first frame update
    void Start()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        joint = GetComponent<FixedJoint>();

        //interactableObjects.Add(GameObject.FindGameObjectWithTag("Interactable").GetComponent<Interactable>());
        //Debug.Log(CanInteractObjects[0].name + ":)");
    }

    // Update is called once per frame
    void Update()
    {
        //Grab
        if (grabAction.GetStateDown(pose.inputSource))
        {
            Debug.Log(pose.inputSource + "TriggerDown");
            PickUp();
        }
        //Drop
        if(grabAction.GetStateUp(pose.inputSource))
        {
            Debug.Log(pose.inputSource + "TriggeredUP");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("CanInteract"))
            return;
        //making object CanInteract with controller.
        interactableObjects.Add(other.gameObject.GetComponent<CanInteract>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        //Removing object from interactable with controller
        interactableObjects.Remove(other.gameObject.GetComponent<CanInteract>());
    }
    public void Drop()
    {
        //Null
        if (!currentInteractable)
            return;

        Rigidbody target = currentInteractable.GetComponent<Rigidbody>();
        //Velocity
        target.velocity = pose.GetVelocity();
        target.angularVelocity = pose.GetAngularVelocity();

        //Detach 
        joint.connectedBody = null;

        //clear hands
        currentInteractable.activeHand = null;
        currentInteractable = null;


    }
    public void PickUp()
    {
        //Getting Nearest object
        currentInteractable = GetNeareastInteractable();

        //Not in radius
        if (!currentInteractable)
            return;

        //If object is held by 2nd controller, we will swap it
        if (currentInteractable.activeHand != null)
            currentInteractable.activeHand.Drop();

        //Poisiton
        currentInteractable.transform.position = transform.position;

        //Attach Rigidbody
        Rigidbody target = currentInteractable.GetComponent<Rigidbody>();
            //connecting object with hands
        joint.connectedBody = target;

        //Set active hand
        currentInteractable.activeHand = this;
    }

    public CanInteract GetNeareastInteractable()
    {
        //Var for returning the neareast object
        CanInteract nearest = null;
        float minDist = float.MaxValue;
        //var to calculate distance from the controller
        float distance = 0.0f;
        //looping throught all objects to find cloasest. 
        foreach(CanInteract interactable in interactableObjects)
        {
            //how far is the object in distance, scalar.
            distance = (interactable.transform.position - transform.position).sqrMagnitude;
            if (distance < minDist)
            {
                minDist = distance;
                nearest = interactable;
            }
        }


        return nearest;
    }

}
