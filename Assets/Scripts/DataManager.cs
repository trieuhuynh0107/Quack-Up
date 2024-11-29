using UnityEngine;

namespace Game.Score
{
    public static class Data_Manager
    {
        private const string HighScoreKey = "HighScore"; // Khóa lưu trữ HighScore

        // Lấy HighScore từ PlayerPrefs
        public static int GetHighScore()
        {
            return PlayerPrefs.GetInt(HighScoreKey, 0); // 0 là giá trị mặc định nếu chưa có
        }

        // Lưu HighScore vào PlayerPrefs
        public static void SetHighScore(int highScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, highScore); // Lưu giá trị
            PlayerPrefs.Save(); // Đảm bảo dữ liệu được ghi lại
        }

        // Reset HighScore (nếu cần)
        public static void ResetHighScore()
        {
            PlayerPrefs.DeleteKey(HighScoreKey); // Xóa khóa lưu trữ
            PlayerPrefs.Save();
        }
    }
}
