using UnityEngine;

namespace ScottEwing.ExtensionMethods
{
    public static class LayerMaskExtensions
    {
        public static bool IsLayerInLayerMask(this LayerMask mask, int layer) => mask == (mask | (1 << layer));
    }
}
