using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [Header("プレイヤー参照")]
    [SerializeField] private Transform _muzzle;  //発射口
    [SerializeField] private GameObject _bullet;  //弾
    private PlayerLevelManager _playerLevelManager;
    private PlayerController _player;  //プレイヤーオブジェクト
    private Vector3 _playerPos;  //プレイヤーの現在位置

    [Header("StatsData参照")]
    EnemyStutsData _stutsData;  //敵別ステータスの設定

    [Header("デバッグ用")]
    public EnemyStutsData _debugData;
    public PlayerController _debugPlayer;
    public PlayerLevelManager _debugLevel;

    [HideInInspector] public bool _isPlayerInRange;
    [HideInInspector] public bool _isHit;

    private int _currentHp;
    private int _currentAtk;
    private float _fireInterval;
    private int _scoreValue;
    private float _currentSpeed;
    private float _expGain;
    private float _attackRange;
    private float _attackInterval;
    private float _knockBackForce;

    private float _timer;
    private float _attackTimer;

    Rigidbody2D _rb;

    public void Init(PlayerController player, EnemyStutsData stutsData,PlayerLevelManager playerLevelManager)
    {
        _player = player;
        _stutsData = stutsData;

        _currentHp = _stutsData.MAXHP;
        _currentAtk = _stutsData.ATK;
        _currentSpeed = _stutsData.SPEED;
        _fireInterval = _stutsData.FireInterval;
        _scoreValue = _stutsData.SCORE;
        _expGain = _stutsData.EXP;
        _knockBackForce = _stutsData.KnockBack;
        _attackRange = _stutsData.ATTACKRANGE;
        _attackInterval = _stutsData.ATTACKINTERVAL;
        _playerLevelManager = playerLevelManager;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_stutsData == null)
        {
            _stutsData = _debugData;
            _currentSpeed = _stutsData.SPEED;
            _attackRange = _stutsData.ATTACKRANGE;
            _attackInterval = _stutsData.ATTACKINTERVAL;
        }

        if (_player == null)
        {
            _player = _debugPlayer;
        }

        if (_playerLevelManager == null)
        {
            _playerLevelManager = _debugLevel;
        }

        if (_muzzle)
        {
            FacePlayer();
        }


        if (_stutsData.enemyType == EnemyStutsData.EnemyType.LongRange
        && _isPlayerInRange
        && _bullet
        && _muzzle)
        {
            if (_isPlayerInRange)
            {
                EnemyFired();
            }
            else
            {
                EnemyMove();
            }
        }

        if (_stutsData.enemyType == EnemyStutsData.EnemyType.ShortRange)
        {
            ShortRangeAttack();
        }
    }

    /// <summary>
    /// 敵の基本的な動き
    /// </summary>
    private void EnemyMove()
    {
        _playerPos = _player.gameObject.transform.position;  //プレイヤーの現在位置を取得

        //Vector2.MoveTowards(a, b, maxDistanceDelta) は、「a から b へ maxDistanceDelta 分だけ進む」
        transform.position =
            Vector2.MoveTowards(transform.position, _playerPos, _currentSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 遠距離攻撃
    /// </summary>
    private void EnemyFired()
    {
        if (_fireInterval < _timer)
        {
            Debug.Log("発射");
            _playerPos = _player.gameObject.transform.position; // 発射時に最新位置を取得

            var bullet = Instantiate(_bullet, _muzzle.position, Quaternion.identity);
            var targetBullet = bullet.GetComponent<TargetBullet>();
            //方向を渡す
            targetBullet.SetDirection(_playerPos);
            //攻撃力を渡す
            targetBullet.Initialize(_currentAtk);

            _timer = 0f;
        }
    }

    private void ShortRangeAttack()
    {
        _playerPos = _player.gameObject.transform.position;
        //自分と相手の距離を取得
        float distance = Vector2.Distance(transform.position, _playerPos);

        if (distance > _attackRange)
        {
            EnemyMove();
        }
        else
        {
            _attackTimer += Time.deltaTime;  //振りかぶるモーション等攻撃を避ける時間
            if (_attackTimer >= _attackInterval)
            {
                _attackTimer = 0f;
                Attack();
            }
        }

    }

    private void Attack()
    {
        if (_player == null) return;

        Debug.Log("近距離攻撃！");

        _player.TakeDamage(_currentAtk);

        KnockBack();
    }

    /// <summary>
    /// 敵の向きや銃口進んでいる向きに変える
    /// </summary>
    private void FacePlayer()
    {
        Vector3 dir = _player.gameObject.transform.position - transform.position;

        transform.localScale = new Vector3(Mathf.Sign(dir.x), 1, 1);

        // デバッグ：プレイヤーへのライン
        Debug.DrawLine(_muzzle.position, _player.gameObject.transform.position, Color.red);
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

    private void KnockBack()
    {
        Vector2 knockBackDirection = (transform.position - _player.gameObject.transform.position).normalized;

        _rb.AddForce(knockBackDirection * _knockBackForce, ForceMode2D.Impulse);

    }

    private void Die()
    {
        /// <summary>
        // 経験値オーブを落とす
        ExpDropper dropper = GetComponent<ExpDropper>();
        if (dropper != null)
        {
            dropper.DropExpOrbs();
        }
        /// <summary>

        ScoreManager.I.AddScore(_scoreValue);

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
            _stutsData.enemyType == EnemyStutsData.EnemyType.LongRange)
        {
            KnockBack();
        }
    }
}
