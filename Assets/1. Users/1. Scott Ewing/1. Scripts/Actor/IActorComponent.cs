using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActorComponent{
    public void Initialise(S_ActorController sActorController);
    public void OnDestroy();
}
