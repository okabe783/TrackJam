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
    
    [Header("ボス出現設定")]
    [SerializeField] private List<BossSpawnData> _bossSpawnDataList;
    private int _bossIndex = 0;
    
    // シーンにあるPlayerObj
    [SerializeField] private PlayerController _player;
    // EnemyのData
    [SerializeField] private List<EnemyStutsData> _enemyStutsList;
    [SerializeField] private PlayerLevelManager _playerLevelManager;
    
    [SerializeField] private GameManager _gameManager;
    
    //タイマー変数
    public float SpawnTimer;
    // Spawn可能かどうか
    public bool IsSpawn;
    //スポーンさせた数（数を追加していく）
    public float Spawned;
    //enemyのオブジェクトプール用
    private ObjectPooler _pooler;
    

    private void Start()
    {
        //変数にコンポーネントを格納する
        _pooler = GetComponent<ObjectPooler>();
        IsSpawn = false;
    }

    private void Update()
    {
        if(!IsSpawn)
            return;
        
        //spawnタイマーの時間を減らす
        SpawnTimer -= Time.deltaTime;

        // 経過時間に応じて敵を増やす
        UpdateSpawnCountPerWave();
        CheckBossSpawn();
        
        //確認
        if (SpawnTimer < 0)
        {
            SpawnTimer = GetSpawnDelay();

            //スポーン上限の確認
            if (Spawned < enemyCount)
            {
                for (int i = 0; i < _spawnCountPerWave; i++)
                {
                    if (Spawned >= enemyCount) 
                        break;
                    
                    Spawned++;
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

    private void UpdateSpawnCountPerWave()
    {
        float gameTime = _gameManager.GameTime;
        
        // 30秒ごとにスポーン数+1（最大10体まで）
        _spawnCountPerWave = Mathf.Clamp(3 + Mathf.FloorToInt(gameTime / 30f), 3, 10);
    }
    
    private void CheckBossSpawn()
    {
        if (_bossIndex >= _bossSpawnDataList.Count)
            return;

        var data = _bossSpawnDataList[_bossIndex];
        if (_gameManager.GameTime >= data.SpawnTime)
        {
            SpawnBoss(data);
            _bossIndex++;
        }
    }
    
    private void SpawnBoss(BossSpawnData data)
    {
        if (data.BossPrefab == null) 
            return;

        GameObject boss = Instantiate(data.BossPrefab);
        boss.name = "Boss";
        Transform basePoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        Vector2 offset = data.SpawnOffset;
        boss.transform.position = basePoint.position + (Vector3)offset;
        
        Boss bossScript = boss.GetComponent<Boss>();
        if (bossScript != null)
        {
            bossScript.Init(_player, data.StutsData, _playerLevelManager);
        }

        Debug.Log($"[Spawner] ボス出現: {data.BossPrefab.name}（{data.SpawnTime}秒）");
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
