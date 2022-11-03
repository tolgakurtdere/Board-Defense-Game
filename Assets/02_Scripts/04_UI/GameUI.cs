using TMPro;
using UnityEngine;

namespace TK.UI
{
    public class GameUI : UI
    {
        [SerializeField] private TextMeshProUGUI levelCountText;

        public void SetLevelCountText(string levelText)
        {
            levelCountText.text = levelText;
        }
    }
}