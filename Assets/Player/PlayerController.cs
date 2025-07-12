using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerオブジェクトの親にアタッチするクラス
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public StatusData _statusData;
    [Header("初期武器プレハブ")]
    [SerializeField] private DefaultBullet _weaponPrefab;

    [Header("発射位置")]
    [SerializeField] private Transform _muzzle;


    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Animator _animators;

    private int _maxHp;
    private float _moveSpeed;
    private int _currentHp;
    private bool _isDead;
    private float _attackPower;
    private bool _isFacingRight = true;


    private List<GameObject> _ownedWeapons = new();
    [SerializeField] private float _weaponTimer = 1f;
    private float _currentWeaponTimer;
    private Animator animator;

    private void Awake()
    {
        _maxHp = _statusData.Hp;
        _moveSpeed = _statusData.MoveSpeed;
        _isDead = false;
        _attackPower = _statusData.Atk;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _currentHp = _maxHp;
        _currentWeaponTimer = _weaponTimer;
    }

    void Update()
    {
        _weaponTimer -= Time.deltaTime;
        Move();

        if (_weaponTimer <= 0f)
        {
            _weaponTimer = _currentWeaponTimer;
            Attack();
        }
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
        if (moveX > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (moveX < 0 && _isFacingRight)
        {
            Flip();
        }
        Anim();

    }
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;

        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    private void Anim()
    {
        animator.SetBool("RightAnimation", _moveInput.magnitude != 0.0f);
    }

    private void Attack()
    {
        if (_weaponPrefab == null || _muzzle == null)
        {
            Debug.LogWarning("[Player] 武器または Muzzle が未設定です");
            return;
        }

        GameObject bullet = Instantiate(_weaponPrefab.gameObject, _muzzle.position, Quaternion.identity);
        DefaultBullet bulletComp = bullet.GetComponent<DefaultBullet>();

        if (bulletComp != null)
        {
            bulletComp.Init(_attackPower);
            bulletComp.SetDirection();
        }
    }

    public void TakeDamage(int damage)
    {
        if (_isDead) return;

        //_currentHp -= damage;

        if (_currentHp <= 0)
        {
             Die();
        }
    }

    void Die()
    {
        _isDead = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int enemyAtk = collision.gameObject.GetComponent<Enemy>()._currentAtk;
            TakeDamage(enemyAtk);
        }
    }

    // ========== 強化/回復 ==========

    public void AddWeapon(GameObject weaponPrefab)
    {
        if (weaponPrefab == null)
            return;

        GameObject newWeapon = Instantiate(weaponPrefab, transform.position + Vector3.right, Quaternion.identity);
        newWeapon.transform.SetParent(transform);
        _ownedWeapons.Add(newWeapon);

        Debug.Log($"[Player] 武器追加: {weaponPrefab.name}");
    }

    public void UpgradeExistingWeapon(string weaponName)
    {
        foreach (GameObject weapon in _ownedWeapons)
        {
            if (weapon.name.Contains(weaponName))
            {
                var upgradable = weapon.GetComponent<IUpgradeableWeapon>();
                if (upgradable != null)
                {
                    upgradable.Upgrade();
                    Debug.Log($"[Player] {weaponName} をレベルアップ");
                }
                return;
            }
        }

        Debug.LogWarning($"[Player] {weaponName} が見つからなかった");
    }

    public void UpgradeAttack(int amount)
    {
        _attackPower += amount;
        Debug.Log($"[Player] 攻撃力アップ: +{amount} (現在: {_attackPower})");
    }

    public void Heal(int value)
    {
        _currentHp = Mathf.Min(_currentHp + value, _maxHp);
        Debug.Log($"[Player] HP回復 +{value}（{_currentHp}/{_maxHp}）");
    }

    public void BoostSpeed(float amount)
    {
        _moveSpeed += amount;
        Debug.Log($"[Player] 移動速度 +{amount}（現在: {_moveSpeed}）");
    }

}
