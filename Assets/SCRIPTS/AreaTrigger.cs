using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTrigger : MonoBehaviour
{
    public GameObject uiPanel; // Reference to the UI panel you want to show/hide
    private bool isTargetInsideArea = false;

    private void Update()
    {
        if (isTargetInsideArea && Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            isTargetInsideArea = true;
            ShowUIPanel();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            isTargetInsideArea = false;
            HideUIPanel();
        }
    }

    private void ShowUIPanel()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
        }
    }

    private void HideUIPanel()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }

    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}