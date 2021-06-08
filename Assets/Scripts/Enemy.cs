using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float _initialSpeed;
    [Tooltip("How much speed enemy gains with each new level")]
    [SerializeField] [Min(0f)] private float _deltaSpeed;

    private Ball _ball;
    private float _currentSpeed;
    
    private void Update()
    {
        if(_ball.IsInTransition)
            MoveToBall();
    }

    private void MoveToBall()
    {
        var ballPosX = _ball.transform.position.x;
        var enemyPosX = transform.position.x;
        
        if (enemyPosX - ballPosX > .001f) 
            return;

        var velocity = Mathf.Sign(ballPosX - enemyPosX) * Vector3.right;
        velocity *= Time.deltaTime * _currentSpeed;

        transform.position += velocity;
    }
}
