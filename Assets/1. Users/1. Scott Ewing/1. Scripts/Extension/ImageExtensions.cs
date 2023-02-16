using UnityEngine;
using UnityEngine.UI;

namespace ScottEwing.ExtensionMethods{
    public static class ImageExtensions{
        public static void SetAlpha(this Image image, float alpha) {
            var newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;
        }
    }
}