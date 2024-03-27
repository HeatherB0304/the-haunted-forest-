using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCollisions : MonoBehaviour
{
    public CheckpointManager CMRef;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("C1"))
        {
            PlayerPrefs.SetFloat("xPos", other.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("yPos", other.gameObject.transform.position.y);
            PlayerPrefs.SetFloat("zPos", other.gameObject.transform.position.z);
        }
        else if (other.gameObject.CompareTag("C2"))
        {
            PlayerPrefs.SetFloat("xPos", other.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("yPos", other.gameObject.transform.position.y);
            PlayerPrefs.SetFloat("zPos", other.gameObject.transform.position.z);


        }
        else if (other.gameObject.CompareTag("C3"))
        {
            PlayerPrefs.SetFloat("xPos", other.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("yPos", other.gameObject.transform.position.y);
            PlayerPrefs.SetFloat("zPos", other.gameObject.transform.position.z);

        }
        else if (other.gameObject.CompareTag("C4"))
        {
            PlayerPrefs.SetFloat("xPos", other.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("yPos", other.gameObject.transform.position.y);
            PlayerPrefs.SetFloat("zPos", other.gameObject.transform.position.z);

        }
    }
}
