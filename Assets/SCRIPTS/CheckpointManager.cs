using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public Transform defaultSpawn;
    public static Transform currentCheckpoint;
    public GameObject player;

    public bool isPaused = false;

    public GameObject pauseMenu;

    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        LoadCheckpoint();
        ResetCheckpoint();
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        { 
            ResetCheckpoint();
        }
        
        if (Input.GetKeyDown("l"))
        {
            LoadCheckpoint();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            if(!isPaused)
            {
                PauseMenuOn();
                isPaused = true;
            }
        }
    }

    void LoadCheckpoint()
    {
        Vector3 tempSpawn = new Vector3(PlayerPrefs.GetFloat("xPos"), PlayerPrefs.GetFloat("yPos"), PlayerPrefs.GetFloat("zPos"));
        player.transform.position = tempSpawn;
    }

    void ResetCheckpoint()
    {
        PlayerPrefs.SetFloat("xPos", defaultSpawn.position.x);
        PlayerPrefs.SetFloat("yPos", defaultSpawn.position.y);
        PlayerPrefs.SetFloat("zPos", defaultSpawn.position.z);

    }

    void PauseMenuOn()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PauseMennuOff()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
