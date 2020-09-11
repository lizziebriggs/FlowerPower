using UnityEngine;

namespace GUI
{
    public class GuiBase : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private static readonly int IsOpen = Animator.StringToHash("IsOpen");

        public void Show()
        {
            Time.timeScale = 0;
            animator.SetBool(IsOpen, true);
        }

        public void Hide()
        {
            Time.timeScale = 1;
            animator.SetBool(IsOpen, false);
        }
    }
}
