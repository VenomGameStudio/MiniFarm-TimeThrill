using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlantGrowScript : MonoBehaviour
{
    public TextMeshProUGUI isEmptyText, plantText, plantedText, alreadyPlantedText, cropHarvestText, cropInHandText;

    public bool handFree, planted, canPlant, isGrown, canHarvest;
    private SpriteRenderer spriteRenderer;
    public GameManager gameManager;
    public PlayerManager crop;

    public string plantGrown;

    public Sprite firstStage, secondStage, thirdStage;
    public Sprite brinjal, tomato, capsicum, reddish, potato, blueBerry, cherry;
    public float timer;

    public GameObject bubble, handItem;

    private string cropName;

    void Start()
    {
        isGrown = false;
        planted = false;
        canPlant = false;
        canHarvest = false;
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        cropHarvestText.gameObject.SetActive(false);
        cropInHandText.gameObject.SetActive(false);
        isEmptyText.gameObject.SetActive(false);
        plantText.gameObject.SetActive(false);
        plantedText.gameObject.SetActive(false);
        alreadyPlantedText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (canPlant && !handFree && CrossPlatformInputManager.GetButton("Action"))         // drop code
        {
            gameManager.SeedManager(0);
            FindObjectOfType<AudioManager>().Play("SeedPlanted");
            spriteRenderer.sprite = secondStage;
            plantText.gameObject.SetActive(false);
            plantedText.gameObject.SetActive(true);
            planted = true;
            canPlant = false;
            gameManager.isBubbleActive = false;
            bubble.gameObject.SetActive(false);
            handItem.gameObject.SetActive(false);
        }

        if (isGrown && handFree && canHarvest && CrossPlatformInputManager.GetButton("Action"))         //pick up code
        {
            crop.cropName = cropName;
            FindObjectOfType<AudioManager>().Play("SeedPlanted");
            gameManager.CropManager(1);                 // --> Hand is not empty now or crop picked in hand.
            spriteRenderer.sprite = firstStage;
            cropHarvestText.gameObject.SetActive(false);
            cropInHandText.gameObject.SetActive(true);  // crop wala msg.
            planted = false;
            isGrown = false;
            bubble.gameObject.SetActive(true);
            handItem.gameObject.SetActive(true);
            switch (cropName)
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
            gameManager.isBubbleActive = true;
        }
    }

    private void FixedUpdate()
    {
        if (planted)
        {
            timer += Time.deltaTime;
            if (timer >= 5f && timer < 15f)
            {
                spriteRenderer.sprite = thirdStage;
            }
            else if (timer >= 15f)
            {
                switch (plantGrown)
                {
                    case "Brinjal":
                        spriteRenderer.sprite = brinjal;
                        break;
                    case "Tomato":
                        spriteRenderer.sprite = tomato;
                        break;
                    case "Capsicum":
                        spriteRenderer.sprite = capsicum;
                        break;
                    case "Reddish":
                        spriteRenderer.sprite = reddish;
                        break;
                    case "Potato":
                        spriteRenderer.sprite = potato;
                        break;
                    case "BlueBerry":
                        spriteRenderer.sprite = blueBerry;
                        break;
                    case "Cherry":
                        spriteRenderer.sprite = cherry;
                        break;
                    default:
                        break;
                }
                timer = 0f;
                planted = false;
                isGrown = true;
                cropName = plantGrown;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager manager = collision.GetComponent<PlayerManager>();
            if (manager)
            {
                canPlant = manager.PlantArea(gameObject);
            }
            handFree = gameManager.HandFree();
            if (!handFree)
            {
                if (planted || isGrown)
                {
                    alreadyPlantedText.gameObject.SetActive(true);
                    handFree = true;
                }
                else
                {
                    plantGrown = manager.seedName;
                    plantText.gameObject.SetActive(true);
                }
            }
            else if (handFree && isGrown)
            {
                cropHarvestText.gameObject.SetActive(true);
                canHarvest = true;
            }
            else if (!planted)
                isEmptyText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cropHarvestText.gameObject.SetActive(false);
            cropInHandText.gameObject.SetActive(false);
            isEmptyText.gameObject.SetActive(false);
            plantText.gameObject.SetActive(false);
            plantedText.gameObject.SetActive(false);
            alreadyPlantedText.gameObject.SetActive(false);
            handFree = false;
            canPlant = false;
            canHarvest = false;
            if (!gameManager.isBubbleActive)
            {
                bubble.gameObject.SetActive(false);
                handItem.gameObject.SetActive(false);
            }
        }
    }
}
