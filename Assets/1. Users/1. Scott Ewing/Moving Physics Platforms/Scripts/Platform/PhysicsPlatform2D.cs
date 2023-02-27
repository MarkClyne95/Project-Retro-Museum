using UnityEngine;

namespace ScottEwing.MovingPlatforms{
    public class PhysicsPlatform2D : BasePhysicsPlatform{
        public Rigidbody2D Rb { get; set; }
        public PhysicsPlatform2D(Rigidbody2D rigidbody2D) {
            Rb = rigidbody2D;
        }


        public override void MovePlatform(Vector3 direction, float force) {
            Rb.velocity = direction * force;
        }

        public override void MoveWithAcceleration(Vector3 direction, float acceleration) {
            Rb.AddForce(direction * acceleration, ForceMode2D.Impulse);
        }
        
        public override Vector3 GetPlatformVelocity() {
            return Rb.velocity;
        }

        /*public override bool IsRigidbodyNull() {
            if (Rb == null) {
                return true;
            }
            return false;
        }*/

        /*public override bool IsRigidbodyNull() {
            if (Rb == null) {
                return true;
            }
            return false;
        }*/

        public override float GetPlatformVelocityMagnitude() {
            return Rb.velocity.magnitude;
        }

        public override void ClampVelocity(float maxLength) {
            Rb.velocity = Vector3.ClampMagnitude(Rb.velocity, maxLength);       // Set platform velocity to zero
        }

        public override void SetPlatformVelocity(Vector3 velocity) {
            Rb.velocity = velocity;
        }

    }
}