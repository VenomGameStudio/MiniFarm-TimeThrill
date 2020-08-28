using TMPro;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class SeedPickUp : MonoBehaviour
{
    public TextMeshProUGUI pickupText, pickedUpText, isEmpltyText;
    private bool pickup, handFree, inHand;
    private float timer;

    public GameManager gameManager;
    public SpriteRenderer spriteRenderer;
    public Sprite availableBeg;
    public Sprite brinjal, tomato, capsicum, reddish, potato, blueBerry, cherry;

    public GameObject bubble, handItem;
    public string itemName;

    private void Start()
    {
        inHand = false;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        pickupText.gameObject.SetActive(false);
        pickedUpText.gameObject.SetActive(false);
        isEmpltyText.gameObject.SetActive(false);

        bubble.gameObject.SetActive(false);
        handItem.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (pickup && handFree && CrossPlatformInputManager.GetButton("Action"))
        {
            FindObjectOfType<AudioManager>().Play("BagPickUp");
            
            gameManager.SeedManager(1);
            FindObjectOfType<PlayerManager>().PlantName(gameObject);
            pickupText.gameObject.SetActive(false);
            pickedUpText.gameObject.SetActive(true);
            spriteRenderer.sprite = null;
            inHand = true;
            gameManager.isBubbleActive = true;
        }
    }

    private void FixedUpdate()
    {
        if (inHand)
        {
            timer += Time.deltaTime;
            pickup = false;
            if (timer >= 10f)
            {
                spriteRenderer.sprite = availableBeg;
                timer = 0f;
                inHand = false;
                pickup = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bubble.gameObject.SetActive(true);
            PlayerManager manager = collision.GetComponent<PlayerManager>();
            if (manager)
            {
                pickup = manager.PickupItem(gameObject);
            }
            handFree = gameManager.HandFree();
            if (handFree)
            {
                if (!gameManager.isBubbleActive)
                {
                    FindObjectOfType<PlayerManager>().TableItem(gameObject);
                    itemName = FindObjectOfType<PlayerManager>().itemName;
                    handItem.gameObject.SetActive(true);
                    switch (itemName)
                    {
                        case "Brinjal":
                            handItem.GetComponent<Image>().sprite = brinjal;
                            break;
                        case "Tomato":
                            handItem.GetComponent<Image>().sprite = tomato;
                            break;
                        case "Capsicum":
                            handItem.GetComponent<Image>().sprite = capsicum;
                            break;
                        case "Reddish":
                            handItem.GetComponent<Image>().sprite = reddish;
                            break;
                        case "Potato":
                            handItem.GetComponent<Image>().sprite = potato;
                            break;
                        case "BlueBerry":
                            handItem.GetComponent<Image>().sprite = blueBerry;
                            break;
                        case "Cherry":
                            handItem.GetComponent<Image>().sprite = cherry;
                            break;
                        default:
                            handItem.GetComponent<Image>().sprite = null;
                            break;
                    }
                }
                pickupText.gameObject.SetActive(true);
            }
            else
            {
                isEmpltyText.gameObject.SetActive(true);            // already have seed in hand.
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickupText.gameObject.SetActive(false);
            pickedUpText.gameObject.SetActive(false);
            isEmpltyText.gameObject.SetActive(false);
            pickup = false;
            handFree = false;
            if (!gameManager.isBubbleActive)
            {
                bubble.gameObject.SetActive(false);
                handItem.gameObject.SetActive(false);
            }
        }
    }
}
