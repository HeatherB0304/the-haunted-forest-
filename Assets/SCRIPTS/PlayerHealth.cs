using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth;
    public float playerMaxHealth;

    public TMP_Text textMeshPro;
    public Image healthBar;

    bool beingHit = false;
    public float damageDelay;
    private float time;

    public ScoreTracker scoreTrackerRef;

    public Canvas gameOverCanvas; // Reference to the canvas containing the game over UI
    public Image popUpImage; // Reference to the UI image for the pop-up
    public float popUpDuration = 3f; // Adjust the duration as needed

    private bool isGamePaused = false;
    private bool isGameOver = false;
    private bool isPopUpDisplayed = false;

    void Start()
    {
        playerHealth = playerMaxHealth;
        gameOverCanvas.gameObject.SetActive(false); // Hide the game over canvas initially
        popUpImage.gameObject.SetActive(false); // Hide the pop-up image initially

        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor initially
        Cursor.visible = false; // Hide the cursor initially
    }

    void Update()
    {
        if (isGamePaused)
        {
            return; // Skip the rest of the Update method while the game is paused
        }

        textMeshPro.text = playerHealth.ToString();

        if (playerHealth <= 0 && !isGameOver)
        {
            PauseGame();
            GameOver();
        }

        if (beingHit && time >= damageDelay)
        {
            playerHealth -= 1;
            healthBar.fillAmount = playerHealth / playerMaxHealth;
            time = 0;
        }

        if (beingHit)
        {
            time += 1f * Time.deltaTime;
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        isGamePaused = true;

        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Show the cursor
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        isGamePaused = false;

        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

   void GameOver()
    {
        scoreTrackerRef.SetHighScore();
        gameOverCanvas.gameObject.SetActive(true); // Show the game over canvas
        isGameOver = true;

        if (!isPopUpDisplayed)
        {
            // Start the coroutine to display and hide the pop-up image
            StartCoroutine(ShowAndHidePopUp());
        }
    }

    IEnumerator ShowAndHidePopUp()
    {
        isPopUpDisplayed = true; // Set the flag to true

        // Display the pop-up image
        popUpImage.gameObject.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(popUpDuration);

        // Hide the pop-up image
        popUpImage.gameObject.SetActive(false);

        isPopUpDisplayed = false; // Reset the flag
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            beingHit = true;
        }

        if (other.gameObject.CompareTag("ResetLvl"))
        {
            SceneManager.LoadScene("Whole Level");
        }
    }

    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            beingHit = false;
        }
    }

    public void StopHurtinMe()
    {
        beingHit = false;
    }

    public void HealPlayer(float amount)
    {
        playerHealth = Mathf.Min(playerHealth + amount, playerMaxHealth);
        healthBar.fillAmount = playerHealth / playerMaxHealth;
    }

    // Call this method from your UI button or other input to resume the game
    public void ResumeGameFromUI()
    {
        ResumeGame();
    }
}