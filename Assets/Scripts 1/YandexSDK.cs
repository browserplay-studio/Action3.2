using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexSDK : MonoBehaviour
{
    public enum TableMode
    {
        Numeric,
        Time
    }

    public class OnLeaderboardEntriesFetchedEventArgs
    {
        public string Json { get; }
        public LeaderboardData Data { get; }

        public OnLeaderboardEntriesFetchedEventArgs(string json, LeaderboardData data)
        {
            Json = json;
            Data = data;
        }
    }

    public event Action<OnLeaderboardEntriesFetchedEventArgs> OnLeaderboardFetched = null;

    public event Action<bool> OnPlayerInitialized = null;

    [Serializable]
    public class LeaderboardData
    {
        public string tableName = "None";
        public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
    }

    [Serializable]
    public class LeaderboardEntry
    {
        public string name;
        public int score;
    }

    [DllImport("__Internal")]
    private static extern void GetLeaderboardEntries(string tableName);

    [DllImport("__Internal")]
    private static extern void SetLeaderboardScore(string tableName, int score);

    [DllImport("__Internal")]
    private static extern string GetTableByIndex(int index);

    [DllImport("__Internal")]
    private static extern string InitializePlayer();

    [Header("Debug")]
    [SerializeField] private TextAsset[] m_Tables = null;

    public bool IsPlayerInitialized { get; private set; }

    public void InitPlayer()
    {
#if UNITY_EDITOR
        HandlePlayerInit(1);
#elif UNITY_WEBGL
        InitializePlayer();
#endif
    }

    public void ParseLeaderboard(int index)
    {
#if UNITY_EDITOR
        string json = m_Tables[index].text;
        json = json.Replace("\n", "");
        json = json.Replace(" ", "");

        HandleLeaderboard(json);
#elif UNITY_WEBGL
        string tableName = GetTableName(index);
        GetLeaderboardEntries(tableName);
#endif
    }

    public void SetScore(string tableName, int score)
    {
#if UNITY_EDITOR
        Debug.Log($"SetScore: {tableName}, {score}");
#elif UNITY_WEBGL
        SetLeaderboardScore(tableName, score);
#endif
    }

    public void SetTimeScore(string tableName, float timeScore)
    {
        int score = Mathf.RoundToInt(timeScore * 1000);
        SetScore(tableName, score);
    }

    public string GetTableName(int index)
    {
#if UNITY_EDITOR
        return $"table{index}";
#elif UNITY_WEBGL
        return GetTableByIndex(index);
#endif
    }

    private void HandleLeaderboard(string json)
    {
        var data = JsonUtility.FromJson<LeaderboardData>(json);

        var args = new OnLeaderboardEntriesFetchedEventArgs(json, data);
        OnLeaderboardFetched?.Invoke(args);
    }

    private void HandlePlayerInit(int value)
    {
        IsPlayerInitialized = !(value == 0);

        OnPlayerInitialized?.Invoke(IsPlayerInitialized);
    }
}
