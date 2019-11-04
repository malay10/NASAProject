using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvtarHUD : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Vector3 newPosition = player.position;

        //transform.position = player.position;

        //transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
    }


}
