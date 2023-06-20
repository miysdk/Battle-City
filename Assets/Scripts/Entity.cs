using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int lives = 3;
    
    PlayerController pc;
    UIController uic;
    GameController gc;

    private void Start() {
        pc = gameObject.GetComponent<PlayerController>();
        uic = FindObjectOfType<UIController>();
        gc = FindObjectOfType<GameController>();
    }

    public void StartTakingDamage()
    {
        StartCoroutine(TakeDamage());
    }

    IEnumerator TakeDamage()
    {
        SpriteRenderer sr = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Color c = sr.color;
        sr.color = Color.red;

        lives --;

        if(pc is not null)
        {
            pc.Respawn();
        }

        if(lives == 0) 
        {
            if(pc is null)
            {
                gc.spawnedEnemies.Remove(gameObject);
                FindObjectOfType<PlayerController>().score += GetComponent<EnemyController>().scoreGain;
            }

            Destroy(gameObject);
            yield break;
        }

        yield return new WaitForSeconds(.3f);
        sr.color = c;
    }
}
