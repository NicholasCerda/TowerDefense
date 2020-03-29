using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float speed = 5;
    public float lifeTime = 5;
    private Rigidbody rb;
    public GameObject target;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }
    void FixedUpdate()
    {
        if (target != null) {
            rb.velocity = (target.transform.position - transform.position).normalized * speed;
        }
        else
        {
            Destroy(gameObject, .5f);
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.parent.tag == "Enemy")
        {
            collision.transform.parent.GetComponent<Enemy>().takeDamage(3);
            Destroy(gameObject);
        }
    }
}
