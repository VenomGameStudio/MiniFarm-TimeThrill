using TMPro;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DustbinScript : MonoBehaviour
{
    public TextMeshProUGUI dropText, dropedText, emptyHandText;
    private bool handFree, throwAway;
    public GameManager gameManager;

    private void Start()
    {
        dropText.gameObject.SetActive(false);
        dropedText.gameObject.SetActive(false);
        emptyHandText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!handFree && throwAway && CrossPlatformInputManager.GetButton("Action"))
        {
            gameManager.SeedManager(0);
            gameManager.CropManager(0);
            dropText.gameObject.SetActive(false);
            dropedText.gameObject.SetActive(true);          // --> item droped text
            handFree = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager manager = collision.GetComponent<PlayerManager>();
            if (manager)
            {
                throwAway = manager.DropArea(gameObject);
            }
            handFree = gameManager.HandFree();
            if (!handFree)
                dropText.gameObject.SetActive(true);            // --> e to drop text
            else
                emptyHandText.gameObject.SetActive(true);       // --> empty hand text
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dropText.gameObject.SetActive(false);
            dropedText.gameObject.SetActive(false);
            emptyHandText.gameObject.SetActive(false);
            throwAway = false;
            handFree = false;
        }
    }
}
