using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToadIdle : IState<Toad>
{
    #region Initialize

    public static ToadIdle Instance { get; private set; }

    static ToadIdle()
    {
        Instance = new ToadIdle();
    }

    public void Enter(Toad enemy)
    {
        enemy.animator.Play("Idle");
        enemy.debugText.SetText("Idle");
    }

    public void Execute(Toad enemy)
    {
        enemy.ChangeState();
    }

    public void Exit(Toad enemy)
    {

    }

    #endregion
}

public class ToadJump : IState<Toad>
{
    #region Initialize

    public static ToadJump Instance { get; private set; }

    static ToadJump()
    {
        Instance = new ToadJump();
    }

    #endregion

    public void Enter(Toad enemy)
    {
        enemy.animator.Play("Jump");
        enemy.debugText.SetText("Jump");
        enemy.Flip();
        enemy.Jump();
    }

    public void Execute(Toad enemy)
    {
        if (enemy.isFalling && enemy.coll.enabled == false)
        {
            enemy.Flip();
            enemy.EnableColliderWhileFalling();
        }
        // 到达顶端，准备下落
        if (enemy.isRising && enemy.transform.position.y >= enemy.maxJumpHeight)
        {
            enemy.TitanFall();
        }

        // 已经落地
        if (!enemy.isRising && !enemy.isFalling)
        {
            enemy.ChangeState();
        }
    }

    public void Exit(Toad enemy)
    {
        enemy.isRising = enemy.isFalling = false;
    }

}

public class ToadAttack : IState<Toad>
{
    #region Initialize

    public static ToadAttack Instance { get; private set; }

    static ToadAttack()
    {
        Instance = new ToadAttack();
    }

    #endregion

    public void Enter(Toad enemy)
    {
        enemy.animator.Play("Attack");
        enemy.debugText.SetText("Attack");
        enemy.tongueCol.enabled = true;
    }

    public void Execute(Toad enemy)
    {
        enemy.Flip();
        if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            enemy.ChangeState();
        }
    }

    public void Exit(Toad enemy)
    {
        enemy.tongueCol.enabled = false;
    }

}

public class ToadDeath : IState<Toad>
{
    public static ToadDeath Instance { get; private set; }

    static ToadDeath()
    {
        Instance = new ToadDeath();
    }


    public void Enter(Toad enemy)
    {
        Debug.Log("death");
        enemy.animator.Play("Death");
        enemy.debugText.SetText("Death");
    }

    public void Execute(Toad enemy)
    {
        if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 2.2f)
        {
            enemy.enabled = false;
        }
    }

    public void Exit(Toad enemy)
    {

    }
}