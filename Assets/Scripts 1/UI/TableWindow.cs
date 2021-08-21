using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TableWindow : MonoBehaviour
{
    [SerializeField] private int m_TableIndex = 0;
    [SerializeField] private YandexSDK.TableMode m_TableMode = YandexSDK.TableMode.Numeric;
    [Space]
    [SerializeField] private Transform m_ElementsParent = null;
    [SerializeField] private TableElement m_ElementPrefab = null;

    private string m_TableName = "None";

    private YandexSDK m_SDK = null;

    private void Awake()
    {
        m_SDK = FindObjectOfType<YandexSDK>();

        m_TableName = m_SDK.GetTableName(m_TableIndex);

        DestroyChildrens();
    }

    private void OnEnable()
    {
        m_SDK.OnLeaderboardFetched += OnLeaderboardFetched;
    }

    private void OnDisable()
    {
        m_SDK.OnLeaderboardFetched -= OnLeaderboardFetched;
    }

    private void Start()
    {
        m_SDK.ParseLeaderboard(m_TableIndex);
    }

    private void OnLeaderboardFetched(YandexSDK.OnLeaderboardEntriesFetchedEventArgs args)
    {
        if (args.Data.tableName != m_TableName) return;

        //Debug.Log(gameObject.name);
        Debug.Log(args.Json);

        DestroyChildrens();

        for (int i = 0; i < args.Data.entries.Count; i++)
        {
            var entry = args.Data.entries[i];

            TableElement element = Instantiate(m_ElementPrefab, m_ElementsParent);
            element.Init(m_TableMode);
            element.UpdateVisual(entry);
        }
    }

    private void DestroyChildrens()
    {
        for (int i = m_ElementsParent.childCount - 1; i >= 0; i--)
        {
            var child = m_ElementsParent.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    public static string FormatTime(float time)
    {
        int value = Mathf.RoundToInt(time * 1000);

        return FormatScore(value);
    }

    public static string FormatScore(int value)
    {
        // @"hh\:mm\:ss\.fff"

        // 59921 - 59 seconds * 1000 = value in ms
        // 00:59.921

        string text = new TimeSpan(0, 0, 0, 0, value).ToString(@"mm\:ss\.fff");

        return text;
    }
}
