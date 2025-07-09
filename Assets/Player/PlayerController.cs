using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] StatusData statusData;
    private Rigidbody2D rb;
    private Vector2 _moveInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ///<summary>
        ///移動処理
        float _movex = Input.GetAxisRaw("Horizontal");
        float _movey = Input.GetAxisRaw("Vertical");

        _moveInput = new Vector2(_movex, _movey).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = statusData._moveSpeed * _moveInput;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("残りのHPは"+statusData._hp--);
            statusData._hp = statusData._hp--;

            if (statusData._hp == 0)
            {
                Debug.Log("ゲームオーバー");
                //ゲームオーバー処理を書く
            }
        }
    }
}
