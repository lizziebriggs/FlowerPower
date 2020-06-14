using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneManager : MonoBehaviour
    {
        public void LoadScene(int sceneIndex)
        {
            //LoadAsynchronously(sceneIndex);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            Debug.Log("Scene loaded");
        }

        private IEnumerator LoadAsynchronously(int sceneIndex)
        {
            AsyncOperation loadingOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);

            while (!loadingOperation.isDone)
            {
                // Loading bar progress
                yield return null;
            }
        }
    }
}
