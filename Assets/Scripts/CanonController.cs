using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    public float cooldown = 1;
    public float bulletSpeed = 1;
    public GameObject bulletPrefarb;
    public GameObject canon;
    
    float fireTime = .3f;
    bool isCooldown = false;

    private void Start()
    {
        canon.SetActive(false);
    }

    public void Fire()
    {
        if(isCooldown || bulletPrefarb == null) return;
        Instantiate(bulletPrefarb, canon.transform.position, canon.transform.rotation).GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        StartCoroutine(ShowFire());
    }

    IEnumerator ShowFire()
    {
        isCooldown = true;
        canon.SetActive(true);

        yield return new WaitForSeconds(fireTime);

        canon.SetActive(false);

        yield return new WaitForSeconds(cooldown - fireTime);
        
        isCooldown = false;
    }
}
