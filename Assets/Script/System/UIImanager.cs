using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UImanager : MonoBehaviour
{
    [Header("UI element")]
    public TMP_Text killsCountText;      
    public TMP_Text waveCounterText;
    public TMP_Text ammoText;

    private int totalKills = 0;
    private int currentWave = 1;

    public void SetKillCount(int kills)
    { 
        totalKills = kills;
        if (killsCountText != null)
            killsCountText.text = "Kills: " + totalKills;
    }
    public void SetWave(int wave)
    {
        currentWave = wave;
        if (waveCounterText != null)
            waveCounterText.text = "Wave: " + currentWave;
    }

    public void SetAmmo(int current, int max)
    {
        if (ammoText != null)
            ammoText.text = current + " / " + max;
    }


}
