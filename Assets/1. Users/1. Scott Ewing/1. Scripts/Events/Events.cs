using UnityEngine;

namespace ScottEwing.EventSystem{
    // The Game Events used across the Game.
    // Anytime there is a need for a new event, it should be added here.
    public static class Events{
        //== Game Events
        public static Template TemplateEvent = new Template();
        public static ActorAttackEvent ActorAttackEvent = new ActorAttackEvent();
        public static StartWalkingEvent StartWalkingEvent = new StartWalkingEvent();
        public static TryAttackEvent TryAttackEvent = new TryAttackEvent();
        public static ReceiveHealthEvent ReceiveHealthEvent = new ReceiveHealthEvent();
        public static ItemPickedUpEvent ItemPickedUpEvent = new ItemPickedUpEvent();
        public static FactFoundEvent FactFoundEvent = new FactFoundEvent();






#if SE_CHECKPOINTS
        public static CheckpointEvents checkpointEvents = new CheckpointEvents();
#endif

        
        //==Actor Events
    }
    
    public class Template : GameEvent{

    }
    public class FactFoundEvent : GameEvent{
    }
    public class ItemPickedUpEvent : GameEvent{
    }
    public class ReceiveHealthEvent : GameEvent{
        public int Health;
    }
    public class DamageTakenEvent : GameEvent{
        public int DamageTaken;
        public int RemainingHealth;
        public RaycastHit Hit;
    }
    
    public class ActorDeathEvent : GameEvent{
        public int DamageTaken;
        public RaycastHit Hit;
        public bool Gibbed;
    }
    
    public class ActorAttackEvent : GameEvent{
    }
    
    public class StartWalkingEvent : GameEvent{
        public Transform Target;
    }
    
    public class TryAttackEvent : GameEvent{
    }
    
}