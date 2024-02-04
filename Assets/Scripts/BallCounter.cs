using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 残機表示を行うコンポーネント
/// 管理オブジェクトにアタッチして使う
/// </summary>
public class BallCounter : MonoBehaviour
{
    [Tooltip("残機として表示するスプライト")]
    [SerializeField] Sprite _ballUISprite = null;
    [Tooltip("残機として表示するスプライトのサイズ")]
    [SerializeField] Vector2 _spriteSize = new Vector2(150f, 150f);
    [Tooltip("残機表示をするパネル")]
    [SerializeField] RectTransform _ballCounterPanel = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 残機表示を更新する
    /// </summary>
    /// <param name="ballCount"></param>
    public void Refresh(int ballCount)
    {
        if (_ballUISprite != null && _ballCounterPanel != null)
        {
            foreach (Transform t in _ballCounterPanel.transform)
            {
                Destroy(t.gameObject);
            }
            Debug.Log("リフレッシュ");
            // 残機数だけスプライトをパネルの子オブジェクトとして生成
            for (int i = 0; i < ballCount - 1; i++)
            {
                // Imageを作る
                GameObject go = new GameObject();
                Image image = go.AddComponent<Image>();
                // Spriteをアサインする
                image.sprite = _ballUISprite;
                // サイズを変える
                RectTransform rect = go.GetComponent<RectTransform>();
                rect.sizeDelta = _spriteSize;
                // パネルの子オブジェクトにする
                go.transform.SetParent(_ballCounterPanel.transform);
            }
        }
    }
}
