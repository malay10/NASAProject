using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraineeMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject laptop;
    //adjusting moving speed
    [SerializeField]
    private float smoothSpeed = 4;
    [SerializeField]
    private Vector3 offset = new Vector3(0.5f,0,1);
    void Start()
    {
        //getting the object
        laptop = GameObject.FindGameObjectWithTag("Laptop");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoveToDest(laptop.transform.position + offset));
    }

    IEnumerator MoveToDest(Vector3 pos)
    {
        Debug.Log("StillMovingOutside");
        while (transform.position != pos)
        {
            Debug.Log("StillMoving");
            transform.position = Vector3.MoveTowards(transform.position, pos, smoothSpeed * Time.deltaTime);
            yield return 0;
        }
    }
}
