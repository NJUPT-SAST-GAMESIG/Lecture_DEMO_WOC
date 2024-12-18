using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class peashooter : Plant
{
    private GameObject peaBullet;

    private void Start()
    {
        plantName = "peashooter";
        health = 100;
        attackCooldown = 0.7f;
        peaBullet = Resources.Load<GameObject>("BulletPrefab/PeaBullet");
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity,
            1 << LayerMask.NameToLayer("UI"));//射线检测有无僵尸决定是否执行攻击
        if (!hit)
            return;
        if (!hit.transform.CompareTag("Zombie"))
            return;
        if (peaBullet != null)
        {
            //Debug.Log(1);
            Vector3 trans = new Vector3(transform.position.x, transform.position.y + 0.25f);

            GameObject pb = Instantiate(peaBullet, trans, Quaternion.identity);
            pb.transform.SetParent(transform.parent);
            pb.transform.SetSiblingIndex(1);
        }
    }
}