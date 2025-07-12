using UnityEngine;

[CreateAssetMenu(menuName = "BossData")]
public class BossSpawnData : ScriptableObject
{
    public GameObject BossPrefab;
    // この秒数が経過したら出現
    public float SpawnTime; 
    // 出現位置の調整
    public Vector2 SpawnOffset = new(0, 0);
    public EnemyStutsData StutsData;
}
