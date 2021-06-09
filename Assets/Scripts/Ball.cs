using System;
using System.Collections;
using UnityEngine;

namespace TestTask
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Ball : MonoBehaviour
    {
        [Tooltip("Time to return to initial position")] [SerializeField] [Min(0)]
        private float returnTime = .3f;

        public bool IsInTransition { get; private set; } = true;
        public bool IsReadyForLaunch { get; private set; } = true;

        private Vector3 _initialPosition;
        private Vector3 _lastVelocity;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _initialPosition = transform.position;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _lastVelocity = _rigidbody.velocity;
            
            if (_rigidbody.velocity.magnitude < .001f)
                StartCoroutine(ReturnToInitialPosition());
        }

        public void LaunchBall(Vector3 direction, float force)
        {
            var impulseVelocity = direction.normalized * force;
            _rigidbody.AddForce(impulseVelocity, ForceMode.Impulse);
        }

        private IEnumerator ReturnToInitialPosition()
        {

            _rigidbody.velocity = Vector3.zero;
            _lastVelocity = Vector3.zero;

            while ((_initialPosition - transform.position).magnitude > .001f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, _initialPosition, ref _lastVelocity, returnTime);
                yield return null;
            }

            transform.position = _initialPosition;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Wall"))
            {

                var centralNormal = Vector3.zero;
                foreach (var contactPoint in other.contacts)
                    centralNormal += contactPoint.normal;

                centralNormal /= other.contactCount;

                _lastVelocity = Vector3.Reflect(_lastVelocity, centralNormal).normalized * _lastVelocity.magnitude;

                return;
            }

            var enemy = other.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                StartCoroutine(ReturnToInitialPosition());
                return;
            }

            var target = other.transform.GetComponent<Target>();
            if (target != null)
            {
                target.Eliminate();

                StartCoroutine(ReturnToInitialPosition());
            }
        }
    }
}