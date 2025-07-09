using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyTimer : MonoBehaviour
{
    [SerializeField,Header("ボスのデータ")] CharacterData[] _enemydeta;
    [SerializeField] float _gametimer = 15f;
    [SerializeField,Header("ファーストの敵の変化時間")] float _firstweve = 10;
    [SerializeField] float _secondweve = 20;  //2回目のウェーブ変化

    //敵のウェーブを時間ごとで変えていきたい　ボスの出現をここに書くかどうか

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_gametimer < 0)
        {
            _gametimer -= Time.deltaTime;
            BossSupon();

        }
        if (_gametimer ==_firstweve)
        {
            EnemyUptime();

        }

    }
    void EnemyUptime()
    {
        //せなからもらうコードでウェーブの処理を書く
    }
    void BossSupon()
    {
        //ここにボスの出す処理
    }
}
