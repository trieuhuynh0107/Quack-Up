using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class Data_Manager
{
    private static string saveFilePath = Application.persistentDataPath + "/playerData.json"; // File lưu trữ
    private static PlayerData playerData = new PlayerData(); // Cấu trúc dữ liệu mặc định

    // Lưu dữ liệu vào file
    public static void SaveData()
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, json);
    }

    // Tải dữ liệu từ file
    public static void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
    }

    public static void SetHighScore(int score)
    {
        if (score > playerData.highScore)
        {
            playerData.highScore = score;
            SaveData();
        }
    }

    public static int GetHighScore()
    {
        return playerData.highScore;
    }

    public static List<(string, int)> GetTopScores()
    {
        // Chuyển danh sách lưu trữ sang danh sách dễ sử dụng
        List<(string, int)> topScores = new List<(string, int)>();
        foreach (var entry in playerData.topScores)
        {
            topScores.Add((entry.name, entry.score));
        }
        return topScores;
    }

    public static void AddToTopScores(string name, int score)
    {
        playerData.topScores.Add(new ScoreEntry { name = name, score = score });

        // Sắp xếp theo điểm giảm dần
        playerData.topScores.Sort((a, b) => b.score.CompareTo(a.score));

        // Giữ lại top 5
        if (playerData.topScores.Count > 5)
        {
            playerData.topScores.RemoveRange(5, playerData.topScores.Count - 5);
        }

        SaveData();
    }

    [System.Serializable]
    private class PlayerData
    {
        public int highScore = 0;
        public List<ScoreEntry> topScores = new List<ScoreEntry>(); // Danh sách điểm cao
    }

    [System.Serializable]
    private class ScoreEntry
    {
        public string name; // Tên người chơi
        public int score; // Điểm số
    }
}
