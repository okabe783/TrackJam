using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpDropper : MonoBehaviour
{
    [Header("オーブの種類")]
    public GameObject smallExpOrbPrefab; // 小オーブ (例: 3EXP)
    public GameObject largeExpOrbPrefab; // 大オーブ (例: 10EXP)

    [Header("ドロップ確率設定")]
    [Range(0f, 1f)] public float smallOrbChance = 0.8f; // 80% 小、20% 大

    [Header("ドロップ数")]
    public int dropCount = 1; // 複数落とす場合は変更

    public void DropExpOrbs()
    {
        for (int i = 0; i < dropCount; i++)
        {
            float rand = Random.value;

            GameObject orbPrefab = (rand < smallOrbChance) ? smallExpOrbPrefab : largeExpOrbPrefab;

            if (orbPrefab != null)
            {
                Vector2 dropPos = (Vector2)transform.position + Random.insideUnitCircle * 0.5f; // ちょっとばらける
                Instantiate(orbPrefab, dropPos, Quaternion.identity);
            }
        }
    }
}
