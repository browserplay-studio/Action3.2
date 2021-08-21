using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{
    [SerializeField] private int m_TableIndex = 0;
    [SerializeField] private InputField m_ScoreInput = null;

    private string m_TableName = "None";

    private YandexSDK m_SDK = null;

    private void Awake()
    {
        m_SDK = FindObjectOfType<YandexSDK>();

        m_TableName = m_SDK.GetTableName(m_TableIndex);
    }

    public void OnScoreButtonClicked()
    {
        if (int.TryParse(m_ScoreInput.text, out int score))
        {
            m_SDK.SetScore(m_TableName, score);
        }
    }

    public void OnTableButtonClicked()
    {
        m_SDK.ParseLeaderboard(m_TableIndex);
    }
}
