using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("プレイヤー参照")]
    [SerializeField] Transform _muzzle;  //発射口
    [SerializeField] GameObject _bullet;  //弾
    [SerializeField] GameObject _player;  //プレイヤーオブジェクト
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

    //public void Init(Player player)
    //{
    //    _player = player;
    //}

    void Update()
    {
        if (_player == null) return;

        _timer += Time.deltaTime;

        if (_muzzle)
        {
            FacePlayer();
        }

        if (!_isPlayerInRange)
        {
            EnemyMove();
        }

        if (_stutsdata.enemyType == EnemyStutsData.EnemyType.LongRange
            && _isPlayerInRange
            && _bullet
            && _muzzle)
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

    /// <summary>
    /// 遠距離攻撃
    /// </summary>
    private void EnemyFired()
    {
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
    /// 敵の向きや銃口進んでいる向きに変える
    /// </summary>
    private void FacePlayer()
    {
        Vector3 dir = _player.transform.position - transform.position;

        transform.localScale = new Vector3(Mathf.Sign(dir.x), 1, 1);

        // デバッグ：プレイヤーへのライン
        Debug.DrawLine(_muzzle.position, _player.transform.position, Color.red);
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
}
