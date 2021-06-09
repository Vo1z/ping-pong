using UnityEngine;

namespace TestTask
{
    public class Slingshot : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float maxDistance = 3f;
        [SerializeField] [Range(0, .1f)] private float aimSensitivityScale = .007f;
        [SerializeField] [Range(0, 10f)] private float forceScale = 1f;
        [Space]
        [SerializeField] private GameObject aim;
        [SerializeField] private Ball ball;

        public GameObject Aim => aim;
        public Ball Ball => ball;
        public float SlingshotForce => Vector3.Magnitude(_initialAimPosition - aim.transform.position) * forceScale;
        public Vector3 AimDirection => (aim.transform.position - _initialAimPosition);

        private Vector3 _initialAimPosition;

        private void Awake() => _initialAimPosition = aim.transform.position;

        private void Update()
        {
            if(Input.touchCount < 1)
                return;

            var touch = Input.GetTouch(0);
            var deltaPos = touch.deltaPosition * aimSensitivityScale;
            var aimPos = aim.transform.position;
            
            if (touch.phase != TouchPhase.Moved && touch.phase != TouchPhase.Stationary)
            {
                if (SlingshotForce > .1f && ball.IsReadyForLaunch) 
                    ball.LaunchBall(AimDirection, SlingshotForce);

                aim.transform.position = _initialAimPosition;
                return;
            }

            //Prevents aim going out of bounds
            if (Vector3.Magnitude(_initialAimPosition - aimPos) > maxDistance)
            {
                aim.transform.position = aimPos + (_initialAimPosition - aimPos).normalized * .001f; 
                return;
            }

            aim.transform.position = new Vector3(aimPos.x + deltaPos.x, _initialAimPosition.y, aimPos.z + deltaPos.y);
            aim.transform.LookAt(_initialAimPosition);
            aim.transform.Rotate(0, 180f, 0);
        }
        
        private void OnDrawGizmos()
        {
            if(aim == null)
                return;

            Gizmos.DrawWireSphere(_initialAimPosition, maxDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_initialAimPosition, .1f);
        }
    }
}
