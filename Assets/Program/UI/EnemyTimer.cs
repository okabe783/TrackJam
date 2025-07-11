using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyTimer : MonoBehaviour
{   //スポーンさせる数を設定する
    //enemyCount
    //最短のスポーン間隔
    //minRandomDelay;
    //最長のスポーン間隔
    // maxRandomDelay;


    [SerializeField, Header("ファーストの敵の変化時間")] float _firstWeve = 10f;
    [SerializeField] float _secondWave = 15f;  //2回目のウェーブ変化
    [SerializeField] float _finalWave = 30f;
    [SerializeField, Header("ウェーブの変更する数字")] float _firstChange = 3;
    [SerializeField] int _changeCount = 3; //最大数の変更この値を足していきたい


    Spawner _spawner;
    bool _waveup1 = false;
    bool _waveup2 = false; 
    bool _waveup3 = false;


    float _gametimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        _spawner = GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gametimer < 600)
        {
            _gametimer += Time.deltaTime;

        }
        if (_gametimer > _firstWeve && _waveup1 == false)
        {
            EnemyUptime();
        }
        else if (_gametimer > _secondWave && _waveup2 == false)
        {
            UptimeenemyCount();
        }
        else if (_gametimer > _finalWave && _waveup3 == false)
        {
            FinalChenge();
        }

    }
    void EnemyUptime()
    {
        Debug.Log("一回目の変化");
        _spawner.minRandomDelay = 1f;
        _spawner.maxRandomDelay = _firstChange;

        _waveup1 = true;
    }
    void UptimeenemyCount()
    {
        _spawner.enemyCount += _changeCount;
        _waveup2 = true;
    }
    void FinalChenge()
    {
        _spawner.enemyCount += _changeCount;

    }
}
