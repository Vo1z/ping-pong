using System;
using UnityEngine;

namespace TestTask
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] [Min(0f)] private float initialSpeed = 1.3f;
        [Tooltip("How much speed enemy gains with each new level")] 
        [SerializeField] [Min(0f)] private float deltaSpeed = .2f;
        [Space]
        [SerializeField] private Ball ball;

        private float _currentSpeed;

        private void Awake()
        {
            GameManager.Instance.OnLevelUp += IncreaseSpeed;
            _currentSpeed = initialSpeed;
        }

        private void Start() => GameManager.Instance.OnLevelUp += IncreaseSpeed;

        private void Update()
        {
            if (ball.IsInTransition)
                FollowTheBall();
        }

        private void FollowTheBall()
        {
            var ballPosX = ball.transform.position.x;
            var enemyPosX = transform.position.x;

            if (Math.Abs(ballPosX - enemyPosX) < .1f)
                return;
            
            var velocity = Mathf.Sign(ballPosX - enemyPosX) * Vector3.right;
            velocity *= Time.deltaTime * _currentSpeed;
            
            transform.position += velocity;
        }

        private void IncreaseSpeed() => _currentSpeed += deltaSpeed;
    }
}