using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージごとの設定をするためのスクリプト
/// 「StageSettings」という空のオブジェクトにシーンごとにアタッチ
/// </summary>
public class StageSettings : MonoBehaviour
{
    [Header("ステージ内の総ブロック数")] public int _blockCount;
    [Header("ステージ内の時間制限")] public float _timer;
    [Header("残機数")] public int life = 3;
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
