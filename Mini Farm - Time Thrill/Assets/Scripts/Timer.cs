using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public PlayerManager manager;
    public GameObject timerDisplay;
    public TruckScript truck;
    private int minLeft = 4, secLeft = 60;
    private bool takingAway = false, noTime = false;
    public bool loaded;

    void Start()
    {
        loaded = truck.timeloaded;
        timerDisplay.GetComponent<TextMeshProUGUI>().text = "05:00";
    }

    void Update()
    {
        loaded = truck.timeloaded;
        if (takingAway == false && minLeft >= 0 && secLeft > 0)
        {
            StartCoroutine(TimeTake());
        }
    }

    IEnumerator TimeTake()
    {
        takingAway = true;
        if (loaded)
        {
            secLeft += 10; 
            loaded = false;
            truck.timeloaded = false;
        }
        yield return new WaitForSeconds(1);
        secLeft -= 1;
        if (minLeft <= 0 && secLeft <= 0)
        {
            manager.TimeLeft();
            noTime = true;
        }
        if (secLeft == 0)
        {
            minLeft -= 1;
            secLeft = 59;
        }
        if (secLeft < 10)
            timerDisplay.GetComponent<TextMeshProUGUI>().text = "0" + minLeft + ":0" + secLeft;
        else if (noTime)
            timerDisplay.GetComponent<TextMeshProUGUI>().text = "Game Over.";
        else
            timerDisplay.GetComponent<TextMeshProUGUI>().text = "0" + minLeft + ":" + secLeft;
        takingAway = false;
    }
}
