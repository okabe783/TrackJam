using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 経験値オーブprefabにつけるスクリプト
/// </summary>
public class ExpOrb : MonoBehaviour
{
    // オーブがプレイヤーに当たったときに与える経験値量
    public float expAmount = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLevelManager playerLevel = other.GetComponent<PlayerLevelManager>();
            if (playerLevel != null)
            {
                playerLevel.AddExperience(expAmount);
            }

            // オーブを吸収（SEやエフェクトを後で追加してもOK）
            Destroy(gameObject);

            Debug.Log($"経験値オーブを吸収: {expAmount} EXP");
        }
    }
}
