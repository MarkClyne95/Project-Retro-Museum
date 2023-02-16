using UnityEngine;

namespace ScottEwing.Triggers{
    public interface ICastInteractor{
        float SphereCastRadius { get; set; }
        LayerMask CollisionLayers { get; set; }
        QueryTriggerInteraction TriggerInteraction { get; set; }
    }
}
