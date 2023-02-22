using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRetroMuseum.ScottEwing{
    public static class Layers{
        public const string Ground = "Ground";
        public const string Player = "Player";
        public const string TakesDamage = "TakesDamage";
        public const string Dead = "Dead";

        public static int TakesDamageValue() => LayerMask.NameToLayer(TakesDamage);
        public static int DeadValue() => LayerMask.NameToLayer(Dead);

    }

    public static class Tags{
        
    }
    /*public class LayersAndTags : MonoBehaviour{
        
    }*/

    
}
