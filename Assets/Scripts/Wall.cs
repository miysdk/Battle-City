using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public List<Sprite> stages;
    public bool indestructable = false;

    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if(indestructable) return;
        stages.RemoveAt(0);
        if(stages.Count == 0) {
            Destroy(gameObject);
            return;
        }
        sr.sprite = stages[0];
    }
}
