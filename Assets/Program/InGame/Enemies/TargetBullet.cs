using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 1f;  //弾速
    Vector2 _direction;
    private int _power;

    public void Initialize (int power)
    {
        _power = power;
    }

    void FixedUpdate()
    {
        Fire();
    }

    /// <summary>
    /// 方向指定
    /// </summary>
    /// <param name="targetPosition"></param>
    public void SetDirection(Vector2 targetPosition)
    {
        _direction = new Vector2((targetPosition.x - transform.position.x),
            (targetPosition.y - transform.position.y)).normalized;
        
    }

    /// <summary>
    /// 発射
    /// </summary>
    private void Fire()
    {
        transform.Translate(_direction * _bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(_power);
            Destroy(gameObject);
        }
    }
}
