using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//スポーンモード
public enum Spawnmodes
{
    constant,//一定間隔でスポーンする
    Random,//ランダムにスポーンする

}

public class Spawner : MonoBehaviour
{

    //スポーンモードの選択
    [SerializeField] private Spawnmodes spawnModes = Spawnmodes.constant;
    //最短のスポーン間隔
    [SerializeField] public float minRandomDelay;
    //最長のスポーン間隔
    [SerializeField] public float maxRandomDelay;
    //一定モードのスポーン時間
    [SerializeField] public float constantSpawnTime;
    //スポーンさせる数を設定する
    [SerializeField] public int enemyCount = 10;

    [SerializeField] public int poolSize = 20;

    [SerializeField]public GameObject EnemyPrefab;        // 敵プレハブ（Enemy）

    
    //タイマー変数
    public float spawnTimer;
    //スポーンさせた数（数を追加していく）
    public float spawned;
    //enemyのオブジェクトプール用
    private ObjectPooler pooler;
    

    private void Start()
    {
        //変数にコンポーネントを格納する
        pooler = GetComponent<ObjectPooler>();

        
    }

    private void Update()
    {
        //spawnタイマーの時間を減らす
        spawnTimer -= Time.deltaTime;
        //確認
        if (spawnTimer < 0)
        {
            spawnTimer = GetSpawnDelay();

            //スポーン上限の確認
            if (spawned < enemyCount)
            {
                //生成済みの数を追加
                spawned++;

                //敵を生成
                SpawnEnemy();

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
        GameObject newInstance = pooler.GetObjectFromPool();   
        //エネミーの初期設定
        SetEnemy(newInstance);
        //表示する
        newInstance.SetActive(true);
    }

    private void SetEnemy(GameObject newInstance)
    {
        //enemyでMovepointを使えるように格納している
        Enemy enemy = newInstance.GetComponent<Enemy>();
       
        //生成位置をこのオブジェクトの位置に設定
        enemy.transform.position = transform.position;
        

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
            return UnityEngine.Random.Range(minRandomDelay, maxRandomDelay);

        }
    }
}
