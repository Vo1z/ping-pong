using UnityEngine;

namespace TestTask
{
    public class Target : MonoBehaviour
    {
        private void Start() => TargetManager.Instance.AddTarget(this);
        public void Eliminate() => TargetManager.Instance.TurnOffTarget(this);
    }
}