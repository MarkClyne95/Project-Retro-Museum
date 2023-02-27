using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ScottEwing.MovingPlatforms;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

/*
 * THIS DOESNT WORK PROPERLY
 * ASYNC CODE WILL STILL RUN WHEN PLAY MODE IS ENDED
 */
namespace ScottEwing.MovingPlatforms{
    public class TransformPlatform : BasePhysicsPlatform{
        private Transform _transform;
        private Task _task;

        private bool _isCancelled;
        //private CancellationTokenSource _tokenSource;

        public TransformPlatform(Transform transform) {
            this._transform = transform;
            //_tokenSource = new CancellationTokenSource();
        }

        public override void MovePlatform(Vector3 direction, float force) {
            Debug.Log("MovePlatform");
            //_tokenSource.IsCancellationRequested = false;
            _task = MovePlatformAsync(direction, force);
        }

        private async Task MovePlatformAsync(Vector3 direction, float force) {
            while (!_isCancelled) {
                _transform.Translate(direction * force);
                await Task.Yield();
            }

            _isCancelled = false;
        }

        public override void MoveWithAcceleration(Vector3 direction, float acceleration) {
            Debug.Log("MoveWithAcceleration");

        }

        public override Vector3 GetPlatformVelocity() {
            Debug.Log("GetPlatformVelocity");

            return Vector3.back;
        }



        public override float GetPlatformVelocityMagnitude() {
            Debug.Log("GetPlatformVelocityMagnitude");

            return 1;
        }

        public override void StopPlatform() {
            _isCancelled = true;
            //_tokenSource.Cancel();
        }

        public override void ClampVelocity(float maxLength) {
            Debug.Log("ClampVelocity");
        }

        public override void SetPlatformVelocity(Vector3 velocity) {
            Debug.Log("Set Platform Velocity");
        }
    }
}
