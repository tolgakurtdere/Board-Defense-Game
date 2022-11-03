using TK.Utility;
using UnityEngine;

namespace TK.Manager
{
    public class PrefsManager : MonoSingleton<PrefsManager>
    {
        [SerializeField] private int levelIndex;

        private const string LevelIndexKey = "com.tk.levelindex";

        protected override void Awake()
        {
            base.Awake();
            levelIndex = PlayerPrefs.GetInt(LevelIndexKey);
        }

        private void SetLevelIndex(int index)
        {
            this.levelIndex = index;
            PlayerPrefs.SetInt(LevelIndexKey, this.levelIndex);
        }

        public int GetLevelIndex()
        {
            return levelIndex;
        }

        public void IncrementLevelIndex()
        {
            SetLevelIndex(levelIndex + 1);
        }
    }
}