using UnityEngine;
using UnityEngine.UI;

public class AreaTriggerStory : MonoBehaviour
{
    public Text displayText;
    public Collider triggerCollider;
    public float displayTime = 5f; // Adjust the display time as needed

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayText();
            Invoke("DestroyTextAndCollider", displayTime);
        }
    }

    private void DisplayText()
    {
        displayText.gameObject.SetActive(true);
    }

    private void DestroyTextAndCollider()
    {
        displayText.gameObject.SetActive(false);
        Destroy(triggerCollider.gameObject);
        Destroy(gameObject); // Destroy the script component itself if necessary
    }
}