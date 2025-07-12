using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absolute : MonoBehaviour
{
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] int attackDamage = 2;
    [SerializeField] LayerMask enemyLayer;
    private float cooldown = 3f;
    float count = 2;
    Animator animator;
    public Transform attackPoint;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;


        if (count > cooldown)
        {
            count = 0;
            attack();
        }


    }

    void attack()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(attackDamage);
            }


        }

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
