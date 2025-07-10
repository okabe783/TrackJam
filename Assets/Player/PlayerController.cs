using UnityEngine;

/// <summary>
/// Playerオブジェクトの親にアタッチするクラス
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // スタックメモリ
    public StatusData _statusData;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    private int _maxHp;
    private float _moveSpeed;
    private int _currentHp;
    private bool _isDead;

    private void Awake()
    {
        _maxHp = _statusData._hp;
        _moveSpeed = _statusData._moveSpeed;
        _isDead = false;
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentHp = _maxHp;
    }
    
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        _rb.velocity = _moveSpeed * _moveInput;
    }

    // 移動処理
    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _moveInput = new Vector2(moveX, moveY).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int enemyAtk = collision.gameObject.GetComponent<Enemy>()._currentAtk;
            
            // ここで敵の攻撃力分のダメージを受ける
            _currentHp -= enemyAtk;

            if (_currentHp == 0 && _isDead == false)
            {
                // バグらないようにフラグをたてる
                _isDead = true;
                Debug.Log("ゲームオーバー");
                //ゲームオーバー処理を書く
            }
        }
    }
}
