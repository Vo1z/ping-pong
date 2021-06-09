using UnityEngine;

namespace TestTask
{
    public class Slingshot : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float maxDistance = 3f;
        [SerializeField] [Range(0, .1f)] private float aimSpeedScale = .007f;
        [SerializeField] [Range(0, 10f)] private float forceScale = 1f;
        
        [Space]
        [SerializeField] private GameObject aim;
        [SerializeField] private Ball ball;

        private float SlingshotForce => (_initialAimPosition - aim.transform.position).magnitude * forceScale;
        private Vector3 AimDirection => (aim.transform.position - _initialAimPosition);

        private Vector3 _initialAimPosition;

        private void Awake() => _initialAimPosition = aim.transform.position;

        private void Update()
        {
            if(Input.touchCount < 1)
                return;

            var touch = Input.GetTouch(0);
            var deltaPos = touch.deltaPosition * aimSpeedScale;
            var aimPos = aim.transform.position;
            
            if (touch.phase != TouchPhase.Moved && touch.phase != TouchPhase.Stationary)
            {
                if (SlingshotForce > .1f && ball.IsReadyForLaunch) 
                    ball.LaunchBall(AimDirection, SlingshotForce);

                aim.transform.position = _initialAimPosition;
                return;
            }

            //Prevents aim going out of bounds
            if ((_initialAimPosition - aim.transform.position).magnitude > maxDistance)
            {
                aim.transform.position = aimPos + (_initialAimPosition - aimPos).normalized * .001f; 
                return;
            }

            aim.transform.position = new Vector3(aimPos.x + deltaPos.x, _initialAimPosition.y, aimPos.z + deltaPos.y);
        }
        
        private void OnDrawGizmos()
        {
            if(aim == null)
                return;
            
            var aimPos = aim.transform.position;

            Gizmos.DrawWireSphere(_initialAimPosition, maxDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_initialAimPosition, .1f);
        }
    }
}
