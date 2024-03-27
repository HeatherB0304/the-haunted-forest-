using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    private Transform target;
    private float dist;
    public float enemySpeed;
    public float prox;

    private Rigidbody rb;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target").transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        dist = Vector3.Distance(target.position, transform.position);

        if (dist <= prox)
        {
            Debug.Log("test");
            transform.LookAt(target);
            rb.AddForce(transform.forward * enemySpeed);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, prox);

    }
}