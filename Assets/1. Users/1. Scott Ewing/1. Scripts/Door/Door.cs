using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
//using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

namespace ProjectRetroMuseum.ScottEwing.Door{
    public class Door : MonoBehaviour{
        [SerializeField] private bool _openWhenTriggered;
        [SerializeField] private Transform _openTransform, _closedTransform;
        private Vector3 _openPos, _closedPos;
        private Coroutine _moveRoutine;
        [SerializeField] private float _moveTime = 0.25f;
        private AudioSource _audioSource;
        [SerializeField] private AudioClip _openDoorClip;
        [SerializeField] private AudioClip _closeDoorClip;

        private float _volume;
        [SerializeField] protected float _volumeMultiplier = 0.5f;


        void Awake() {
            _audioSource = GetComponent<AudioSource>();
            _openPos = _openTransform.position;
            _closedPos = _closedTransform.position;
            transform.position = _openWhenTriggered ? _closedPos : _openPos;

            _volume = PlayerPrefs.GetFloat("SFXVol");
        }

        public void TriggerEntered() {
            if (_openWhenTriggered)
                OpenDoor();
            else
                CloseDoor();
        }

        public void TriggerExited() {
            if (_moveRoutine != null) StopCoroutine(_moveRoutine);
            if (_openWhenTriggered)
                CloseDoor();
            else
                OpenDoor();
        }

        private void OpenDoor() {
            if (_moveRoutine != null) StopCoroutine(_moveRoutine);
            _moveRoutine = StartCoroutine(MoveDoorRoutine(_closedPos, _openPos));
            _audioSource.PlayOneShot(_openDoorClip, _volume * _volumeMultiplier);
        }

        private void CloseDoor() {
            if (_moveRoutine != null) StopCoroutine(_moveRoutine);
            _moveRoutine = StartCoroutine(MoveDoorRoutine(_openPos, _closedPos));
            _audioSource.PlayOneShot(_closeDoorClip, _volume * _volumeMultiplier);
        }

        IEnumerator MoveDoorRoutine(Vector3 start, Vector3 end) {
            float time = 0;
            while (time < _moveTime) {
                transform.position = Vector3.Lerp(start, end, time / _moveTime);
                time += Time.deltaTime;
                yield return null;
            }

            transform.position = end;
            _moveRoutine = null;
        }
    }
}