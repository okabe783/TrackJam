using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("プレイヤー参照")]
    [SerializeField] private Transform _muzzle;  //発射口
    [SerializeField] private GameObject _bullet;  //弾
    [SerializeField] private GameObject _player;  //プレイヤーオブジェクト
    [SerializeField] private GameObject _straightBullet;
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
    [SerializeField] EnemyStutsData _stutsData;  //敵別ステータスの設定

    [Header("突進設定")]
    [SerializeField] private float _rushSpeed = 5f;
    [SerializeField] private float _rushInterval = 3f;

    private float _fireInterval;
    private int _power;
    private float _currentSpeed;

    private float _teleportTimer;
    private float _fireTimer;
    private float _allRangefireTimer;
    private bool _isFiringAllDirection;

    Rigidbody2D _bossRb;
    private float _rushIntervalTimer;


    void Start()
    {
        _fireInterval = _stutsData.FireInterval;
        _power = _stutsData.ATK;
        _currentSpeed = _stutsData.SPEED;
        _bossRb = GetComponent<Rigidbody2D>();
    }

    //public void Init(Player player)
    //{
    //    _player = player;
    //}

    void Update()
    {
        _teleportTimer += Time.deltaTime;
        _fireTimer += Time.deltaTime;
        _allRangefireTimer += Time.deltaTime;
        _rushIntervalTimer += Time.deltaTime;

        BossMove();

        if (_teleportInterval < _teleportTimer)
        {
            Teleport();
        }

        if (_stutsData.enemyType == EnemyStutsData.EnemyType.LastBoss
         && _bullet
         && _muzzle)
        {
            BossFired();
        }

        if (_allRangefireTimer > _allRangefireInterval)
        {
            AllRangeFired();
        }

        if (_rushIntervalTimer > _rushInterval)
        {
            Rush();
        }
    }

    private void BossMove()
    {
        _playerPos = _player.transform.position;  //プレイヤーの現在位置を取得
        transform.position =
            Vector2.MoveTowards(transform.position, _playerPos, _currentSpeed * Time.deltaTime);
        //Vector2.MoveTowards(a, b, maxDistanceDelta) は、「a から b へ maxDistanceDelta 分だけ進む」
    }

    /// <summary>
    /// プレイヤーの一定距離内の転移
    /// </summary>
    private void Teleport()
    {
        _teleportTheta = Random.Range(0, 360);
        float teleportDistance = Random.Range(_teleportMinDistance, _teleportMaxDistance);

        _teleportPosition.x = teleportDistance * Mathf.Cos(_teleportTheta) + _player.transform.position.x;
        _teleportPosition.y = teleportDistance * Mathf.Sin(_teleportTheta) + _player.transform.position.y;
        _teleportPosition.z = 0;
        this.transform.position = _teleportPosition;

        _teleportTimer = 0;
    }

    /// <summary>
    /// プレイヤーに向かっての遠距離攻撃
    /// </summary>
    private void BossFired()
    {
        if (_fireInterval < _fireTimer)
        {
            Debug.Log("発射");
            _playerPos = _player.transform.position; // 発射時に最新位置を取得

            var bullet = Instantiate(_bullet, _muzzle.position, Quaternion.identity);
            var enemyBullet = bullet.GetComponent<TargetBullet>();
            enemyBullet.SetDirection(_playerPos);
            enemyBullet.Initialize(_power);
            _fireTimer = 0f;
        }
    }

    private void Rush()
    {
        var targetPosition = _player.transform.position;

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

            Instantiate(_straightBullet, _muzzleTransform.position, _muzzleTransform.rotation);

            yield return new WaitForSeconds(_fireAllDirectionInterval);
        }

        //muzzleの位置と角度を初期化
        _muzzleTransform.position = initMuzzlePosion;
        _muzzleTransform.rotation = initMuzzleRotation;

        _allRangefireTimer = 0f;
        _isFiringAllDirection = false;
    }
}
