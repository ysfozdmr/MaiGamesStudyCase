using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fenrir.Actors;
using Fenrir.EventBehaviour;

namespace Fenrir.Managers
{
    [RequireComponent(typeof(DataManager))]
    public class GameManager : EventBehaviour<GameManager>
    {
        public Runtime runtime;
        private int level;
        private bool isRestart;
        private bool isFirstLoaded;

        private void Start()
        {
            FirstLoad();
        }

        private void FirstLoad()
        {
            isFirstLoaded = true;
            level = PlayerPrefs.GetInt("UILevel", 1);
            runtime.currentLevelIndex = PlayerPrefs.GetInt("level", 0);
            LoadLevel(false, true);
        }

        private void LoadLevel(bool isRestarted, bool isFirstLoad)
        {
            if (runtime.currentLevel)
            {
                Destroy(runtime.currentLevel.gameObject);
            }

            RandomLevel(isRestarted,isFirstLoad);

            GameObject createdLevel =
                Instantiate(DataManager.Instance.levelCapsule.LevelPrefab(runtime.currentLevelIndex));
            if (createdLevel.TryGetComponent(out LevelActor levelActor))
            {
                runtime.currentLevel = levelActor;
                levelActor.SetupLevel();
                runtime.isGameOver = false;
                PushEvent(BaseGameEvents.LevelLoaded);
            }
        }

        void RandomLevel(bool isRestarted,bool isFirstLoad)
        {
            if (level > 10)
            {
                if (!isRestarted&&!isFirstLoad)
                {
                    int randomLevelIndex;
                    do
                    {
                        randomLevelIndex = Random.Range(0, 9);
                    } while (randomLevelIndex == runtime.currentLevelIndex);

                    runtime.currentLevelIndex = randomLevelIndex;
                    PlayerPrefs.SetInt("level",randomLevelIndex);
                }
            }
        }

        public void StartLevel()
        {
            if (!runtime.isGameStarted)
            {
                runtime.isGameStarted = true;
                PushEvent(BaseGameEvents.StartGame);
            }
            else
            {
                throw new System.ApplicationException("Level Already Started");
            }
        }

        public void FinishLevel(bool status)
        {
            runtime.isGameStarted = false;
            runtime.isGameOver = true;
            DataManager.Instance.PlayersBalls.Clear();
            PushEvent(BaseGameEvents.FinishGame);
            PushEvent(status ? BaseGameEvents.WinGame : BaseGameEvents.LoseGame);

            if (status)
            {
                runtime.currentLevelIndex++;
                PlayerPrefs.SetInt("level", runtime.currentLevelIndex);
            }
        }

        public void NextLevel()
        {
            isFirstLoaded = false;
            isRestart = false;
            level++;
            PlayerPrefs.SetInt("UILevel", level);
            PushEvent(BaseGameEvents.NextLevel);
            LoadLevel(isRestart, isFirstLoaded);
        }

        public void RestartLevel()
        {
            isFirstLoaded = false;
            isRestart = true;
            PushEvent(BaseGameEvents.RestartGame);
            LoadLevel(isRestart, isFirstLoaded);
        }

        [System.Serializable]
        public struct Runtime
        {
            public bool isGameStarted;
            public bool isGameOver;
            public int currentLevelIndex;

            public LevelActor currentLevel;
        }
    }
}