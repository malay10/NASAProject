using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;

    public GameObject impact;

    private Vector3 startPos;
    private void Start()
    {
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }
        if(Vector3.Distance(startPos, gameObject.transform.position) > 500)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision obj)
    {
        GameObject impactGO = Instantiate(impact, gameObject.transform.position, Quaternion.LookRotation(obj.contacts[0].normal));
        Destroy(impactGO, .5f); 
        Destroy(gameObject);
    }
}
