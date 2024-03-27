using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDestroyScript : MonoBehaviour
{
    public GameObject bossEnemy;  // Reference to the boss enemy GameObject
    public GameObject bossUIElement;  // Reference to the boss UI element
    public GameObject achievementUIElement;  // Reference to the achievement UI element

    public float achievementDisplayTime = 5f;  // Adjust the time the achievement UI is displayed

    private void Start()
    {
        // Ensure the UI elements are initially disabled
        SetUIElementActive(bossUIElement, false);
        SetUIElementActive(achievementUIElement, false);
    }

    private void Update()
    {
        // Check if the boss enemy is destroyed
        if (bossEnemy == null)
        {
            // Boss is destroyed, enable the boss UI element
            SetUIElementActive(bossUIElement, true);

            // Check if the achievement UI element is not active
            if (!achievementUIElement.activeSelf)
            {
                // Enable the achievement UI element
                SetUIElementActive(achievementUIElement, true);

                // Optionally, you can perform additional actions related to the achievement here

                // Start a coroutine to disable the achievement UI element after a set time
                StartCoroutine(DisableAchievementUI());
            }

            // Disable this script to prevent further updates
            enabled = false;
        }
    }

    private void SetUIElementActive(GameObject uiElement, bool isActive)
    {
        // Toggle the visibility of the UI element
        if (uiElement != null)
        {
            uiElement.SetActive(isActive);
        }
        else
        {
            Debug.LogError("UI element not assigned!");
        }
    }

    private IEnumerator DisableAchievementUI()
    {
        // Wait for the specified time before disabling the achievement UI element
        yield return new WaitForSeconds(achievementDisplayTime);

        // Disable the achievement UI element
        SetUIElementActive(achievementUIElement, false);
    }
}