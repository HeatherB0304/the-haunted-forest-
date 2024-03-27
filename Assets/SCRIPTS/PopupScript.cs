using UnityEngine;
using UnityEngine.UI;

public class PopupScript : MonoBehaviour
{
    public float popupDuration = 5f; // Set the duration for how long the popup should be visible
    private float timer;
    private bool popupActive = true;

    public Text popupText; // Reference to the Text component in the UI

    void Start()
    {
        // Hide the popup initially
        SetPopupActive(true);
    }

    void Update()
    {
        if (popupActive)
        {
            // Update the timer
            timer += Time.deltaTime;

            // Check if the timer has exceeded the popup duration
            if (timer >= popupDuration)
            {
                // Hide the popup after the specified duration
                SetPopupActive(false);
            }
        }
    }

    void SetPopupActive(bool active)
    {
        // Set the GameObject and its children (Text in this case) active/inactive
        gameObject.SetActive(active);
        popupText.enabled = active;

        // Reset the timer when the popup becomes active
        if (active)
        {
            timer = 0f;
        }

        // Update the popupActive flag
        popupActive = active;
    }
}
