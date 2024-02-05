using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// シーンをロードする
/// ボタンにアタッチ
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [Tooltip("ロードするシーン名")]
    [SerializeField] string _sceneName;
    [Tooltip("画面が暗くなるまでのスピード 大きい方が早い")]
    [SerializeField] float _fadeSpeed = 0.7f;
    Image _fadePanel = null; // フェード用のパネル Raycast Targetの設定をすること 
    bool _loading = false;  // ロード開始フラグ

    void Start()
    {
        var fadePanelObject = GameObject.Find("FadePanel");
        _fadePanel = fadePanelObject.GetComponent<Image>();
    }


    void Update()
    {
        if (_loading)
        {
            if (_fadePanel != null)
            {
                Color panelColor = _fadePanel.color;
                panelColor.a += _fadeSpeed * Time.deltaTime;
                _fadePanel.color = panelColor;

                if (panelColor.a > 1f)
                {
                    SceneManager.LoadScene(_sceneName);
                    _loading = false;
                    Debug.Log(_loading);
                }
            }
            else
            {
                Debug.Log("フェード用のパネルが設定されていません");
            }
        }
    }

    /*public void TimeStart()
    {
        Time.timeScale = 1;
    }*/

    public void LoadScene()
    {
        _loading = true;
    }

    public void LoadScene(string sceneName)
    {
        _loading = true;
        _sceneName = sceneName;
    }
}
