using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class TargetManager : MonoBehaviour
    {
        public static TargetManager Instance { get; private set; }

        public int ActiveTargets
        {
            get
            {
                var activeTargets = 0;
                foreach (var target in _targets)
                    if (target.isActiveAndEnabled)
                        activeTargets++;

                return activeTargets;
            }
        }

        private List<Target> _targets = new List<Target>();

        private void Awake()
        {
            if (Instance != this && Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void Start() => GameManager.Instance.OnLevelUp += TurnOnTargets;

        public void AddTarget(Target target)
        {
            if (target != null)
                _targets.Add(target);
        }

        public void TurnOffTarget(Target target)
        {
            target.gameObject.SetActive(false);
            //todo debug
            print($"Number of active targets {ActiveTargets}");
            if (ActiveTargets < 1)
                GameManager.Instance.LevelUp();
        }

        public void TurnOnTargets()
        {
            foreach (var target in _targets)
                target.gameObject.SetActive(true);
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            foreach (var targer in _targets)
                Gizmos.DrawLine(transform.position, targer.transform.position);
        }
    }
}
