using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Lightning : MonoBehaviour
{
    [SerializeField] int _damage = 3;
    [SerializeField]float _cool=20f;
    float _count;
    
    void Update()
    {
        _count += Time.deltaTime;
        Debug.Log(_count);
        if (_count > _cool)
        {
            _count = 0;
            DamageRandomEnemyInCamera();
        }
    }

    void DamageRandomEnemyInCamera()
    {
        List<Enemy> visibleEnemies = new List<Enemy>();

        // シーン内のすべての敵オブジェクトを取得
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemyObject in allEnemies)
        {
            Renderer renderer = enemyObject.GetComponent<Renderer>();
            if (renderer != null && renderer.isVisible)
            {
                Enemy enemyScript = enemyObject.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    visibleEnemies.Add(enemyScript);
                }
            }
            
        }

        if (visibleEnemies.Count == 0)
        {
            return;
        }

        
        int randomIndex = Random.Range(0, visibleEnemies.Count);
        Enemy targetEnemy = visibleEnemies[randomIndex];

        // ダメージを与える
        targetEnemy.TakeDamage(_damage);
        Debug.Log("kougeki");
    }
}
