using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    private bool _isUsed = false;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Zombie"))
            return;
        BaseZombie zombie = other.GetComponent<BaseZombie>();
        if (_isUsed)
        {
            zombie.Die();
            return;
        }
        _isUsed = true;
        _rigidbody2D.velocity = new Vector2(8, 0);
        zombie.Die();
        Destroy(gameObject,5f);
    }
}
