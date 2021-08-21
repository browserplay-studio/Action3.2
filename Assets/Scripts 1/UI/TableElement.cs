using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TableElement : MonoBehaviour
{
    [SerializeField] private Text m_Label = null;

    private YandexSDK.TableMode m_TableMode = YandexSDK.TableMode.Numeric;

    public void Init(YandexSDK.TableMode mode)
    {
        m_TableMode = mode;
    }

    public void UpdateVisual(YandexSDK.LeaderboardEntry entry)
    {
        string text = "!";

        if (m_TableMode == YandexSDK.TableMode.Numeric)
        {
            text = $"{entry.name}: {entry.score}";
        }
        else if (m_TableMode == YandexSDK.TableMode.Time)
        {
            text = $"{entry.name}: {TableWindow.FormatScore(entry.score)}";
        }

        m_Label.text = text;
    }
}
