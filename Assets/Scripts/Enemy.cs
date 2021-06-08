using System;
using UnityEngine;

namespace TestTask
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] [Min(0f)] private float _initialSpeed;

        [Tooltip("How much speed enemy gains with each new level")] [SerializeField] [Min(0f)]
        private float _deltaSpeed;

        [SerializeField] private Ball _ball;

        private float _currentSpeed;

        private void Awake()
        {
            GameManager.Instance.OnLevelUp += IncreaseSpeed;
            _currentSpeed = _initialSpeed;
        }

        private void Start() => GameManager.Instance.OnLevelUp += IncreaseSpeed;

        private void Update()
        {
            if (_ball.IsInTransition)
                FollowTheBall();
        }

        private void FollowTheBall()
        {
            var ballPosX = _ball.transform.position.x;
            var enemyPosX = transform.position.x;

            if (Math.Abs(ballPosX - enemyPosX) < .0001f)
                return;

            var velocity = Mathf.Sign(ballPosX - enemyPosX) * Vector3.right;

            velocity *= Time.deltaTime * _currentSpeed;

            transform.position += velocity;
        }

        private void IncreaseSpeed() => _currentSpeed += _deltaSpeed;
    }
}