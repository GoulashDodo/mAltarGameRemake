using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using mAltarGameRemake.Scripts.Utils;

namespace mAltarGameRemake.Scripts
{
    public class GameEntryPoint
    {

        private static GameEntryPoint _instance;
        private Coroutines _coroutines;
        private UIRootView _UIRoot;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutoStartGame()
        {
            _instance = new GameEntryPoint();

            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            _coroutines = new GameObject("[COURUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);

            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");

            _UIRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_UIRoot.gameObject);


        }

        private void RunGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.GAMEPLAY)
            {
                _coroutines.StartCoroutine(LoadAndStartGameplay());
                return;
            }

            if(sceneName != Scenes.BOOT)
            {
                return;
            }

#endif

            _coroutines.StartCoroutine(LoadAndStartGameplay());


        }

        private IEnumerator LoadAndStartGameplay()
        {
            _UIRoot.ShowLoadingScreen();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(2);


            GameplayEntryRoot sceneEntryRoot = Object.FindFirstObjectByType<GameplayEntryRoot>();
            sceneEntryRoot.Run();


            _UIRoot.HideLoadingScreen();

        }
        

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }

    }
}

