using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScottEwing{
    public interface ITakesDamage{
        
        void TakeDamage(int damage, RaycastHit raycastHit);
    }
}
