using TMPro;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TruckScript : MonoBehaviour
{
    public GameObject truck;
    public float timer;
    public Animator animator;

    public TextMeshProUGUI cropDropText, cropLoadedText, emptyHandText;
    public bool handFree, isTruck, canKeep, scoreloaded, timeloaded, isLeaving;
    public string objCropName, cropName;
    public string[] goal;

    public GameManager gameManager;

    private void Start()
    {
        handFree = false;
        isTruck = false;
        isLeaving = false;
        canKeep = false; scoreloaded = false; timeloaded = false;
        cropDropText.gameObject.SetActive(false);
        cropLoadedText.gameObject.SetActive(false);
        emptyHandText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isTruck && canKeep && !handFree && CrossPlatformInputManager.GetButton("Action"))
        {
           // foreach (string s in gameManager.goals.)
            //{

            //}

            FindObjectOfType<AudioManager>().Play("BagDrop");
    
            gameManager.CropManager(0);
            cropDropText.gameObject.SetActive(false);
            cropLoadedText.gameObject.SetActive(true);
            handFree = true;
            scoreloaded = true;
            timeloaded = true;
            gameManager.isBubbleActive = false;
        }

    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer <= 79f)
        {
            isLeaving = false;
        }
        if (timer >= 40f)
        {
            animator.SetBool("stay", false);
            animator.SetBool("canCome", true);
            animator.SetBool("canGo", false);
        }
        if (timer >= 80f)
        {
            FindObjectOfType<AudioManager>().Play("TruckRev");
            animator.SetBool("canCome", false);
            animator.SetBool("canGo", true);
            animator.SetBool("stay", true);
            timer = 0;
            isLeaving = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager manager = collision.GetComponent<PlayerManager>();
            if (manager)
            {
                isTruck = manager.DropArea(gameObject);
            }
            handFree = gameManager.HandFree();
            if (!handFree)
            {
                cropName = manager.cropName;
                if (gameManager.seeds == 0 && gameManager.crop == 1)
                {
                    canKeep = true;
                    cropDropText.gameObject.SetActive(true);
                }
            }
            else if (handFree || !canKeep)
                emptyHandText.gameObject.SetActive(true);
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cropDropText.gameObject.SetActive(false);
        cropLoadedText.gameObject.SetActive(false);
        emptyHandText.gameObject.SetActive(false);
        isTruck = false;
        handFree = false;
        canKeep = false;
    }
}
