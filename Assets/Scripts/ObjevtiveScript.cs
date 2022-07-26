using UnityEngine;
using UnityEngine.UI;

public class ObjevtiveScript : MonoBehaviour
{
    public int random;
    public Sprite[] objectives;
    public string cropName;
    private bool left;

    private void Start()
    {
        left = false;
        Change();
    }

    private void Update()
    {
        left = FindObjectOfType<TruckScript>().isLeaving;
        if (left)
        {
            Change();
            left = false;
        }
    }

    private void Change()
    {
        random = Random.Range(0, objectives.Length);
        GetComponent<Image>().sprite = objectives[random];
        PlantName(random);
    }

    private void PlantName(int num)
    {
        switch (num)
        {
            case 0:
                cropName = "Reddish";
                break;
            case 1:
                cropName = "Brinjal";
                break;
            case 2:
                cropName = "Tomato";
                break;
            case 3:
                cropName = "Capsicum";
                break;
            case 4:
                cropName = "Cherry";
                break;
            case 5:
                cropName = "BlueBerry";
                break;
            case 6:
                cropName = "Potato";
                break;
            default:
                break;
        }
    }
}
