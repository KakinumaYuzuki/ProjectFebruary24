using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI���Ǘ�����R���|�[�l���g
/// StageSetting�ɃA�^�b�`����
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("�c�@���[�h")]
    //[SerializeField]

    [Header("���ԃ��[�h")]

    GameManager _gameManager;
    [Header("�\������p�l��")]
    [SerializeField] GameObject _pausePanel;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _gameClearPanel;

    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (_pausePanel)
        {
            // �|�[�Y�̎�
            if (_gameManager.GetGameState() == GameManager.GameState.Pause)
            {
                _pausePanel.SetActive(true);
            }
            else if (_gameManager.GetGameState() == GameManager.GameState.Standby || _gameManager.GetGameState() == GameManager.GameState.Play)
            {
                _pausePanel.SetActive(false);
            }
        }

        if (_gameOverPanel)
        {
            // �Q�[���I�[�o�[�̎�
            if (_gameManager.GetGameState() == GameManager.GameState.GameOver)
            {
                _gameOverPanel.SetActive(true);
            }
            else if (_gameManager.GetGameState() == GameManager.GameState.Standby || _gameManager.GetGameState() == GameManager.GameState.Play)
            {
                _gameOverPanel.SetActive(false);
            }
        }

        if (_gameClearPanel)
        {
            // �Q�[���N���A�̎�
            if (_gameManager.GetGameState() == GameManager.GameState.GameClear)
            {
                _gameClearPanel.SetActive(true);
            }
            else if (_gameManager.GetGameState() == GameManager.GameState.Standby || _gameManager.GetGameState() == GameManager.GameState.Play)
            {
                _gameClearPanel.SetActive(false);
            }
        }
    }
}
