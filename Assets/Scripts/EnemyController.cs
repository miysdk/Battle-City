using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MovementController
{
    public int scoreGain = 200;
    public LayerMask wallLayer;

    Vector2 dir = Vector2.zero;
    List<Vector2> dirs = new List<Vector2>();
    CanonController canon;
    
    protected override void Start()
    {
        base.Start();
        RandomDir();
        canon = GetComponent<CanonController>();
        Invoke("Fire", Random.Range(1f, 5f));
    }

    private void Update()
    {
        StartCoroutine(Move(dir));
    }

    void RandomDir()
    {
        CancelInvoke("RandomDir");

        if(!Physics2D.Linecast(transform.position, transform.position += new Vector3(0, 1, 0), wallLayer))
            dirs.Add(Vector2.up);

        if(!Physics2D.Linecast(transform.position, transform.position += new Vector3(0, -1, 0), wallLayer))
            dirs.Add(Vector2.down);

        if(!Physics2D.Linecast(transform.position, transform.position += new Vector3(1, 0, 0), wallLayer))
            dirs.Add(Vector2.right);

        if(!Physics2D.Linecast(transform.position, transform.position += new Vector3(-1, 0, 0), wallLayer))
            dirs.Add(Vector2.left);

        if(dirs.Count != 0)
            dir = dirs[Random.Range(0, dirs.Count)];

        Invoke("RandomDir", 3);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RandomDir();
    }

    void Fire()
    {
        if(canon == null) 
            return;
        
        canon.Fire();
        Invoke("Fire", Random.Range(1f, 5f));
    }
}
