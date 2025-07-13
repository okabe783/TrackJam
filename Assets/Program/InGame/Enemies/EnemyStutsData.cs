using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Create EnemyStatusData")]

public class EnemyStutsData : ScriptableObject
{
    [Header("基本ステータス")]
    public int MAXHP; //最大HP
    public int ATK; //攻撃力　
    public float SPEED; //移動速度
    public float KnockBack; //のけぞり
    public int EXP; //経験値
    public int SCORE;  //加算するスコア

    [Header("敵のタイプ")]
    public EnemyType enemyType;

    [Header("ShortRange設定")]
    public float ATTACKRANGE;
    public float ATTACKINTERVAL;

    [Header("LongRange設定")]
    public float FireInterval;

    [Header("突進設定")]
    public float RUSHSPEED;  //突進速度
    public float RUSHINTERVAL;  //突進のインターバル

    [Header("アニメーター")]
    public AnimatorController ANIM;
    public Sprite SPR;

    public enum EnemyType
    {
        ShortRange,
        LongRange,
        FirstBoss,
        SecondBoss,
        LastBoss
    }
}
