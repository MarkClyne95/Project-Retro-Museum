using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScottEwing{
    public interface ITakesDamage{
        
        bool TakeDamage(int damage, RaycastHit raycastHit);
    }
}
