/***************
 * 担　当：小林*
 * 制作日：    *
 * *************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class tScore : MonoBehaviour {

    // スコアを表示するGUIText
    public GUIText scoreGUIText;

    // ハイスコアを表示するGUIText
    public GUIText highScoreGUIText;

    // スコア
    public static  float t_Score;

    public static  float time;

    // ハイスコア
    private static  NCMB.tHightScore highScore;
    private static  bool isNewRecord;

    // PlayerPrefsで保存するためのキー
    //private string highScoreKey = "highScore";

    void Start()
    {
        Initialize();

        // ハイスコアを取得する。保存されてなければ0点。
        string name = FindObjectOfType<UserAuth>().CurrentPlayer();
        highScore = new NCMB.tHightScore(0, name);
        highScore.Fetch();
    }

    void Update()
    {
        // スコアがハイスコアより大きければ
        if (highScore.score < t_Score)
        {
            isNewRecord = !isNewRecord;
            highScore.score = (int)t_Score;
        }

        // スコア・ハイスコアを表示する
        scoreGUIText.text = t_Score.ToString();
        highScoreGUIText.text = "HighScore : " + highScore.score.ToString();
    }

    // ゲーム開始前の状態に戻す
    private static  void Initialize()
    {
        // スコアを0に戻す
        t_Score = 0;
        //SceneManager.LoadScene("LeaderBoard");
        // ハイスコアを取得する。保存されてなければ0を取得する。
        isNewRecord = !isNewRecord;
    }

    // ポイントの追加
    public static void AddPoint()
    {
        time = StopWatchGameTimer.SetTime();
        //Debug.Log("addpoint");
        t_Score = time;
        //Debug.Log("time : " + time);
        //Debug.Log("t_Score : " + t_Score);
        Save();
    }

    // ハイスコアの保存
    public static  void Save()
    {
        // ハイスコアを保存する（ただし記録の更新があったときだけ）
        if (isNewRecord)
        {
            Debug.Log(t_Score);
            highScore.Save();
        }

        // ゲーム開始前の状態に戻す
        Initialize();
    }
}
