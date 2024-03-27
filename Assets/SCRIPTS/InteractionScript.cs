using UnityEngine;
using UnityEngine.UI;

public class InteractionScript : MonoBehaviour
{
    public Text popupText;  // Reference to the UI text element
    public GameObject pickupObject;  // Reference to the object the player can pick up

    private bool inTriggerArea = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            inTriggerArea = true;
            popupText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            inTriggerArea = false;
            popupText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (inTriggerArea && Input.GetKeyDown(KeyCode.E))  // Assuming "E" is the key to pick up the object
        {
            // Perform pickup logic
            PickupObject();
        }
    }

    private void PickupObject()
    {
        // Implement your pickup logic here

        // Assuming you want to destroy the pickup object
        Destroy(pickupObject);

        // Hide the UI text element
        Destroy(popupText);

        // You can add any additional logic for the pickup event here
    }
}
