using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public float damage = 1f;
    public float range = 1000f;
    public float backfire = 10f;
    public float recoil = 5f;

    public Camera fpsCam;
    public Image crossHair;
    public GameObject firePoint;
    public GameObject recoilPoint;
    private GameObject effectToSpawn;
    public List<GameObject> vfx = new List<GameObject>();

    private float timeToFire = 0;
    private Vector3 scale;
    private Vector3 direction;
    private Vector3 gunBeforeRecoil;
    private Quaternion rotation;
    private AudioSource ac;

    private void Start()
    {
        ac = GetComponent<AudioSource>();
        effectToSpawn = vfx[0];
        gunBeforeRecoil = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButton(0) || Input.GetAxis("Fire1") == 1) && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().fireRate;
            Shoot();
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, gunBeforeRecoil, recoil * Time.deltaTime);
        }
        
        
    }

    void Shoot()
    {
        RaycastHit hit;
        ac.Play();
        //Recoil
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, recoilPoint.transform.localPosition, backfire * Time.deltaTime);

       
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            RotateToHit(gameObject, hit.point);
            SpawnVFX();
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                //Debug.Log(hit.transform.name);
                target.TakeDamage(damage);
            }
        }
        else
        {
            rotation = fpsCam.transform.rotation;
            SpawnVFX();
        }

        

    }

    void RotateToHit(GameObject obj, Vector3 dest)
    {
        direction = dest - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);
    }

    void SpawnVFX()
    {
        GameObject vfx;

        if (firePoint != null)
        {
            vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            vfx.transform.rotation = rotation;
        }
        else
        {
            Debug.Log("No Fire Point");
        }
    }
}
