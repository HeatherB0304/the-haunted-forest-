using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareEnemyManager : MonoBehaviour
{
    private Transform target;
    public float prox;
    private float dist;

    public GameObject enemy;
    public AudioClip yourAudioClip; // Assign your audio clip in the Unity Editor

    private AudioSource audioSource;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target").transform;

        // Assuming the AudioSource is attached to the same GameObject as this script
        audioSource = GetComponent<AudioSource>();

        // Ensure the audio clip is assigned in the Unity Editor
        if (yourAudioClip == null)
        {
            Debug.LogError("AudioClip not assigned! Please assign it in the Unity Editor.");
        }
    }

    void Update()
    {
        dist = Vector3.Distance(target.position, transform.position);

        if (dist <= prox)
        {
            // Activate the enemy
            enemy.SetActive(true);

            // Check if AudioSource and AudioClip are assigned
            if (audioSource != null && yourAudioClip != null)
            {
                // Play the audio clip
                audioSource.PlayOneShot(yourAudioClip);
            }
            else
            {
                Debug.LogError("AudioSource or AudioClip is not assigned!");
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, prox);
    }
}
