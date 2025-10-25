using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;
    public float cooldown = 2f;
    private float timer;

    void Update()
    {
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (timer <= 0f)
        {
            anim.SetBool("isAttacking", true);
            timer = cooldown;
        }
    }

    public void StopAttack()
    {
        anim.SetBool("isAttacking", false);
    }
}

