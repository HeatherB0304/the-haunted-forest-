using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TriggerUI : MonoBehaviour
{
    public Text displayText;
    public float displayTime = 5f; // Set the duration in seconds

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            // Display the text
            displayText.gameObject.SetActive(true);

            // Start a coroutine to hide the text after a delay
            StartCoroutine(HideTextAfterDelay());
        }
    }

    IEnumerator HideTextAfterDelay()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(displayTime);

        // Hide the text
        displayText.gameObject.SetActive(false);

        // Destroy the UI element or the entire script if needed
        Destroy(gameObject);
    }
}