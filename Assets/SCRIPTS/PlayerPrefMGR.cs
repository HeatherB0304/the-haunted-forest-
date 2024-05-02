using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefMGR : MonoBehaviour
{
    public Transform defaultSpawn;
    public bool resetPrefsOnStart;

    // Start is called before the first frame update
    void Awake()
    {
        if (resetPrefsOnStart)
        {
            ResetPrefs();
        }
    }

    void ResetPrefs()
    {
        PlayerPrefs.SetInt("hasGun", 0);
        PlayerPrefs.SetFloat("xPos", defaultSpawn.position.x);
        PlayerPrefs.SetFloat("yPos", defaultSpawn.position.y);
        PlayerPrefs.SetFloat("zPos", defaultSpawn.position.z);
    }
}
