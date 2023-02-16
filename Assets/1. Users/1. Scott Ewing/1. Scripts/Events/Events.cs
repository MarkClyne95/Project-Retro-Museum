using UnityEngine;

namespace ScottEwing.EventSystem{
    // The Game Events used across the Game.
    // Anytime there is a need for a new event, it should be added here.
    public static class Events{
        //== Game Events
        public static Template TemplateEvent = new Template();

#if SE_CHECKPOINTS
        public static CheckpointEvents checkpointEvents = new CheckpointEvents();
#endif

        
        //==Actor Events
    }
    
    public class Template : GameEvent{

    }
    public class DamageTakenEvent : GameEvent{
        public int DamageTaken;
        public int RemainingHealth;
    }
    
    public class ActorDeathEvent : GameEvent{
        public int DamageTaken;
    }
    
}