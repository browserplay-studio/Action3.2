using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthButton : MonoBehaviour
{
    private YandexSDK m_SDK = null;

    private void Awake()
    {
        m_SDK = FindObjectOfType<YandexSDK>();
    }

    private void OnEnable()
    {
        m_SDK.OnPlayerInitialized += OnPlayerInitialized;
    }

    private void OnDisable()
    {
        m_SDK.OnPlayerInitialized -= OnPlayerInitialized;
    }

    private void OnPlayerInitialized(bool status)
    {
        if (status)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("произошла ошибка при авторизации пользователя");
        }
    }
}
