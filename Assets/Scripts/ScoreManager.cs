using UnityEngine;
using TMPro;

namespace Game.Score
{
    public class ScoreManager : MonoBehaviour
    {
        public Transform player; // Theo dõi nhân vật
        public TextMeshProUGUI scoreText; // Hiển thị điểm hiện tại
        private float topScore = 0.0f; // Điểm cao nhất đạt được trong lần chơi hiện tại

        void Update()
        {
            // Cập nhật điểm số dựa trên vị trí nhân vật
            if (player.position.y > topScore)
            {
                topScore = player.position.y;
            }

            // Hiển thị điểm hiện tại
            if (scoreText != null)
            {
                scoreText.text = Mathf.Round(topScore).ToString();
            }
        }

        // Trả về điểm hiện tại để GameOverManager sử dụng
        public int GetCurrentScore()
        {
            return Mathf.RoundToInt(topScore);
        }
    }
}
