using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    public Transform player;        // プレイヤーのTransform
    public Vector3 offset = new Vector3(0, 1.5f, 0);
    public Slider hpSlider;
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

    void LateUpdate()
    {
        //追従
        transform.position = player.position + offset;

        //正面向きに
        transform.forward = Camera.main.transform.forward;

    }

    private void Update()
    {
        if (hpSlider != null && playerController != null)
        {
            hpSlider.value = playerController._currentHp;
        }

    }
}

