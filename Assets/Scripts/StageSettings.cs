using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W���Ƃ̐ݒ�����邽�߂̃X�N���v�g
/// �uStageSettings�v�Ƃ�����̃I�u�W�F�N�g�ɃV�[�����ƂɃA�^�b�`
/// </summary>
public class StageSettings : MonoBehaviour
{
    [Header("�X�e�[�W���̑��u���b�N��")] public int _blockCount;
    [Header("�X�e�[�W���̎��Ԑ���")] public float _timer;
    [Header("�c�@��")] public int life = 3;
    GameManager _gameManager = null;

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
}
