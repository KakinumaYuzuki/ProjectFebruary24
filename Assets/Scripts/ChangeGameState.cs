using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

/// <summary>
/// �V�[���J�ڎ��ɃQ�[���X�e�[�g��ύX���邽�߂̃R���|�[�l���g
/// �{�^���ɃA�^�b�`���ăN���b�N���Ɋ֐����Ăяo��
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
    }   // ���̂Ƃ���Loading�Ŕ��肵�Ă�����̂͂Ȃ�����|�[�Y����Standby�ɑJ�ڂ���OK
}
