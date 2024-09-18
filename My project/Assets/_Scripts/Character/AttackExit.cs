using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackExit : StateMachineBehaviour
{
    readonly int ComboCount = Animator.StringToHash("ComboCount");

    [SerializeField]
    string triggerName;

    PlayerDamageConroller playerDamage;
    bool colliderEnabled = false;
    AnimatorStateInfo previousStateInfo = new();

    [System.Serializable]
    public class ComboInfo
    {
        public string comboName;
        public int comboCount;
    }

    [SerializeField]
    public List<ComboInfo> comboInfo;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerDamage == null)
        {
            playerDamage = animator.gameObject.GetComponentInChildren<PlayerDamageConroller>();
        }

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerDamage.DisableCollider();
        animator.ResetTrigger(triggerName);
        animator.SetInteger(ComboCount, 0);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var item in comboInfo)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(item.comboName))
            {
                animator.SetInteger(ComboCount, item.comboCount);

                
                if (previousStateInfo.fullPathHash != animator.GetCurrentAnimatorStateInfo(0).fullPathHash)
                {
                    playerDamage.EnableCollider();
                }

                previousStateInfo = animator.GetCurrentAnimatorStateInfo(0);

                return;
            }
        }
    }
}