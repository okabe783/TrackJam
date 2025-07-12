using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    private int _power;
    Rigidbody2D _rb;

    public void Initialize(int power)
    {
        _power = power;
    }

    void Update()
    {
        BulletMove();
    }

    private void BulletMove()
    {
        // 毎フレーム、進行方向に加算
        transform.position += transform.right * _bulletSpeed * Time.deltaTime;
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
