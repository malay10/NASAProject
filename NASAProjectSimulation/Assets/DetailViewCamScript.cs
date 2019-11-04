using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailViewCamScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetObject(GameObject abc)
    {
        Debug.Log(abc.name + " " + abc.transform);
        transform.position = new Vector3 (abc.transform.position.x, abc.transform.position.y, abc.transform.position.z -4);
        
        //Object to display,

        //align the camera to the object view
    }
}
