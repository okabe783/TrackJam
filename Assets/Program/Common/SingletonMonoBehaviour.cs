using System;
using UnityEngine;

/// <summary>
///  SingletonPatternというデザインパターンの一種
/// このクラスを継承したオブジェクトがアクセスされたときにまだInstanceされていなければ新しくInstanceを作る
/// もし、すでにInstanceされていれば2つめのオブジェクトはDestroyする
/// つまりそのオブジェクトが1つであることを保証するデザインパターンである
/// つまり2つ以上配置したり動的に生成するクラスには使用してはいけない
/// </summary>
/// <typeparam name="T"></typeparam>

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    //　シングルトンインスタンスを取得
    public static T I
    {
        get
        {
            if (_instance == null)
            {
                Type t = typeof(T);
                _instance = (T)FindAnyObjectByType(t);
                if (_instance == null)
                {
                    Debug.LogError(t + "をアタッチしているオブジェクトはありません");
                }
            }

            return _instance;
        }
    }
    
    public static bool ExistInstance()
    {
        return _instance != null;
    }

    protected void Awake()
    {
        CheckInstance();
    }
    
    // 2つ以上存在する場合は削除する
    protected bool CheckInstance()
    {
        // もし自身がまだInstanceされていなければ自身を登録する
        if (_instance == null)
        {
            _instance = this as T;
            return true;
        }
        
        if (I == this)
        {
            return true;
        }
        
        // もし2つ目をInstanceしようとした場合は自身を破棄する
        Destroy(this);
        return false;
    }
}
