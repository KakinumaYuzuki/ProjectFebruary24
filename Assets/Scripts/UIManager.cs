using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIを管理するコンポーネント
/// StageSettingにアタッチする
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("残機モード")]
    //[SerializeField]

    [Header("時間モード")]

    GameManager _gameManager;
    [Header("表示するパネル")]
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
            // ポーズの時
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
            // ゲームオーバーの時
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
            // ゲームクリアの時
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
