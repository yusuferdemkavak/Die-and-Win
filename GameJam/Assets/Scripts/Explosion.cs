using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject blood;
    public GameObject player;
    
    public InGameUIScript ui;

    public Vector2 startPos;

    public float explosionRadius;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Explode();
            Die();
        }
    }

    public void Explode()
    {
        if(Vector2.Distance(transform.position, enemy1.transform.position) < explosionRadius)
        {
            enemy1.GetComponent<EnemyAI>().isDead = true;
        }
        
        if (Vector2.Distance(transform.position, enemy2.transform.position) < explosionRadius)
        {
            enemy2.GetComponent<EnemyAI>().isDead = true;
        }
    }

    public void Die()
    {
        Instantiate(blood, transform.position, Quaternion.identity);
        ui.isOver = true;
        Destroy(gameObject);
    }
}
