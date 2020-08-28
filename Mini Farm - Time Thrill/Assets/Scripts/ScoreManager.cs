using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public GameObject scoreDisplay;
    public int currentScore = 0;
    public TruckScript truck;

    public bool isLoaded;

    private void Start()
    {
        isLoaded = false;
        scoreDisplay.GetComponent<TextMeshProUGUI>().text = "00000";
    }

    private void FixedUpdate()
    {
        isLoaded = truck.scoreloaded;
        if (isLoaded)
        {
            currentScore += 10;
            if (currentScore < 10)
                scoreDisplay.GetComponent<TextMeshProUGUI>().text = "0000" + currentScore;
            else if (currentScore < 100)
                scoreDisplay.GetComponent<TextMeshProUGUI>().text = "000" + currentScore;
            else if (currentScore < 1000)
                scoreDisplay.GetComponent<TextMeshProUGUI>().text = "00" + currentScore;
            else if (currentScore < 10000)
                scoreDisplay.GetComponent<TextMeshProUGUI>().text = "0" + currentScore;
            else if (currentScore < 100000)
                scoreDisplay.GetComponent<TextMeshProUGUI>().text = "" + currentScore;
            isLoaded = false;
            truck.scoreloaded = false;
        }
    }
}
