using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("プレイヤー参照")]
    [SerializeField] GameObject _player;  //プレイヤーオブジェクト
    [SerializeField] Transform _muzzle;  //発射口
    [SerializeField] GameObject _bullet;  //弾
    Vector3 _playerPos;  //プレイヤーの現在位置

    [Header("StatsData参照")]
    [SerializeField] EnemyStutsData _stutsdata;  //敵別ステータスの設定

    [HideInInspector] public bool _isPlayerInRange;

    private int _currentHp;
    private int _currentAtk;
    private float _fireInterval;
    private float _timer;

    void Awake()
    {
        _currentHp = _stutsdata.MAXHP;
        _currentAtk = _stutsdata.ATK;
        _fireInterval = _stutsdata.FireInterval;
    }

    void Update()
    {
        if (_player == null) return;

        if (!_isPlayerInRange)
        {
            EnemyMove();
        }

        if (_stutsdata.enemyType == EnemyStutsData.EnemyType.LongRange
            && _isPlayerInRange)
        {
            EnemyFired();
        }
    }

    /// <summary>
    /// 敵の基本的な動き
    /// </summary>
    private void EnemyMove()
    {
        _playerPos = _player.transform.position;  //プレイヤーの現在位置を取得
        transform.position =
            Vector2.MoveTowards(transform.position, _playerPos, _stutsdata.SPEED * Time.deltaTime);
        //Vector2.MoveTowards(a, b, maxDistanceDelta) は、「a から b へ maxDistanceDelta 分だけ進む」
    }

    private void EnemyFired()
    {
        _timer += Time.deltaTime;

        if (_fireInterval < _timer)
        {
            Debug.Log("発射");
            _playerPos = _player.transform.position; // 発射時に最新位置を取得

            var bullet = Instantiate(_bullet, _muzzle.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetDirection(_playerPos);

            _timer = 0f;
        }
    }

    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    /// <param name="damage">攻撃側から参照</param>
    public void TakeDamage(int damage)
    {
        _currentHp -= damage;

        //0以下になったら死亡
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //AddScore();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //PlayerControler playerControler = collision.gameObject.GetComponent<PlayerControler>();
        //playerControler.TakeDamage(_currentAtk);

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (_stutsdata.enemyType == EnemyStutsData.EnemyType.LongRange
    //        && collision.gameObject.CompareTag("PlayerSerchArea"))
    //    {
    //        EnemyFired();
    //    }
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (_stutsdata.enemyType == EnemyStutsData.EnemyType.LongRange
    //        && collision.gameObject.CompareTag("PlayerSerchArea"))
    //    {
    //        EnemyFired();
    //    }
    //}

}
