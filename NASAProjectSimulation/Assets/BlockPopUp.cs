using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPopUp : MonoBehaviour
{

    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.FindGameObjectWithTag("DisplayInfo");
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallDisplay()
    {
        Debug.Log("Setting True");
        panel.SetActive(true);
    }
    public void HideDisplay()
    {
        panel.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Detect the collider
        Debug.Log("Setting True");
        panel.SetActive(true);
    }

    private void OnCollisionExit(Collision collision)
    {

        Debug.Log("HIde the panel");
        panel.SetActive(false);
    }

    IEnumerator Display()
    {
        yield return new WaitForSeconds(1);
        //panel active
    }

    private void OnDestroy()
    {
    
    }
}
