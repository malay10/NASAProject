﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetectionRaycast : MonoBehaviour
{

    private GameObject lastPanel;
    // Start is called before the first frame update
    void Start()
    {
        lastPanel = null;
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit " + hit.collider.name);

            if (hit.collider.CompareTag("Interactable"))
            {
                hit.collider.gameObject.SendMessage("CallDisplay");
                lastPanel = hit.collider.gameObject;
                if (Input.GetKey(KeyCode.F))
                {
                    Debug.Log("CanInteract");
                    hit.collider.gameObject.transform.parent = transform;
                    CamMouseMovement.lookAround = false;
                }
                else if (Input.GetKey(KeyCode.G))
                {
                    hit.collider.gameObject.transform.parent = null;
                    CamMouseMovement.lookAround = true;
                }
            }
            
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
            if(lastPanel != null)
            {
                lastPanel.SendMessage("HideDisplay");
                lastPanel = null;
            }


        }
    }
}
