using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 3f;
    [SerializeField] private float _searchRadius = 5f;
    [SerializeField] private LayerMask _enemyLayer;

    private float _currentAtk;

    private Vector3 _direction;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.position += _direction * (_speed * Time.deltaTime);
    }

    public void Init(float atk)
    {
        _currentAtk = atk;
    }

    public void SetDirection()
    {
        // 最大10体の敵を検知
        Collider2D[] results = new Collider2D[10]; 
        int count = Physics2D.OverlapCircleNonAlloc(transform.position, _searchRadius, results, _enemyLayer);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        for (int i = 0; i < count; i++)
        {
            if (results[i] != null)
            {
                float distance = Vector2.Distance(transform.position, results[i].transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = results[i].transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            _direction = (closestEnemy.position - transform.position).normalized;
        }
        else
        {
            _direction = Vector3.right;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)_currentAtk);
            }
            else
            {
                Debug.LogWarning("[Bullet] Enemy スクリプトが見つからない");
            }

            // 弾を消す処理もお忘れなく
            Destroy(gameObject); 
        }
    }
}
