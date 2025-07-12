using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    public Slider hpSlider; // Unityのインスペクタからセットする
    public PlayerController playerController;

    private void Start()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerControllerが設定されていません！");
        }

        if (hpSlider != null && playerController != null)
        {
            hpSlider.maxValue = playerController._statusData.Hp;
        }
    }

    private void Update()
    {
        if (hpSlider != null && playerController != null)
        {
            hpSlider.value = playerController._statusData.Hp;
        }
    }
}
