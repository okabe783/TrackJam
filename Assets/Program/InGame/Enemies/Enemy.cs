using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("プレイヤー参照")]
    [SerializeField] private Transform _muzzle;  //発射口
    [SerializeField] private GameObject _bullet;  //弾
    private PlayerController _player;  //プレイヤーオブジェクト
    private Vector3 _playerPos;  //プレイヤーの現在位置

    [Header("StatsData参照")]
    [SerializeField] EnemyStutsData _stutsData;  //敵別ステータスの設定

    [HideInInspector] public bool _isPlayerInRange;

    private int _currentHp;
    public int _currentAtk;
    private float _fireInterval;
    private float _timer;
    private int _scoreValue;
    private float _currentSpeed;

    public void Init(PlayerController player, EnemyStutsData stutsData)
    {
        _player = player;
        _stutsData = stutsData;

        _currentHp = _stutsData.MAXHP;
        _currentAtk = _stutsData.ATK;
        _currentSpeed = _stutsData.SPEED;
        _fireInterval = _stutsData.FireInterval;
        _scoreValue = _stutsData.SCORE;
    }

    void Update()
    {
        if (_player == null) return;

        _timer += Time.deltaTime;

        if (_muzzle)
        {
            FacePlayer();
        }

        //索敵範囲外の時
        if (!_isPlayerInRange)
        {
            EnemyMove();
        }

        //typeが遠距離キャラの時＋索敵範囲内の時
        if (_stutsData.enemyType == EnemyStutsData.EnemyType.LongRange
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

    private void Die()
    {
        ScoreManager.I.AddScore(_scoreValue);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //PlayerControler playerControler = collision.gameObject.GetComponent<PlayerControler>();
        //playerControler.TakeDamage(_currentAtk);
    }
}
