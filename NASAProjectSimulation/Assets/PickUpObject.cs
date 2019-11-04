using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickUpObject : MonoBehaviour
{
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;
    private GameObject tempObject = null;
    private bool canInstantiate = true;
    [SerializeField]
    private Camera detailViewCam;
    [SerializeField]
    private GameObject objectPreview;

    Vector3 newRotation;

    private void Start()
    {
        detailViewCam = GameObject.FindGameObjectWithTag("DetailView").GetComponent<Camera>();
        newRotation = gameObject.transform.rotation.eulerAngles;
        objectPreview = GameObject.FindGameObjectWithTag("ObjectPreviewCanvas");
        objectPreview.SetActive(false);
    }


    private void Update()
    {
        if (!CamMouseMovement.lookAround)
        {
            StartCoroutine(Rotate());
            this.GetComponent<BlockPopUp>().HideDisplay();
            objectPreview.SetActive(true);
            detailViewCam.SendMessage("GetObject", this.gameObject);
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            float v = verticalSpeed * Input.GetAxis("Mouse Y");
            transform.Rotate(v, h, 0);
        }
        else
        {
            
            //transform.rotation = tempTransform.rotation;
            canInstantiate = true;
           
        }
    }



    IEnumerator Rotate()
    {
        
        yield return new WaitUntil(() => CamMouseMovement.lookAround == true);
        Debug.Log("DoneRotation");
        transform.eulerAngles = newRotation;
        objectPreview.SetActive(false);
        
    }


}
