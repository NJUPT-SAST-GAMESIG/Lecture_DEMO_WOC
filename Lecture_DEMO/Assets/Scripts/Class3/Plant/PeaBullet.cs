using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    private float speed = 3;
    [SerializeField] private int damage = 0;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetAtk(int atk)
    {
        damage = atk;
    }

    private void Start()
    {
        Destroy(gameObject, 3f); //5秒后销毁
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            other.GetComponent<Zombie>().TakeDamage(damage);
            
            Destroy(gameObject);
            
            
        }
    }
    
}