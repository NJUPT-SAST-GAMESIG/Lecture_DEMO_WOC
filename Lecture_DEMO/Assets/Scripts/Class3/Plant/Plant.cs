using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Plant : MonoBehaviour
{
    protected string plantName; // 植物名称
    protected int health; // 生命值
    protected int attackDamage; // 攻击伤害
    protected float attackCooldown; // 攻击冷却时间
    protected float attackRange; // 攻击范围
    public bool isAlive = true; // 植物是否存活

    private float attackTimer; // 攻击冷却计时器


    protected virtual void Start()
    {
        SetAnimator("Animation/Plants/" + plantName + "/" + plantName);
    }

    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void SetAnimator(string animatorPath)
    {
        RuntimeAnimatorController runTimeAnimator = Resources.Load<RuntimeAnimatorController>(animatorPath);

        Animator animator = gameObject.GetComponent<Animator>();

        if (animator == null)
            animator = gameObject.AddComponent<Animator>();

        if (runTimeAnimator != null)
            animator.runtimeAnimatorController = runTimeAnimator;
    }


    protected virtual void Die()
    {
        isAlive = false;

        Animator animator = gameObject.GetComponent<Animator>();
        if (animator == null)
            animator = gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = null;

        Image image = gameObject.GetComponent<Image>();
        image.color = new Color(255, 255, 255, 0f);
        GridScript grid = gameObject.GetComponent<GridScript>();
        if (grid)
            grid.ReleaseGrid();
        Destroy(this);
    }


    protected virtual void FixedUpdate()
    {
        if (!isAlive) return;

        // 更新攻击冷却时间
        if (attackTimer < attackCooldown)
        {
            attackTimer += Time.fixedDeltaTime;
        }
        else
        {
            // 攻击冷却结束，可以执行攻击
            Attack();
            attackTimer = 0f;
        }
    }

    // 植物攻击行为（可以根据需要重写）
    protected virtual void Attack()
    {
    }
}