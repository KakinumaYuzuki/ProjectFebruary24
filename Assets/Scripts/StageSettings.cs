using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

/// <summary>
/// ステージごとの設定をするためのスクリプト
/// 「StageSettings」という空のオブジェクトにシーンごとにアタッチ
/// </summary>
public class StageSettings : MonoBehaviour
{
    GameManager _gameManager = null;
    [Tooltip("ステージ内の総ブロック数")]
    [SerializeField]int _blockCount;
    [Tooltip("ステージ内の時間制限")]
    [SerializeField]float _timer;
    [Tooltip("残機数")]
    [SerializeField]int _life = 3;
    // sutate変更
    //[Tooltip("最初のゲームステート")]
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
