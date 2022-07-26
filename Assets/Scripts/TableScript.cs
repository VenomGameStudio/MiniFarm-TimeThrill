using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class TableScript : MonoBehaviour
{
    public GameObject table;
    public SpriteRenderer spriteRenderer;
    public Sprite brinjal, tomato, capsicum, reddish, potato, blueBerry, cherry;
    public TextMeshProUGUI canKeepText, keptText, emptyHandText, tableFullText, cropPickUpText, cropPickedText;
    public GameManager gameManager;

    public bool keepArea, handFree, tableFull, canKeep, canPick;

    public GameObject bubble, handItem;
    private string cropName;

    private void Start()
    {
        tableFull = false;
        canKeep = false; canPick = false;
        spriteRenderer.sprite = null;
        canKeepText.gameObject.SetActive(false);
        keptText.gameObject.SetActive(false);
        emptyHandText.gameObject.SetActive(false);
        tableFullText.gameObject.SetActive(false);
        cropPickUpText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (keepArea && !handFree && !tableFull && canKeep && CrossPlatformInputManager.GetButton("Action"))        //drop code
        {
            FindObjectOfType<AudioManager>().Play("BagDrop");

            gameManager.CropManager(0);
            canKeepText.gameObject.SetActive(false);
            keptText.gameObject.SetActive(true);
            handFree = true;
            tableFull = true;
            switch (cropName)
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
            bubble.gameObject.SetActive(false);
            handItem.gameObject.SetActive(false);
            gameManager.isBubbleActive = false;
        }

        else if (keepArea && handFree && tableFull && CrossPlatformInputManager.GetButton("Action"))        //pickup code
        {
            FindObjectOfType<AudioManager>().Play("BagPickUp");

            gameManager.CropManager(1);
            cropPickUpText.gameObject.SetActive(false);
            cropPickedText.gameObject.SetActive(true);
            spriteRenderer.sprite = null;
            tableFull = false;
            handFree = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager manager = collision.GetComponent<PlayerManager>();
            if (manager)
            {
                keepArea = manager.DropArea(gameObject);
            }
            handFree = gameManager.HandFree();
            if (!handFree)
            {
                if (tableFull)
                    tableFullText.gameObject.SetActive(true);
                else
                {
                    cropName = manager.cropName;
                    if (gameManager.crop == 1 && gameManager.seeds == 0)
                    {
                        canKeep = true;
                        canKeepText.gameObject.SetActive(true);
                    }
                }
            }
            else if (handFree && tableFull)
            {
                canPick = true;
                cropPickUpText.gameObject.SetActive(true);
            }
            else if (handFree || !canKeep)
                emptyHandText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canKeepText.gameObject.SetActive(false);
            keptText.gameObject.SetActive(false);
            emptyHandText.gameObject.SetActive(false);
            tableFullText.gameObject.SetActive(false);
            cropPickUpText.gameObject.SetActive(false);
            cropPickedText.gameObject.SetActive(false);
            keepArea = false;
            handFree = false;
            canKeep = false; canPick = false;
            if (!gameManager.isBubbleActive)
            {
                bubble.gameObject.SetActive(false);
                handItem.gameObject.SetActive(false);
            }
        }
    }
}
