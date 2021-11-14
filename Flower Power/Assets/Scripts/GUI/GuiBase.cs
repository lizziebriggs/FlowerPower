using UnityEngine;

namespace GUI
{
    public class GuiBase : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private bool pause;
        [SerializeField] private GameObject greyOut;
        private static readonly int IsOpen = Animator.StringToHash("IsOpen");

        public void Show()
        {
            if(pause) Time.timeScale = 0;
            if(greyOut) greyOut.SetActive(true);
            animator.SetBool(IsOpen, true);
        }

        public void Hide()
        {
            if(pause) Time.timeScale = 1;
            if(greyOut) greyOut.SetActive(false);
            animator.SetBool(IsOpen, false);
        }
    }
}
