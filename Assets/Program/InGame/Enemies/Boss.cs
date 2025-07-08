using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("プレイヤー参照")]
    [SerializeField] Transform _muzzle;  //発射口
    [SerializeField] GameObject _bullet;  //弾
    [SerializeField] GameObject _player;  //プレイヤーオブジェクト
    Vector3 _playerPos;


    [Header("テレポート設定")]
    [SerializeField] float _teleportMinDistance = 3;
    [SerializeField] float _teleportMaxDistance = 5;
    [SerializeField] float _teleportInterval = 2;
    float _teleportTheta;
    Vector3 _teleportPosition;

    [Header("")]

    [Header("StatsData参照")]
    [SerializeField] EnemyStutsData _stutsdata;  //敵別ステータスの設定

    private float _fireInterval;
    private int _power;

    float _teleportTimer;
    float _fireTimer;

    void Start()
    {
        _fireInterval = _stutsdata.FireInterval;
        _power = _stutsdata.ATK;
    }

    //public void Init(Player player)
    //{
    //    _player = player;
    //}

    void Update()
    {
        _teleportTimer += Time.deltaTime;
        _fireTimer += Time.deltaTime;


        if (_teleportInterval < _teleportTimer)
        {
            Teleport();
        }

        if (_stutsdata.enemyType == EnemyStutsData.EnemyType.LastBoss
         && _bullet
         && _muzzle)
        {
            BossFired();
        }

    }

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

    private void BossFired()
    {
        if (_fireInterval < _fireTimer)
        {
            Debug.Log("発射");
            _playerPos = _player.transform.position; // 発射時に最新位置を取得

            var bullet = Instantiate(_bullet, _muzzle.position, Quaternion.identity);
            var enemyBullet = bullet.GetComponent<EnemyBullet>();
            enemyBullet.SetDirection(_playerPos);
            enemyBullet.Initialize(_power);
            _fireTimer = 0f;
        }
    }

    private IEnumerator FireAllDirection(int bulletCount = 6)
    {
        if (bulletCount <= 0) yield break;

        const float fullRotation = 360;

        float fireAllDirectionInterval = 0; //
        float initAngle = 0f; //
        float addAngle = fullRotation / bulletCount;
        Transform muzzleTransform = default; //
        Vector3 initMuzzlePosion = muzzleTransform.position;
        Quaternion initMuzzleRotation = muzzleTransform.rotation;
        float _muzzleToObjectOriginDistance = 0f; //

        for (float crrentAngle = initAngle;
            crrentAngle < initAngle + fullRotation;
            crrentAngle += addAngle)
        {
            muzzleTransform.position = new Vector3(_muzzleToObjectOriginDistance * Mathf.Cos(crrentAngle) + _player.transform.position.x
                                                , _muzzleToObjectOriginDistance * Mathf.Sin(crrentAngle) + _player.transform.position.y
                                                , 0f);

            muzzleTransform.rotation = new Quaternion(muzzleTransform.rotation.x, muzzleTransform.rotation.y, crrentAngle, muzzleTransform.rotation.w);

            yield return new WaitForSeconds(fireAllDirectionInterval);
        }

        muzzleTransform.position = initMuzzlePosion;
        muzzleTransform.rotation = initMuzzleRotation;
    }
}
