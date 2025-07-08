using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
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

    

    public void SetDirection(Vector2 targetPosition)
    {
        _direction = new Vector2((targetPosition.x - transform.position.x),
            (targetPosition.y - transform.position.y)).normalized;
        
    }

    private void Fire()
    {
        transform.Translate(_direction * _bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
