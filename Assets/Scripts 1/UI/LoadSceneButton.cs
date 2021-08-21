using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private int m_Index = 0;

    private Button m_Button = null;

    private void Awake()
    {
        m_Button = GetComponent<Button>();

        m_Button.onClick.AddListener(Load);
    }

    private void Load()
    {
        SceneManager.LoadScene(m_Index);
    }
}
