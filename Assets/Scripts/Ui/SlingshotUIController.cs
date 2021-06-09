using UnityEngine;
using UnityEngine.UI;

namespace TestTask.UI
{
    [RequireComponent(typeof(Slingshot))]
    public class SlingshotUIController : MonoBehaviour
    {
        [SerializeField] private Image arrowImage;
        [SerializeField] [Range(0, 1)] private float arrowPositionScale = .06f;
        [SerializeField] [Range(0, 1)] private float arrowSizeScale = .2f;

        private Slingshot _slingshot;
        private GameObject _slingshotAim;
        private RectTransform _imageTransform;

        private void Awake()
        {
            _slingshot = GetComponent<Slingshot>();
            _slingshotAim = _slingshot.Aim;
            _imageTransform = arrowImage.GetComponent<RectTransform>();
        }

        private void Update()
        {
            var aimingDirection = _slingshot.AimDirection;

            if (aimingDirection != Vector3.zero)
            {
                _imageTransform.rotation = Quaternion.LookRotation(-_slingshot.AimDirection);
                _imageTransform.Rotate(90f, 90f, 0);
            }

            var aimPos = _slingshotAim.transform.position;
            var aimForwardDirection = _slingshotAim.transform.forward.normalized;
            
            _imageTransform.transform.position = aimPos + aimForwardDirection * (_slingshot.PullingForce * arrowPositionScale);
            _imageTransform.transform.localScale = Vector3.one + Vector3.right * (_slingshot.PullingForce * arrowSizeScale);
        }
    }
}
