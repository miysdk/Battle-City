using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Tank"))
        {
            other.GetComponent<Entity>().StartTakingDamage();
            Destroy(gameObject);
        }
    }
}
