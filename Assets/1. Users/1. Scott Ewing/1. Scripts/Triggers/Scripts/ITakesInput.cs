using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScottEwing.Triggers{
    public interface ITakesInput{
        public InputActionProperty InputActionReference { get; set; }
        public bool ShouldCheckInput { get; set; }
        public void GetInput();
    }
}
