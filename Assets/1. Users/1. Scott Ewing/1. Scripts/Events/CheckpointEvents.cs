using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace ScottEwing.EventSystem
{
    public class CheckpointEvents
    {
        public CheckpointReachedEvent checkpointReachedEvent = new CheckpointReachedEvent();
        public ResetToCheckpointEvent resetToCheckpointEvent = new ResetToCheckpointEvent();

    }
    
    public class CheckpointReachedEvent : GameEvent{
        public Vector3 position;
        public Quaternion rotation;
    }
    
    public class ResetToCheckpointEvent : GameEvent{
        public bool checkpointAvailable;
        public Vector3 position;
        public Quaternion rotation;
    }
    
    public class ObjectRespawnedEvent : GameEvent{
        public GameObject respawnedObject;
        public Collider[] childColliders;
    }
}

