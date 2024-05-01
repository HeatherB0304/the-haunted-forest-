using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Raycast : MonoBehaviour
{
    public GameObject player;

    #region RaycastParams
    [Header("RAYCAST PARAMS")]
    public float raycastRange;
    public float shootRange;
    #endregion

    #region GunParams
    [Header("GUN PARAMS")]
    public int gunDamage;
    public int maxClipSize = 12;
    public int clip;
    public int ammo;
    public int startingAmmo = 100;

    //Physics
    public float bulletForce;
    #endregion

    #region GameObjects
    [Header("GAME OBJECTS")]
    public GameObject redKey;
    public GameObject heldKeyGFX;
    public GameObject greenKey;
    public GameObject greenheldKeyGFX;
    public GameObject redGate;
    public GameObject greenGate;
    public GameObject blueKey;
    public GameObject blueheldKey;
    public GameObject blueGate;
    public GameObject yellowKey;
    public GameObject yellowheldKey;
    public GameObject yellowGate;

    public GameObject gun;
    public GameObject gunGFX;
    public GameObject Bullet;

    public GameObject redDoor;
    public GameObject greenDoor;
    public GameObject blueDoor;
    public GameObject yellowDoor;

    #endregion

    #region Transforms
    [Header("TRANSFORMS")]
    public Transform bulletSpawn;
    #endregion

    #region Bools
    [Header("BOOLS")]
    bool hasRedKey = false;
    bool hasGun = false;
    bool gunOut = false;
    #endregion

    #region UI
    [Header("UI")]
    public Text clipTXT;
    public Text ammoTXT;
    public Text collectableCountText;
    public TextMeshProUGUI interactionText; 

    public GameObject clipUI;
    public GameObject ammoUI;
    public GameObject slashUI;
    public GameObject collectableCountTextUI;
    public GameObject completionImageUI;
    
    private int collectedCount = 0;
    private bool hasCollectedFirstItem = false;
    private bool allCollectablesCollected = false;
    
    public GameObject text;
    private float textTime;
    public float textTimeDelay;

    private bool startTextTime = false;
    #endregion

    #region AUDIO
    [Header("AUDIO")]
    public AudioSource gunshotSound;
    public AudioClip gunshotClip;
    public AudioClip jumpscareClip;
    #endregion

    #region ANIMATION
    [Header("ANIMATION")]
    [SerializeField] public Animator gunAnimator;
    [SerializeField] private string recoilAnim = "recoilAnim";
    #endregion

    #region JUMPSCARE
    [Header("JUMPSCARE")]
    //public GameObject JumpscareScreen;
    public AudioSource audioSource;
    public AudioClip scarySound;

    //private float jumpTime;
    //public float jumpTimeDelay;

    private bool startJumpTime = false;
    #endregion

    
    private int totalCollectables = 11; 
    private int bandageHealAmount = 5;
    private bool isInteractionTextVisible = false;

    
    void Start()
    {
        clip += maxClipSize;
        ammo += startingAmmo;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = gunshotClip;

        completionImageUI.gameObject.SetActive(false);
    }

    void Update()
    {
        clipTXT.text = clip.ToString();
        ammoTXT.text = ammo.ToString();
        //distFromRedKeyHole = Vector3.Distance(this.gameObject.transform.position, redKeyHole.position);

        if (Input.GetKeyDown("e"))
        {
            TryPickupCollectable();
        }

        if (collectedCount == totalCollectables && !allCollectablesCollected)
        {
            allCollectablesCollected = true;
            // Display the completion message UI
            completionImageUI.gameObject.SetActive(true);
            // Invoke a method to hide the completion message after 3 seconds
            Invoke("HideCompletionImageUI", 3f);
        }

        if (Input.GetKeyDown("e"))
        {
            CastRay();
        }

        if (Input.GetMouseButtonDown(0) && clip >= 1 && gunOut)
        {
            GunShootRaycast();
            GunShootPhysics();
            DirectlyPlayingAnimation();
            // Play the gunshot sound
            gunshotSound.Play();
            clip--;
        }

        if (Input.GetKeyDown("r") && ammo >= 1 && gunOut)
        {
            int reloadAmount = maxClipSize - clip;
            clip += reloadAmount;
            ammo -= reloadAmount;
        }

        if (clip <= 0 && ammo > -1 && gunOut)
        {
            if (ammo >= maxClipSize)
            {
                ammo -= maxClipSize;
                clip += maxClipSize;
            }
            else
            {
                clip += ammo;
                ammo -= ammo;
            }
        }

        if (ammo <= 0)
        {
            ammoTXT.text = 0.ToString();
        }
        GunControls();

        //ScareCount();

        if (Input.GetKeyDown("e"))
        {
            TryPickupCollectable();
            TryUseBandage();

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastRange))
            {   
                if (hit.transform.gameObject.CompareTag("InteractableObject"))
                {
                    // Display the interaction text
                    DisplayInteractionText("Game Saved, press 'L' to load from last checkpoint.");
                    // Start a coroutine to hide the text after 2 seconds
                    StartCoroutine(HideInteractionText(2f));
                }
            }
        }

        if (startTextTime)
        {
            textTime = textTime + 1f * Time.deltaTime;
        }

        if (textTime >= textTimeDelay)
        {
            startTextTime = false;
            textTime = 0;
            text.SetActive(false);
        }

    }

    void DisplayInteractionText(string text)
    {
        interactionText.text = text;
        interactionText.gameObject.SetActive(true);
        isInteractionTextVisible = true;
    }

    IEnumerator HideInteractionText(float delay)
    {
        yield return new WaitForSeconds(delay);
        interactionText.gameObject.SetActive(false);
        isInteractionTextVisible = false;
    }

    void DirectlyPlayingAnimation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gunAnimator.Play(recoilAnim);
        }
    }

    void PlayAnimationByStateInt()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gunAnimator.SetInteger(recoilAnim, 1);
        }
    }
    void CastRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastRange))
        {
            if (hit.transform.gameObject.tag == "redKey")
            {
                //ScareEm();
                Destroy(redKey);
                heldKeyGFX.SetActive(true);
                hasRedKey = true;
            }

            if (hit.transform.gameObject.tag == "redKeyHole" && hasRedKey)
            {
                Destroy(redDoor);
                heldKeyGFX.SetActive(false);
                Destroy(redGate);
            }

            if (hit.transform.gameObject.tag == "greenKey")
            {
                //ScareEm();
                Destroy(greenKey);
                greenheldKeyGFX.SetActive(true);
                hasRedKey = true;
            }

            if (hit.transform.gameObject.tag == "greenKeyHole" && hasRedKey)
            {
                Destroy(greenDoor);
                greenheldKeyGFX.SetActive(false);
                Destroy(greenGate);
            }

            if (hit.transform.gameObject.tag == "blueKey")
            {
                //ScareEm();
                Destroy(blueKey);
                blueheldKey.SetActive(true);
                hasRedKey = true;
            }

            if (hit.transform.gameObject.tag == "blueKeyHole" && hasRedKey)
            {
                Destroy(blueDoor);
                blueheldKey.SetActive(false);
                Destroy(blueGate);
            }

            if (hit.transform.gameObject.tag == "yellowKey")
            {
                //ScareEm();
                Destroy(yellowKey);
                yellowheldKey.SetActive(true);
                hasRedKey = true;
            }

            if (hit.transform.gameObject.tag == "yellowKeyHole" && hasRedKey)
            {
                Destroy(yellowDoor);
                yellowheldKey.SetActive(false);
                Destroy(yellowGate);
            }

            if (hit.transform.gameObject.tag == "Gun")
            {
                gunOut = true;
                hasGun = true;
                Destroy(gun);
                gunGFX.SetActive(true);
                text.SetActive(true);
                startTextTime = true;
            }
        }
    }
    
    void GunControls()
    {
        if (hasGun)
        {
            clipUI.SetActive(true);
            ammoUI.SetActive(true);
            slashUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (gunOut)
                {
                    gunGFX.SetActive(false);
                    gunOut = false;
                }
                else
                {
                    gunGFX.SetActive(true);
                    gunOut = true;
                }
            }
        }
        else
        {
            clipUI.SetActive(false);
            ammoUI.SetActive(false);
            slashUI.SetActive(false);
        }
    }

    void GunShootRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, shootRange))
        {
            if (hit.transform.gameObject.tag == "enemy" && hasGun && gunOut)
            {
                hit.transform.gameObject.GetComponent<EnemyHealth>().TakeDamage(gunDamage);
            }
        }

        StartCoroutine(PlayGunshotWithDelay(0.2f));
    }

    void GunShootPhysics()
    {
        if (hasGun && gunOut)
        {
            GameObject tempBullet = Instantiate(Bullet, bulletSpawn.position, Quaternion.identity);
            Rigidbody temprRB = tempBullet.GetComponent<Rigidbody>();
            temprRB.AddForce(bulletSpawn.forward * 10 * bulletForce);
        }

    }

    //void ScareEm()
    //{
        //Make scary sound
        //audioSource.clip = jumpscareClip;
        //audioSource.Play();

        //JumpscareScreen.SetActive(true); //will have scary image
        //startJumpTime = true;
    //}

    IEnumerator PlayGunshotWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.clip = gunshotClip;
        audioSource.Play();
    }

    //void ScareCount()
    //{
        //if (startJumpTime)
        //{
            //jumpTime = jumpTime + 1f * Time.deltaTime;
        //}

        //if (jumpTime >= jumpTimeDelay)
        //{
            //JumpscareScreen.SetActive(false);
            //startJumpTime = false;
            //jumpTime = 0;
        //}
    //}

    void TryPickupCollectable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastRange))
        {
            if (hit.transform.gameObject.CompareTag("Collectable"))
            {
                PickupCollectable(hit.transform.gameObject);
            }
            else if (hit.transform.gameObject.CompareTag("AmmoCollectable") && Input.GetKeyDown("e"))
            {
                // Call a method to handle picking up ammo
                PickupAmmo(hit.transform.gameObject);
            }
        }
    }

    void PickupCollectable(GameObject collectable)
    {
        // Implement logic to handle the specific type of collectable
        // For example, you might want to increase the player's score, health, or other parameters.
        // Here, I'll just deactivate the collectable.

        collectable.SetActive(false);

        collectedCount++;

        if (!hasCollectedFirstItem)
        {
            hasCollectedFirstItem = true;
            // Turn on the UI elements related to the collectable count after the first item is collected
            collectableCountTextUI.SetActive(true);
        }
        UpdateCollectableCountUI();
    }

    void UpdateCollectableCountUI()
    {
        // Update the UI Text to display the collected count
        collectableCountText.text = "0" + collectedCount.ToString();
    }

    void HideCompletionImageUI()
    {
        // Hide the completion message UI after 3 seconds
        completionImageUI.gameObject.SetActive(false);
    }

    void PickupAmmo(GameObject ammoCollectable)
    {
        // Implement logic to handle picking up ammo
        // For example, you might want to increase the player's ammo count.

        ammoCollectable.SetActive(false); // Deactivate the ammo collectable.

        // Add the ammo to the player's ammo count.
        int ammoToAdd = 12; // You can adjust this value based on your game's design.
        ammo += ammoToAdd;

        // Update the ammo UI text.
        ammoTXT.text = ammo.ToString();
    }

    void TryUseBandage()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastRange))
        {
            if (hit.transform.gameObject.CompareTag("Bandage"))
            {
                UseBandage(hit.transform.gameObject);
            }
        }
    }

    void UseBandage(GameObject bandage)
    {
        bandage.SetActive(false);

        // Check if the player GameObject reference is not null
        if (player != null)
        {   
            // Get the PlayerHealth component from the player GameObject
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            // Check if the PlayerHealth component is not null
            if (playerHealth != null)
            {
                // Use the HealPlayer method
                playerHealth.HealPlayer(bandageHealAmount);
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on the player GameObject.");
            }
        }
        else
        {
            Debug.LogError("Player GameObject reference not set in the Raycast script.");
        }
    }
}