using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float attractRadius = 5f;
    public float damageRadius = 0.5f;

    public float weakPullSpeed = 0.5f;
    public float mediumPullSpeed = 1.5f;
    public float strongPullSpeed = 3f;

    /// <summary>
    /// もしブラックホールがプレイヤーにダメージを与える場合用
    /// </summary>
    //public float damagePerTick = 10f;
    //public float damageTickInterval = 1f;

    private Transform player;
    private Rigidbody2D playerRb;
    
    // private PlayerHealth playerHealth; // 任意のプレイヤーHP管理スクリプト参照
    

    //private bool isDamaging = false;
    //private float damageTimer = 0f;

    void Update()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                player = p.transform;
                playerRb = p.GetComponent<Rigidbody2D>();
                ///playerHealth = p.GetComponent<PlayerHealth>(); // あなたのゲームに合わせて調整
            }
            else
            {
                return;
            }
        }

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= attractRadius)
        {
            Vector2 direction = (transform.position - player.position).normalized;
            Vector2 playerVelocity = playerRb.velocity;

            float dot = Vector2.Dot(direction, playerVelocity.normalized);

            float pullSpeed = mediumPullSpeed;

            if (playerVelocity.magnitude < 0.1f)
            {
                pullSpeed = mediumPullSpeed;
            }
            else if (dot > 0.1f)
            {
                pullSpeed = strongPullSpeed;
            }
            else if (dot < -0.1f)
            {
                pullSpeed = weakPullSpeed;
            }

            player.position = Vector2.MoveTowards(player.position, transform.position, pullSpeed * Time.deltaTime);
        }

        /// ダメージ処理はコメントアウトされていますが、必要に応じて有効化
        //if (distance <= damageRadius)
        //{
        //    if (!isDamaging)
        //    {
        //        isDamaging = true;
        //        damageTimer = 0f;
        //    }

        //    damageTimer += Time.deltaTime;
        //    if (damageTimer >= damageTickInterval)
        //    {
        //        damageTimer = 0f;
        //        if (playerHealth != null)
        //        {
        //            playerHealth.TakeDamage(damagePerTick);
        //        }
        //    }
        //}
        //else
        //{
        //    isDamaging = false;
        //    damageTimer = 0f;
        //}
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attractRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
