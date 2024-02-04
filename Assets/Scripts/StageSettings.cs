using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

/// <summary>
/// �X�e�[�W���Ƃ̐ݒ�����邽�߂̃X�N���v�g
/// �uStageSettings�v�Ƃ�����̃I�u�W�F�N�g�ɃV�[�����ƂɃA�^�b�`
/// </summary>
public class StageSettings : MonoBehaviour
{
    GameManager _gameManager = null;
    [Tooltip("�X�e�[�W���̑��u���b�N��")]
    [SerializeField]int _blockCount;
    [Tooltip("�X�e�[�W���̎��Ԑ���")]
    [SerializeField]float _timer;
    [Tooltip("�c�@��")]
    [SerializeField]int _life = 3;
    // sutate�ύX
    //[Tooltip("�ŏ��̃Q�[���X�e�[�g")]
    //[SerializeField] GameState _settingGameState;
    private void Awake()
    {
        _gameManager = GameManager.Instance;
        //_gameManager.ResetGame();
    }

    void Start()
    {
        //GameManager.GameState.InStage;

    }

    void Update()
    {
        
    }

    public int GetStageBlockCount()
    {
        return _blockCount;
    }

    public float GetStageTimeLimit()
    {
        return _timer;
    }
    
    public int GetStageLife()
    {
        return _life;
    }

    /*public GameState GetSettingGameState()
    {
        return _settingGameState;
    }*/

}
