using System.IO;
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

    [System.Serializable]
    private class PlayerData
    {
        public int highScore = 0;
    }
}
