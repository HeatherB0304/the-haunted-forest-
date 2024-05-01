using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZmbieAttack : MonoBehaviour
{
    public Transform player; // Assign the player GameObject in the Inspector
    public float triggerRange = 5f; // Adjust the trigger range in the Inspector
    private Animator anim;

    void Start()
    {
        // Assign the Animation component if not assigned in the Inspector
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the player is within the trigger range
        if (Vector3.Distance(transform.position, player.position) <= triggerRange && !anim.GetBool("AttackRange"))
        {
            // Trigger the animation
            anim.SetBool("AttackRange", true);
        }
        else if (Vector3.Distance(transform.position, player.position) > triggerRange && anim.GetBool("AttackRange"))
        {
            anim.SetBool("AttackRange", false);
        }
    }

    // Draw the trigger range in blue wireframe when selected in the Unity Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, triggerRange);
    }
}
