using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int seeds, crop;
    public bool isBubbleActive;

    private void Start()
    {
        isBubbleActive = false;
        seeds = 0;
        crop = 0;
    }

    public void SeedManager (int num)
    {
        seeds = num;
    }

    public void CropManager(int num)
    {
        crop = num;
    }

    public bool HandFree()
    {
        if (seeds == 0 && crop == 0)
            return true;
        return false;
    }


}
