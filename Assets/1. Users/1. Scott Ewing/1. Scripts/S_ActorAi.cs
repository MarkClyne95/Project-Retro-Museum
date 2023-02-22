using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProjectRetroMuseum.ScottEwing{
    public class S_ActorAi : IActorComponent{
        private S_ActorController _actorController;
        public void Initialise(S_ActorController sActorController) {
            _actorController = sActorController;
        }

        public void OnDestroy() {
        }

        public void Update() {
            
        }
    }
}
