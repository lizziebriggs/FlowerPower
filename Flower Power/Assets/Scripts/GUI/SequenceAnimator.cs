using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUI
{
    public class SequenceAnimator : MonoBehaviour
    {
        [SerializeField] private List<Animator> animators;
        [SerializeField] private float sequenceDelay = .1f;
        [SerializeField] private float repeatDelay = 1f;

        private static readonly int DoAnimation = Animator.StringToHash("DoAnimation");

        private void Start()
        {
            StartCoroutine(TriggerAnimation());
        }

        private IEnumerator TriggerAnimation()
        {
            while (true)
            {
                foreach (Animator anim in animators)
                {
                    anim.SetTrigger(DoAnimation);
                    yield return new WaitForSeconds(sequenceDelay);
                }
                
                yield return new WaitForSeconds(repeatDelay);
            }
        }
    }
}
