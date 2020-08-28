using System.Collections;
using TMPro;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DrinkWater : MonoBehaviour
{
    public TextMeshProUGUI pickUpText;
    private bool pickUpAllowed;
    public PlayerManager manager;
    private bool drunk;

    private void Start()
    {
        drunk = false;
        pickUpText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (pickUpAllowed && CrossPlatformInputManager.GetButton("Action"))
        {
            FindObjectOfType<AudioManager>().Play("WaterDrinking");

            manager.Drink(1);
            drunk = true;
            StartCoroutine(PlayAnimation());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpText.gameObject.SetActive(true);
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpText.gameObject.SetActive(false);
            manager.Drink(0);
            pickUpAllowed = false;
        }
    }

    IEnumerator PlayAnimation()
    {
        if (drunk)
        {
            yield return new WaitForSeconds(0.80f);
            manager.animator.SetBool("isDrinking", false);
            drunk = false;
        }
    }
}