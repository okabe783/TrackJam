using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("プレイヤー参照")]
    [SerializeField] private Transform _muzzle;  //発射口
    [SerializeField] private GameObject _bullet;  //弾
    [SerializeField] private GameObject _straightBullet;
    private PlayerController _player;
    private PlayerLevelManager _playerLevelManager;
    private Vector3 _playerPos;


    [Header("テレポート設定")]
    [SerializeField] private float _teleportMinDistance = 3f;  //テレポートのプレイヤーからの最小距離
    [SerializeField] private float _teleportMaxDistance = 5f;  //テレポートのプレイヤーからの最大距離
    [SerializeField] private float _teleportInterval = 2f;  //テレポートのインターバル
    private float _teleportTheta;
    private Vector3 _teleportPosition;

    [Header("全体攻撃設定")]
    [SerializeField] float _fireAllDirectionInterval = 0f;  //一つを撃つ間のインターバル
    [SerializeField] float initAngle = 0f;  //初期角度
    [SerializeField] Transform _muzzleTransform = default; 　//muzzleの位置
    [SerializeField] float _muzzleToObjectOriginDistance = 0f;  //プレイヤーからのマズルの位置
    [SerializeField] float _allRangefireInterval = 3f;  //全方位攻撃のインターバル
    [SerializeField] int _bulletCount = 6;  //弾の数(360を割る数）

    [Header("StatsData参照")]
    EnemyStutsData _stutsData;  //敵別ステータスの設定

    [Header("デバッグ用")]
    public EnemyStutsData _debugData;
    public PlayerController _debugPlayer;
    public PlayerLevelManager _debugLevel;

    Rigidbody2D _bossRb;
    private float _rushIntervalTimer;

    private int _currentHp;
    private int _currentAtk;
    private int _scoreValue;
    private float _fireInterval;
    private float _currentSpeed;
    private float _expGain;
    private float _knockBackForce;
    private float _rushSpeed;
    private float _rushInterval;
    private float _attackRange;
    private float _attackInterval;

    private float _attackTimer;
    private float _teleportTimer;
    private float _fireTimer;
    private float _allRangefireTimer;
    private bool _isFiringAllDirection;


    void Awake()
    {
        _bossRb = GetComponent<Rigidbody2D>();
    }

    public void Init(PlayerController player, EnemyStutsData stutsData, PlayerLevelManager playerLevelManager)
    {
        _player = player;
        _stutsData = stutsData;
        _playerLevelManager = playerLevelManager;

        _currentHp = _stutsData.MAXHP;
        _currentAtk = _stutsData.ATK;
        _currentSpeed = _stutsData.SPEED;
        _fireInterval = _stutsData.FireInterval;
        _scoreValue = _stutsData.SCORE;
        _expGain = _stutsData.EXP;
        _knockBackForce = _stutsData.KnockBack;
        _rushSpeed = _stutsData._RUSHSPEED;
        _rushInterval = _stutsData.RUSHINTERVAL;
        _attackRange = _stutsData.ATTACKRANGE;
        _attackInterval = _stutsData.ATTACKINTERVAL;
    }

    void Update()
    {
        _teleportTimer += Time.deltaTime;
        _fireTimer += Time.deltaTime;
        _allRangefireTimer += Time.deltaTime;
        _rushIntervalTimer += Time.deltaTime;

        if (_stutsData == null)
        {
            _stutsData = _debugData;
            _fireInterval = _stutsData.FireInterval;
        }

        if (_player == null)
        {
            _player = _debugPlayer;
        }

        if (_playerLevelManager == null)
        {
            _playerLevelManager = _debugLevel;
        }

        ////敵のタイプが1体目のボスの時
        //if (_rushIntervalTimer > _rushInterval
        //    && _stutsData.enemyType == EnemyStutsData.EnemyType.FirstBoss)
        //{
        //    Rush();
        //}

        //敵のタイプが2体目のボスの時
        if (_stutsData.enemyType == EnemyStutsData.EnemyType.FirstBoss
            || _stutsData.enemyType == EnemyStutsData.EnemyType.SecondBoss)
        {
            //BossMove();
            ShortRangeAttack();
        }

        //敵のタイプがラスボスの時
        if (_teleportInterval < _teleportTimer
            && _stutsData.enemyType == EnemyStutsData.EnemyType.LastBoss)
        {
            Teleport();
        }

        //敵のタイプがラスボスの時＋nullチェック
        if (_stutsData.enemyType == EnemyStutsData.EnemyType.LastBoss
         && _bullet
         && _muzzle)
        {
            BossFired();
        }

        //敵のタイプが2体目のボスかラスボスの時
        if (_stutsData.enemyType == EnemyStutsData.EnemyType.SecondBoss
            || _stutsData.enemyType == EnemyStutsData.EnemyType.LastBoss)
        {
            if (_allRangefireTimer > _allRangefireInterval)
            {
                AllRangeFired();
            }
        }
    }

    private void BossMove()
    {
        _playerPos = _player.gameObject.transform.position;  //プレイヤーの現在位置を取得

        //Vector2.MoveTowards(a, b, maxDistanceDelta) は、「a から b へ maxDistanceDelta 分だけ進む」
        transform.position =
            Vector2.MoveTowards(transform.position, _playerPos, _currentSpeed * Time.deltaTime);
    }

    private void ShortRangeAttack()
    {
        _playerPos = _player.gameObject.transform.position;
        //自分と相手の距離を取得
        float distance = Vector2.Distance(transform.position, _playerPos);

        if (distance > _attackRange)
        {
            if (_stutsData.enemyType == EnemyStutsData.EnemyType.FirstBoss
               && _rushIntervalTimer > _rushInterval)
            {
                Rush();
            }
            else if (_stutsData.enemyType == EnemyStutsData.EnemyType.SecondBoss)
            {
                BossMove();
            }
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

        //_player.TakeDamage(_currentAtk);

        // ノックバックも入れる場合
        KnockBackBoss();
    }

    /// <summary>
    /// プレイヤーの一定距離内の転移
    /// </summary>
    private void Teleport()
    {
        //テレポートさせる角度をランダム化
        _teleportTheta = Random.Range(0, 360);
        //プレイヤーからの距離をランダム化
        float teleportDistance = Random.Range(_teleportMinDistance, _teleportMaxDistance);

        //座標をsin,cosでr(距離)を取ってくる
        _teleportPosition.x = teleportDistance * Mathf.Cos(_teleportTheta) + _player.transform.position.x;
        _teleportPosition.y = teleportDistance * Mathf.Sin(_teleportTheta) + _player.transform.position.y;
        _teleportPosition.z = 0;

        //とってきた座標にテレポート
        this.transform.position = _teleportPosition;

        _teleportTimer = 0;
    }

    /// <summary>
    /// プレイヤーに向かっての遠距離攻撃
    /// </summary>
    private void BossFired()
    {
        // Debug.Log($"_fireInterval{_fireInterval}_fireTimer {_fireTimer}");
        if (_fireInterval < _fireTimer)
        {
            Debug.Log("発射");
            // 発射時に最新位置を取得
            _playerPos = _player.gameObject.transform.position;

            var bullet = Instantiate(_bullet, _muzzle.position, Quaternion.identity);
            var targetBullet = bullet.GetComponent<TargetBullet>();
            //方向を渡す
            targetBullet.SetDirection(_playerPos);
            //攻撃力を渡す
            targetBullet.Initialize(_currentAtk);
            _fireTimer = 0f;
        }
    }

    private void Rush()
    {
        //プレイヤーの最新位置を取得
        var targetPosition = _player.gameObject.transform.position;

        //方向をベクトルでとってくる
        Vector2 direction = new Vector2((targetPosition.x - transform.position.x),
                                        (targetPosition.y - transform.position.y)).normalized;

        _bossRb.AddForce(direction * _rushSpeed, ForceMode2D.Impulse);

        _rushIntervalTimer = 0f;

    }

    /// <summary>
    /// 全方位攻撃の開始コルーチン
    /// </summary>
    private void AllRangeFired()
    {
        if (_isFiringAllDirection) return;
        StartCoroutine(FireAllDirection());
    }

    /// <summary>
    /// 全方位攻撃
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireAllDirection()
    {
        if (_bulletCount <= 0) yield break;

        _isFiringAllDirection = true;

        const float fullRotation = 360f;

        float addAngle = fullRotation / _bulletCount;
        Vector3 initMuzzlePosion = _muzzleTransform.position;
        Quaternion initMuzzleRotation = _muzzleTransform.rotation;
        Debug.Log("addAngleは" + addAngle);

        //currentAngleに初期角度を渡し、回す角度を一周するまで
        for (float currentAngle = initAngle;
            currentAngle < initAngle + fullRotation;
            currentAngle += addAngle)
        {
            float rad = currentAngle * Mathf.Deg2Rad;  //sin,cosを使うのでradへ変換

            // ボスの中心からの位置でマズルを回す
            Vector3 offset = new Vector3(
                _muzzleToObjectOriginDistance * Mathf.Cos(rad),
                _muzzleToObjectOriginDistance * Mathf.Sin(rad),
                0f);

            _muzzleTransform.position = this.transform.position + offset;

            // Z軸を回転（クォータニオンで角度を指定）
            _muzzleTransform.rotation = Quaternion.Euler(0f, 0f, currentAngle);

            var bullet = Instantiate(_straightBullet, _muzzleTransform.position, _muzzleTransform.rotation);
            var straightBullet = bullet.GetComponent<StraightBullet>();
            if (straightBullet != null)
            {
                straightBullet.Initialize(_currentAtk);
            }
            else
            {
                Debug.LogError("StraightBulletがアタッチされてない");
            }


            yield return new WaitForSeconds(_fireAllDirectionInterval);
        }

        //muzzleの位置と角度を初期化
        _muzzleTransform.position = initMuzzlePosion;
        _muzzleTransform.rotation = initMuzzleRotation;

        _allRangefireTimer = 0f;
        _isFiringAllDirection = false;
    }


    private void KnockBackBoss()
    {
        Vector2 knockBackDirection = transform.position - _player.gameObject.transform.position;

        _bossRb.AddForce(knockBackDirection * _knockBackForce, ForceMode2D.Impulse);

    }
    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        _currentHp -= damage;

        //0以下になったら死亡
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 死亡したときの処理
    /// </summary>
    private void Die()
    {
        ScoreManager.I.AddScore(_scoreValue);

        if (_playerLevelManager != null)
        {
            _playerLevelManager.AddExperience(_expGain);
            Debug.Log($"[Enemy] プレイヤーに経験値 {_expGain} を付与！");
        }

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            KnockBackBoss();
        }
    }
}
