using System;
using System.Collections.Generic;
using System.Linq;
using TK.Utility;
using UnityEngine;

namespace TK.Manager
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        public static event Action OnLevelLoaded;
        public static event Action OnLevelStarted;
        public static event Action<bool> OnLevelStopped;

        [SerializeField] private List<Level> levelPrefabs = new();

        public static Level CurrentLevel;
        public static Transform Thrash;

        private CyclingList<Level> _levels;

        public static int TotalLevelCount => Instance.levelPrefabs.Count;

        protected override void Awake()
        {
            base.Awake();

            //Initialize level list and loads the prefabs
            _levels = new CyclingList<Level>(levelPrefabs);

            //Create the thrash
            Thrash = new GameObject("Thrash").transform;
        }

        private void Start()
        {
            LoadLevel();
        }

        public void LoadLevel(bool nextLevel = false)
        {
            if (!levelPrefabs.Any()) return;
            if (nextLevel) PrefsManager.Instance.IncrementLevelIndex();

            ClearThrash();

            var levelToLoad = _levels.GetElement(PrefsManager.Instance.GetLevelIndex());
            CurrentLevel = Instantiate(levelToLoad, Vector3.zero, Quaternion.identity, Thrash);

            UIManager.GameUI.SetLevelCountText("Level " + (PrefsManager.Instance.GetLevelIndex() + 1));
            UIManager.HomeUI.Show();

            OnLevelLoaded?.Invoke();
        }

        public static void StartLevel()
        {
            if (GameManager.IsPlaying) return;
            GameManager.IsPlaying = true;

            OnLevelStarted?.Invoke();
        }

        public static void StopLevel(bool isSuccess)
        {
            if (!GameManager.IsPlaying) return;
            GameManager.IsPlaying = false;

            if (isSuccess) //level succeed
            {
                UIManager.LevelCompletedUI.Show();
            }
            else //level failed
            {
                UIManager.LevelFailedUI.Show();
            }

            OnLevelStopped?.Invoke(isSuccess);
        }

        private static void ClearThrash()
        {
            var count = Thrash.childCount;
            for (var i = 0; i < count; i++)
            {
                Destroy(Thrash.GetChild(i).gameObject);
            }
        }
    }
}