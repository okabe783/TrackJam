using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    public Slider expSlider;
    public Text levelText;

    public PlayerLevelManager playerLevelManager;

    void Update()
    {
        if (playerLevelManager != null)
        {
            expSlider.maxValue = playerLevelManager.expToLevelUp;
            expSlider.value = playerLevelManager.currentExp;

            levelText.text = "Lv " + playerLevelManager.level;
        }
    }
}
