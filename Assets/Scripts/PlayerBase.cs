using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    protected CharacterController2D characterController2D;
    protected float moveX;
    protected bool jump = false;
    protected float Speed = 50f;

    protected abstract void onMoving();

    protected abstract void CheckBlocking();

    public abstract void CheckLanding();
    protected void Update()
    {
        onMoving();
        CheckBlocking();
    }

}
