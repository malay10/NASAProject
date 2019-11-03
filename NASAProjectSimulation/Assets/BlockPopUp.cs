using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPopUp : MonoBehaviour
{

    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.FindGameObjectWithTag("projectile");
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Setting True");
        panel.SetActive(true);
    }
}
