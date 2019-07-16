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

    public  NCMB.HightScore highScore;
    private bool isNewRecord = false;

    void Start()
    {
        Initialize();

        // ハイスコアを取得する。保存されてなければ0点。
        string name = FindObjectOfType<UserAuth>().CurrentPlayer();
        highScore = new NCMB.HightScore(0.0f, name);
        highScore.Fetch();
    }

    void Update()
    {
      //Debug.Log("ok");

        // スコア・ハイスコアを表示する
        //   scoreGUIText.text = t_Score.ToString();
        //  highScoreGUIText.text = "HighScore : " + highScore.score.ToString();
    }

    // ゲーム開始前の状態に戻す
    private  void Initialize()
    {
        // スコアを0に戻す
      //t_Score = 0;
        //SceneManager.LoadScene("LeaderBoard");
        // ハイスコアを取得する。保存されてなければ0を取得する。
        isNewRecord = false;
    }

    // ポイントの追加
    public  void AddPoint( double time)
    {
        // time = StopWatchGameTimer.SetTime();
        //  Debug.Log("addpoint");
        //Debug.Log("addpoint.time : " + time);
        t_Score = time;
        // スコアがハイスコアより小さければ
        if (highScore.Score > t_Score)
        {
            isNewRecord = true;
            // highScore.score = t_Score;
        }
        Save();
    }

    // ハイスコアの保存
    public  void Save()
    {
          //  Debug.Log("score" + t_Score);
        //Debug.Log("isNewRecord1" + isNewRecord);


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
