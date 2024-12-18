using System;
using UnityEngine;

public enum ZombieState
{
    Move,
    Eat,
    Die,
}

public abstract class BaseZombie : MonoBehaviour
{
    protected ZombieState zombieState = ZombieState.Move;
    protected Rigidbody2D rb;
    protected Animator _animator;

    protected float speed;
    protected int ZombieHp;
    protected int zombiecurrentHp;
    protected int atkpower;
    protected float atkSpeed;
    protected float atkTimer;
    protected Plant currentEatenPlant;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        zombiecurrentHp = ZombieHp;
    }

    protected virtual void FixedUpdate()
    {
        switch (zombieState)
        {
            case ZombieState.Move:
                MoveUpdate();
                break;
            case ZombieState.Eat:
                EatUpdate();
                break;
        }
    }

    protected virtual void MoveUpdate()
    {
        rb.MovePosition(rb.position + Vector2.left * speed * Time.fixedDeltaTime);
    }

    protected virtual void EatUpdate()
    {
        atkTimer += Time.fixedDeltaTime;
        if (atkTimer >= atkSpeed && currentEatenPlant != null)
        {
            currentEatenPlant.TakeDamage(atkpower);
            atkTimer = 0;
        }

        if (currentEatenPlant != null && !currentEatenPlant.isAlive) EatTransitioneToMove();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        currentEatenPlant = other.GetComponent<Plant>();
        if (currentEatenPlant != null)
        {
            _animator.Play("Eat");
            TransitionToEat();
        }
    }

    protected virtual void EatTransitioneToMove()
    {
        _animator.Play("Move");
        zombieState = ZombieState.Move;
    }

    protected virtual void TransitionToEat()
    {
        atkTimer = 0;
        zombieState = ZombieState.Eat;
    }

    public virtual void TakeDamage(int damage)
    {
        if (zombiecurrentHp <= 0) return;
        zombiecurrentHp -= damage;
        if (zombiecurrentHp <= 0)
        {
            zombiecurrentHp = -1;
            Die();
        }
    }

    public virtual void Die()
    {
        if (zombieState == ZombieState.Die) return;
        zombieState = ZombieState.Die;
        GetComponent<Collider2D>().enabled = false;
        _animator.SetBool("IsDie", zombieState == ZombieState.Die);
        GameObject head = Instantiate(Resources.Load<GameObject>("ZombiesPrefab/ZombieHead"), transform);
        head.transform.position = new Vector3(transform.position.x + 0.7f, transform.position.y, transform.position.z);
        Destroy(gameObject, 2f);
    }
}