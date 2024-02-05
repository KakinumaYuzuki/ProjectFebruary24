using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFadePanel : MonoBehaviour
{
    [Tooltip("画面が明るくなるまでのスピード　大きい方が早い")]
    [SerializeField] float _fadeSpeed = 0.7f;
    Image _fadePanel;
    public bool _sceneLoading = true;    // ゲームスタートはこれがfalseになったら

    void Start()
    {
        //var fadePanelObject = GameObject.Find("FadePanel");
        _fadePanel = GetComponent<Image>();
        _fadePanel.color = Color.black;
    }

    void Update()
    {
        if (_fadePanel != null)
        {
            if (_sceneLoading)
            {
                Color panelColor = _fadePanel.color;
                panelColor.a -= _fadeSpeed * Time.deltaTime;
                _fadePanel.color = panelColor;
                if (panelColor.a < 0f)
                {
                    _sceneLoading = false;
                }
            }
        }

    }
}
