using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace dotmob
{

    public partial class GameManager : Singleton<GameManager>
    {
        public static int TOTAL_GAME_COUNT
        {
            get => PrefManager.GetInt(nameof(TOTAL_GAME_COUNT));
            set => PrefManager.SetInt(nameof(TOTAL_GAME_COUNT),value);
        }

        public static LoadGameData LoadGameData { get; set; }


        protected override void OnInit()
        {
            base.OnInit();
            Application.targetFrameRate = 60;
        }
    }

    public partial class GameManager
    {
        // ReSharper disable once FlagArgument
        public static void LoadScene(string sceneName, bool showLoading = true, float loadingScreenSpeed = 5f)
        {
            var loadingPanel = SharedUIManager.LoadingPanel;
            if (showLoading && loadingPanel != null)
            {
                loadingPanel.Speed = loadingScreenSpeed;
                loadingPanel.Show(completed: () =>
                {
                    SceneManager.LoadSceneAsync(sceneName);
                    loadingPanel.Hide();
                });
            }
            else
            {
                SceneManager.LoadSceneAsync(sceneName);
            }
        }
        public static IEnumerator LoadSceneDelay(string sceneName, bool showLoading = true, float loadingScreenSpeed = 5f)
        {

            var loadingPanel = SharedUIManager.LoadingPanel;
            if (showLoading && loadingPanel != null)
            {
                loadingPanel.Speed = loadingScreenSpeed;
                loadingPanel.Show(completed: () =>
                {
                    //SceneManager.LoadSceneAsync(sceneName);
                    //loadingPanel.Hide();
                });
            }
            //else
            //{
            //    SceneManager.LoadSceneAsync(sceneName);
            //}
            yield return new WaitForSeconds(0.1f);
            SceneManager.LoadSceneAsync(sceneName);
            loadingPanel.Hide();

        }

        public static void LoadGame(LoadGameData data, bool showLoading = true, float loadingScreenSpeed = 1f)
        {
            LoadGameData = data;
            LoadScene("Main", showLoading, loadingScreenSpeed);
        }
        public static void SetMode(LoadGameData data, bool showLoading = true, float loadingScreenSpeed = 1f)
        {
            LoadGameData = data;
        }
    }
}

public struct LoadGameData
{
    public GameMode GameMode { get; set; }
    public Level Level { get; set; }
}