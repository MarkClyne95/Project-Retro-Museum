using UnityEngine;

namespace ScottEwing.MovingPlatforms{
    

    public abstract class BasePhysicsPlatform {
        public virtual void MovePlatform(Vector3 direction, float force) {

        }

        public virtual void MoveWithAcceleration(Vector3 direction, float acceleration) {

        }

        public virtual float GetPlatformVelocityMagnitude() {
            return 0;
        }

        public virtual void StopPlatform() {
            ClampVelocity(0);
        }

        public virtual void ClampVelocity(float maxLength) {
            
        }

        public virtual void SetPlatformVelocity(Vector3 velocity) {
            
        }

        public abstract Vector3 GetPlatformVelocity();

        //public abstract bool IsRigidbodyNull();


    }
}
