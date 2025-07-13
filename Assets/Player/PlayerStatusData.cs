using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Create StatusData")]
public class StatusData : ScriptableObject
{
    public Sprite Sprite;
    public int Hp;//HP
    public float MoveSpeed; //移動速度
    public float Atk;
}
