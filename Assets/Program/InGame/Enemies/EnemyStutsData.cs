using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Create EnemyStatusData")]

public class EnemyStutsData : ScriptableObject
{
    [Header("基本ステータス")]
    public int MAXHP; //最大HP
    public int ATK; //攻撃力　
    public float SPEED; //移動速度
    public float NockBack; //のけぞり
    public float SPAN; //間隔
    public int EXP; //経験値

    [Header("敵のタイプ")]
    public EnemyType enemyType;

    [Header("LongRange設定")]
    public float FireInterval;

    public enum EnemyType
    {
        ShortRange,
        LongRange,
        FirstBoss,
        SecondBoss,
        LastBoss
    }
}
