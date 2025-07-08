using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //生成するアイテム
    [SerializeField] private GameObject poolObject;
    //生成する数
    [SerializeField] private int poolSize = 20;
    //生成したオブジェクトを管理するリスト
    private List<GameObject> pool;
    //親オブジェクトを格納しておく
    private GameObject poolContainer;

    private void Awake()
    {
        //インスタンス化
        pool = new List<GameObject>();
        //オブジェクトを生成して名前を付けて変数に格納
        poolContainer = new GameObject($"Pool - {poolObject.name}");
        //プールオブジェクトの作成
        CreatePooler();
    }

    /// <summary>
    /// poolSizeの数poolObjectを生成する
    /// </summary>
    private void CreatePooler()
    {
        //poolsizeの分ループ
        for (int i = 0; i < poolSize; i++)
        {
            //生成したオブジェクトをリストに格納
            pool.Add(CreateObject());

        }
    }

    /// <summary>
    /// poolobjectを生成して呼んだ場所に返す
    /// </summary>
    /// <returns></returns>
    private GameObject CreateObject()
    {
        //オブジェクトを作成して変数に格納する
        GameObject newInstance = Instantiate(poolObject);

        //親の設定
        newInstance.transform.SetParent(poolContainer.transform);


        //非表示
        newInstance.SetActive(false);

        //返す
        return newInstance;
    }

    /// <summary>
    /// プールからオブジェクトを引き出す
    /// </summary>
    /// <returns></returns>
    public GameObject GetObjectFromPool()
    {
        //リストに格納されている分ループする
        for (int i = 0; i < pool.Count; i++)
        {
            //もしプール内の非表示オブジェクトだったら
            if (!pool[i].activeInHierarchy)
            {
                //呼び出した場所に返す
                return pool[i];

            }

        }

        //足りなければ生成して返す
        return CreateObject();

    }

    /// <summary>
    /// プールにオブジェクトを返却
    /// </summary>
    /// <param name="instance"></param>
    public static void ReturnToPool(GameObject instance)
    {
        instance.SetActive(false);
    }

}
