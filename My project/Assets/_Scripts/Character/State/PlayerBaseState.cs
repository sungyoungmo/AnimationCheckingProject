using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{

    protected PlayerController _player;

    protected PlayerBaseState(PlayerController _player)
    {
        this._player = _player;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
    public abstract void OnStateFixedUpdate();
}