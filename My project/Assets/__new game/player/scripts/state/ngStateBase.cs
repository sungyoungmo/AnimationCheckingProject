using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ngStateBase
{
    protected ngPlayerController player;

    protected ngStateBase(ngPlayerController player)
    {
        this.player = player;
    }

    public abstract void StateEnter();
    public abstract void StateUpdate();
    public abstract void StateExit();

}
