using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed = 5;
    
    Rigidbody2D rb;

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public IEnumerator Move(Vector2 dir){
        if(dir == Vector2.up) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(dir == Vector2.down) {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if(dir == Vector2.right) {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if(dir == Vector2.left) {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        rb.velocity = dir * speed;
        yield return new WaitForFixedUpdate();
    }
}
