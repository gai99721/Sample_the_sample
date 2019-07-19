/***************
 * 担　当：小林*
 * 制作日：    *
 * *************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class Score : MonoBehaviour {

    // スコア
    public  double t_Score;
    public  double time = 10;

    public  NCMB.HighScore highScore;
    private bool isNewRecord = false;

    void Start()
    {
        Initialize();

        // ハイスコアを取得する。保存されてなければ0点。
        string name = FindObjectOfType<UserAuth>().CurrentPlayer();
        highScore = new NCMB.HighScore(0.0f, name);
        highScore.Fetch();
    }

    // ゲーム開始前の状態に戻す
    private  void Initialize()
    {
        // ハイスコアを取得する。保存されてなければ0を取得する。
        isNewRecord = false;
    }

    // ポイントの追加
    public  void AddPoint( double time)
    {
        t_Score = time;
        // スコアがハイスコアより小さければ
        if (highScore.Score > t_Score)
        {
            isNewRecord = true;
        }
        Save();
    }

    // ハイスコアの保存
    public  void Save()
    {

        // ハイスコアを保存する（ただし記録の更新があったときだけ）
        if (isNewRecord==true)
        {
             // Debug.Log("isNewRecord2"+ isNewRecord );
            highScore.Save(t_Score);
        }

        // ゲーム開始前の状態に戻す
        Initialize();
    }
}
