using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementController
{

    public Entity entity;

    [HideInInspector]
    public int score = 0;

    CanonController canon;
    Vector3 respawn;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        entity = GetComponent<Entity>();
        canon = GetComponent<CanonController>();
        respawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = Vector2.zero;

        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        if(ver > 0) 
            dir = Vector2.up;

        if(ver < 0) 
            dir = Vector2.down;

        if(hor > 0) 
            dir = Vector2.right;

        if(hor < 0) 
            dir = Vector2.left;
            
        StartCoroutine(Move(dir));

        if(Input.GetButton("Jump") && canon != null)
            canon.Fire();
    }

    public void Respawn(){
        transform.position = respawn;
    }
}
