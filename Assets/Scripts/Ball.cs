using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Ball : MonoBehaviour
{
    [Tooltip("Time to return to initial position")]
    [SerializeField] [Min(0)] private float returnTime = .3f;
    
    public bool IsInTransition { get; private set; }

    private Vector3 _initialPosition;
    private Vector3 _lastVelocity;
    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _initialPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
        
        LaunchBall(transform.forward, 5f);
    }

    public void LaunchBall(Vector3 direction, float force)
    {
        IsInTransition = true;
        
        _lastVelocity = direction.normalized * force;
        _rigidbody.velocity = direction.normalized * force;
    }
    
    private IEnumerator ReturnToInitialPosition()
    {
        IsInTransition = false;
        
        _rigidbody.velocity = Vector3.zero;
        _lastVelocity = Vector3.zero;
        
        var velocity = Vector3.zero;

        while ((_initialPosition - transform.position).magnitude > .001f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, _initialPosition, ref velocity, returnTime);
            yield return null;
        }

        transform.position = _initialPosition;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Wall"))
        {
            //todo debug
            print("Wall was hit");
            
            var centralNormal = Vector3.zero;
            foreach (var contactPoint in other.contacts) 
                centralNormal += contactPoint.normal;

            centralNormal /= other.contactCount;

            _lastVelocity = Vector3.Reflect(_lastVelocity, centralNormal).normalized * _lastVelocity.magnitude;
            print(_lastVelocity);
            
            _rigidbody.velocity = _lastVelocity;

            return;
        }

        var enemy = other.transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            //todo debug
            print("Enemy was hit");
            StartCoroutine(ReturnToInitialPosition());
            //todo add end turn event
            return;
        }

        var target = other.transform.GetComponent<Target>();
        if (target != null)
        {
            //todo debug
            print("Target was hit");
            target.Eliminate();

            StartCoroutine(ReturnToInitialPosition());
        }
    }
}
