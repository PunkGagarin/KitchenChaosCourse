using UnityEngine.SceneManagement;

namespace Menu
{

    public static class Loader
    {

        public static SceneNames levelToLoad;


        public static void Load(SceneNames levelToLoad)
        {
            Loader.levelToLoad = levelToLoad;
            SceneManager.LoadSceneAsync(SceneNames.Loading.ToString());
        }


        public static void LoadingCallback()
        {
            SceneManager.LoadScene(levelToLoad.ToString());
        }
    }


}