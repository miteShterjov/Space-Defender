using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI highScoreText;
    public static ScoreKeeper Instance;
    private int killCounter = 0;

    private const int MaxHighScores = 5;
    private List<int> highScores = new List<int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object across scenes
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        LoadHighScores();
    }

    private void LoadHighScores()
    {
        highScores.Clear();
        int count = PlayerPrefs.GetInt("HighScoreCount", 0);
        for (int i = 0; i < count; i++)
        {
            highScores.Add(PlayerPrefs.GetInt("HighScore" + i, 0));
        }
    }

    public void AddNewScore(int newScore)
    {
        highScores.Add(newScore);
        highScores.Sort((a, b) => b.CompareTo(a)); // Sort descending
        if (highScores.Count > MaxHighScores)
            highScores.RemoveAt(highScores.Count - 1); // Remove lowest

        SaveHighScores();
    }

    private void SaveHighScores()
    {
        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }

        PlayerPrefs.SetInt("HighScoreCount", highScores.Count);
        PlayerPrefs.Save();
    }

    public void DisplayHighScores()
    {
        for (int i = 0; i < highScores.Count; i++)
        {
            if (i < highScores.Count)
                highScoreText.text = $"#{i + 1}: {highScores[i]}\n";
            else
                highScoreText.text = $"#{i + 1}: ---";
        }
    }

    public List<int> GetHighScores()
    {
        return new List<int>(highScores); // Return a copy of the list
    }

    public int GetScore() => killCounter;
    public void AddScore() => killCounter++;
    public void ResetScore() => killCounter = 0;

}
