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
    public float t_Score;

    // ハイスコア
    //private int highScore;

    private NCMB.tHightScore highScore;
    private bool isNewRecord;

    // PlayerPrefsで保存するためのキー
    //private string highScoreKey = "highScore";

    void Start()
    {
        Initialize();

        // ハイスコアを取得する。保存されてなければ0点。
        string name = FindObjectOfType<UserAuth>().currentPlayer();
        highScore = new NCMB.tHightScore(0, name);
        highScore.fetch();
    }

    void Update()
    {
        // スコアがハイスコアより大きければ
        if (highScore.score < t_Score)
        {
            isNewRecord = true;
            highScore.score = (int)t_Score;
        }

        // スコア・ハイスコアを表示する
        scoreGUIText.text = t_Score.ToString();
        highScoreGUIText.text = "HighScore : " + highScore.score.ToString();
    }

    // ゲーム開始前の状態に戻す
    private void Initialize()
    {
        // スコアを0に戻す
        t_Score = 0;
        //SceneManager.LoadScene("LeaderBoard");
        // ハイスコアを取得する。保存されてなければ0を取得する。
        isNewRecord = false;
    }

    // ポイントの追加
    public void AddPoint(float point)
    {
        t_Score = t_Score + point;
    }

    // ハイスコアの保存
    public void Save()
    {
        // ハイスコアを保存する（ただし記録の更新があったときだけ）
        if (isNewRecord)
        {
            highScore.save();
        }

        // ゲーム開始前の状態に戻す
        Initialize();
    }
}
