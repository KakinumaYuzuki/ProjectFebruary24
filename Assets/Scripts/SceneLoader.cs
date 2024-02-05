using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// �V�[�������[�h����
/// �{�^���ɃA�^�b�`
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [Tooltip("���[�h����V�[����")]
    [SerializeField] string _sceneName;
    [Tooltip("��ʂ��Â��Ȃ�܂ł̃X�s�[�h �傫����������")]
    [SerializeField] float _fadeSpeed = 0.7f;
    Image _fadePanel = null; // �t�F�[�h�p�̃p�l�� Raycast Target�̐ݒ�����邱�� 
    bool _loading = false;  // ���[�h�J�n�t���O

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
                Debug.Log("�t�F�[�h�p�̃p�l�����ݒ肳��Ă��܂���");
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
