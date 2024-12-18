using UnityEngine;

public class Zombie : BaseZombie
{
    // 在派生类中赋值
    public Zombie()
    {
        speed = 0.5f;
        ZombieHp = 100;
        atkpower = 20;
        atkSpeed = 1.5f;
        atkTimer = 0;
    }


    // 其他特定于 Zombie 的代码
}