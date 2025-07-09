using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSerchArea : MonoBehaviour
{
    private Enemy _enemyParent;

    void Start()
    {
        _enemyParent = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _enemyParent._isPlayerInRange = true;
            Debug.Log("プレイヤーを検知");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _enemyParent._isPlayerInRange = false;
            Debug.Log("プレイヤーを見失った");
        }
    }
}
