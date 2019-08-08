/***************
 * 担　当：小林*
 * 制作日：    *
 * *************/

//20190719 長嶋　確認

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System;

public class ScoreManager : MonoBehaviour {

    // スコア
    private double timeScore { get; set; }
    private NCMB.HighScore highScore;
    private bool isNewRecord;

    void Start()
    {
        Initialize();

        isNewRecord = false;

        // ハイスコアを取得する。保存されてなければ0点。
        string name = FindObjectOfType<UserAuth>().CurrentPlayer();
        highScore = new NCMB.HighScore(0.0f, name);
        highScore.SarverFetch();
    }

    /// <summary>
    /// @brief ゲーム開始前の状態に戻す
    /// </summary>
    private void Initialize()
    {
        // ハイスコアを取得する。保存されてなければ0を取得する。
        isNewRecord = false;
    }

    /// <summary>
    /// @brief timeScoreにゲームで得たスコアを代入し、ハイスコアを更新していれば保存関数を呼び出す。
    /// </summary>
    /// <param name="time">StopWatchGameTimerのjudgの値が格納されている</param>
    public void AddScore( double time)
    {
        timeScore = time;
        // スコアがハイスコアより小さければ
        if (highScore.GameScore > timeScore)
        {
            isNewRecord = true;
        }
        CallSave();
    }

    /// <summary>
    /// @brief ハイスコア保存関数の呼び出し
    /// </summary>
    public void CallSave()
    {

        // ハイスコアを保存する（ただし記録の更新があったときだけ）
        if (isNewRecord)
        {
            // Debug.Log("isNewRecord2"+ isNewRecord );
            highScore.ServerSave(timeScore);
        }

        // ゲーム開始前の状態に戻す
        Initialize();
    }
}
