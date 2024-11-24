using UnityEngine;
using UnityEngine.UI;

namespace Game.Score
{
    public class ScoreManager : MonoBehaviour
    {
        public Transform player; // Nhân vật để theo dõi
        public Text scoreText;   // Text hiển thị điểm
        private float topScore = 0.0f; // Điểm cao nhất đạt được

        void Update()
        {
            // Cập nhật điểm số theo vị trí nhân vật
            if (player.position.y > topScore)
            {
                topScore = player.position.y;
            }

            // Hiển thị điểm số
            if (scoreText != null)
            {
                scoreText.text = "Score: " + Mathf.Round(topScore).ToString();
            }
        }
    }
}
