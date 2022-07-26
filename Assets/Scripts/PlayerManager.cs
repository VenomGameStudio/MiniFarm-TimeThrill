using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Animator animator;
    public int healthFull;
    public string seedName, cropName, itemName;
    public bool noTimeLeft;


    private void Start()
    {
        healthFull = 0;
        noTimeLeft = false;
    }

    public bool PickupItem(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Seeds":
                return true;
            default:
                return false;
        }
    }

    public void PlantName(GameObject obj)
    {
        switch (obj.name)
        {
            case "BrinjalSeed":
                seedName = "Brinjal";
                break;
            case "TomatoSeed":
                seedName = "Tomato";
                break;
            case "CapsicumSeed":
                seedName = "Capsicum";
                break;
            case "ReddishSeed":
                seedName = "Reddish";
                break;
            case "PotatoSeed":
                seedName = "Potato";
                break;
            case "BlueBerrySeed":
                seedName = "BlueBerry";
                break;
            case "CherrySeed":
                seedName = "Cherry";
                break;
            default:
                break;
        }
    }

    public void TableItem(GameObject obj)
    {
        switch (obj.name)
        {
            case "BrinjalSeed":
                itemName = "Brinjal";
                break;
            case "TomatoSeed":
                itemName = "Tomato";
                break;
            case "CapsicumSeed":
                itemName = "Capsicum";
                break;
            case "ReddishSeed":
                itemName = "Reddish";
                break;
            case "PotatoSeed":
                itemName = "Potato";
                break;
            case "BlueBerrySeed":
                itemName = "BlueBerry";
                break;
            case "CherrySeed":
                itemName = "Cherry";
                break;
            default:
                break;
        }
    }

    public bool DropArea(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Dustbin":
                return true;
            case "DropTable":
                return true;
            case "Truck":
                return true;
            default:
                return false;
        }
    }

    public bool PlantArea(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Soil":
                return true;
            default:
                return false;
        }
    }

    public void Drink(int allowed)
    {
        if (allowed == 1)
        {
            animator.SetBool("isDrinking", true);
            allowed = 0;
            healthFull = 1;
        }
        else
            animator.SetBool("isDrinking", false);
    }

    public void TimeLeft()
    {
        noTimeLeft = true;
    }
}
