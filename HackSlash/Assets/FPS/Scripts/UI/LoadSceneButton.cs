using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Unity.FPS.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        public string SceneName = "";

        [Header("Parameters")]
        [Tooltip("Duration of the fade-to-black at the end of the game")]
        public float SceneTransitionLoadDelay = 3f;

        [Tooltip("The canvas group of the fade-to-black screen")]
        public CanvasGroup SceneTransitionFadeCanvasGroup;

        public bool SceneIsTransitioning { get; private set; }
        float m_TimeLoadEndGameScene;
        string m_SceneToLoad;

        void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject
                && Input.GetButtonDown(GameConstants.k_ButtonNameSubmit))
            {
                LoadTargetScene();
            }

            if(SceneIsTransitioning)
            {
                float timeRatio = 1 - (m_TimeLoadEndGameScene - Time.time) / SceneTransitionLoadDelay;
                SceneTransitionFadeCanvasGroup.alpha = timeRatio;

                AudioUtility.SetMasterVolume(1 - timeRatio);

                // See if it's time to load the end scene (after the delay)
                if (Time.time >= m_TimeLoadEndGameScene)
                {
                    SceneManager.LoadScene(m_SceneToLoad);
                    SceneIsTransitioning = false;
                }
            }
        }

        public void LoadTargetScene()
        {
            // Remember that we need to load the appropriate end scene after a delay
            SceneIsTransitioning = true;
            SceneTransitionFadeCanvasGroup.gameObject.SetActive(true);

            m_SceneToLoad = SceneName;
            m_TimeLoadEndGameScene = Time.time + SceneTransitionLoadDelay;
        }
    }
}