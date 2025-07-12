using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//スポーンモード
public enum Spawnmodes
{
    constant,//一定間隔でスポーンする
    Random,//ランダムにスポーンする
}

public class Spawner : MonoBehaviour
{
    [Header("スポーンモード設定")]
    [SerializeField] private Spawnmodes spawnModes = Spawnmodes.constant;
    //最短のスポーン間隔
    [SerializeField] private float minRandomDelay;
    //最長のスポーン間隔
    [SerializeField] private float maxRandomDelay;
    //一定モードのスポーン時間
    [SerializeField] private float constantSpawnTime;
    
    [Header("スポーン制御")]
    [SerializeField] private int enemyCount = 100;
    // 一度にだす量
    [SerializeField] private int _spawnCountPerWave = 3;
    [SerializeField] private float _spawnRadius = 3f;
    [SerializeField] private Transform[] _spawnPoints;
    
    // シーンにあるPlayerObj
    [SerializeField] private PlayerController _player;
    // EnemyのData
    [SerializeField] private List<EnemyStutsData> _enemyStutsList;
    [SerializeField] private PlayerLevelManager _playerLevelManager;
    
    //タイマー変数
    public float spawnTimer;
    // Spawn可能かどうか
    public bool _isSpawn;
    //スポーンさせた数（数を追加していく）
    public float _spawned;
    //enemyのオブジェクトプール用
    private ObjectPooler _pooler;
    

    private void Start()
    {
        //変数にコンポーネントを格納する
        _pooler = GetComponent<ObjectPooler>();
        _isSpawn = false;
    }

    private void Update()
    {
        if(!_isSpawn)
            return;
        
        //spawnタイマーの時間を減らす
        spawnTimer -= Time.deltaTime;
        //確認
        if (spawnTimer < 0)
        {
            spawnTimer = GetSpawnDelay();

            //スポーン上限の確認
            if (_spawned < enemyCount)
            {
                for (int i = 0; i < _spawnCountPerWave; i++)
                {
                    if (_spawned >= enemyCount) 
                        break;
                    
                    _spawned++;
                    SpawnEnemy();
                }
            }
        }
    }

    /// <summary>
    /// 敵を生成する
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void SpawnEnemy()
    {
        //プールから取得して変数に格納
        GameObject newInstance = _pooler.GetObjectFromPool();
        var stutsData = _enemyStutsList[Random.Range(0, _enemyStutsList.Count)];
        
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.Init(_player,stutsData,_playerLevelManager);
        
        //エネミーの初期設定
        SetEnemy(newInstance);
        //表示する
        newInstance.SetActive(true);
    }

    private void SetEnemy(GameObject newInstance)
    {
        Transform basePoints = _spawnPoints[Random.Range(0,_spawnPoints.Length)];
        Vector2 offset = Random.insideUnitCircle * _spawnRadius;
        newInstance.transform.position = basePoints.position + (Vector3)offset;
    }

    /// <summary>
    /// 一定もしくはランダムの数値を返す
    /// </summary>
    /// <returns></returns>
    private float GetSpawnDelay()
    {
        if (spawnModes == Spawnmodes.constant)
        {
            return constantSpawnTime;
        }
        else
        {
            //引数の間からランダムな数値を選んで返す
            return Random.Range(minRandomDelay, maxRandomDelay);

        }
    }
}
