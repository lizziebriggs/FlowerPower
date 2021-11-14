using UnityEngine;

namespace GUI
{
    public class GuiBase : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject greyOut;
        private static readonly int IsOpen = Animator.StringToHash("IsOpen");

        public void Show()
        {
            Time.timeScale = 0;
            greyOut.SetActive(true);
            animator.SetBool(IsOpen, true);
        }

        public void Hide()
        {
            Time.timeScale = 1;
            greyOut.SetActive(false);
            animator.SetBool(IsOpen, false);
        }
    }
}
