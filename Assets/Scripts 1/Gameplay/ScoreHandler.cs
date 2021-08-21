using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private Text m_TimeLabel = null;
    [SerializeField] private int m_TableIndex = 0;
    
    private PlayerController2D m_Player = null;

    private bool m_IsStarted = false;
    private bool m_IsFinished = false;

    private float m_ElapsedTime = 0;

    private void Awake()
    {
        m_Player = FindObjectOfType<PlayerController2D>();
    }

    private void OnEnable()
    {
        m_Player.OnCollisionStart += OnPlayerCollisionStart;
        m_Player.OnCollisionFinish += OnPlayerCollisionFinish;
    }

    private void OnDisable()
    {
        m_Player.OnCollisionStart -= OnPlayerCollisionStart;
        m_Player.OnCollisionFinish -= OnPlayerCollisionFinish;
    }

    private void Update()
    {
        if (m_IsStarted && !m_IsFinished)
        {
            m_ElapsedTime += Time.deltaTime;

            m_TimeLabel.text = TableWindow.FormatTime(m_ElapsedTime);
        }
    }

    private void OnPlayerCollisionStart()
    {
        if (!m_IsStarted)
        {
            m_IsStarted = true;
        }
    }

    private void OnPlayerCollisionFinish()
    {
        if (m_IsStarted && !m_IsFinished)
        {
            m_IsFinished = true;

            SetScore();
        }
    }

    private void SetScore()
    {
        var sdk = FindObjectOfType<YandexSDK>();
        string tableName = sdk.GetTableName(m_TableIndex);
        sdk.SetTimeScore(tableName, m_ElapsedTime);
    }
}
