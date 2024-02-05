using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

/// <summary>
/// シーン遷移時にゲームステートを変更するためのコンポーネント
/// ボタンにアタッチしてクリック時に関数を呼び出す
/// </summary>
public class ChangeGameState : MonoBehaviour
{
    GameManager _gameManager;
    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    void Update()
    {
        
    }

    public void GoToTitle()
    {
        _gameManager.ChangeGameState(GameState.Title);
    }

    public void GoToStageSelect()
    {
        _gameManager.ChangeGameState(GameState.StageSelect);
    }

    public void GoToLoading()
    {
        _gameManager.ChangeGameState(GameState.Loading);
    }

    public void GoToStandby()
    {
        _gameManager.ChangeGameState(GameState.Standby);
    }   // 今のところLoadingで判定しているものはないからポーズからStandbyに遷移してOK
}
