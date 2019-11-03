using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    private Transform pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            pos.Translate( - 2f * 5* Time.deltaTime, 0, 0); // new Vector3(,0, 0);
        }
    }
}
