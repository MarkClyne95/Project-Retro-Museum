using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using UnityEngine;

public class S_BarrelAnimator : S_ActorAnimator
{
    //--Barrel has no attack animation
    protected override void OnAttack(ActorAttackEvent obj) { }

    //--Called From Animation Event
    public void AttackOnDeath() {
        _actorController.BroadcastTryAttack();
    }   
}
