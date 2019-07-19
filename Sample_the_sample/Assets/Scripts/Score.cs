/***************
 * 担　当：小林*
 * 制作日：    *
 * *************/

//20190719 長嶋　確認

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

//@Nagashima クラス名を２単語以上にしてください　例 ScoreManager
//@Nagashima クラス名を変更した場合、ソースファイル名を変更することを忘れないでください
public class Score : MonoBehaviour {

    // スコア
    //@Nagashima 変数名は2単語以上にしてください　timeScore
    //@Nagashima 変数名に_を使用しないでください
    //@Nagashima メンバ変数は基本的にprivateを使用してください。他クラスで使用したい場合はアクセサーを使用してください
    //@Nagashima 変数の型を意識して初期化してください　= 10.0
    //@Nagashima メンバの初期化はStart()で行ってください
    public double t_Score;
    public double time = 10;
    public NCMB.HighScore highScore;
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
    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 関数の簡単な説明
    /// </summary>
    private void Initialize()
    {
        // ハイスコアを取得する。保存されてなければ0を取得する。
        isNewRecord = false;
    }

    // ポイントの追加
    //@Nagashima PointなのかScoreなのかをはっきりさせてください
    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 関数の簡単な説明
    /// </summary>
    /// <param name="time">変数の説明</param>
    public void AddPoint( double time)
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
    //@Nagashima 関数名は２単語以上にしてください　SaveHighScore
    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 関数の簡単な説明
    /// </summary>
    public void Save()
    {

        // ハイスコアを保存する（ただし記録の更新があったときだけ）
        //@Nagashima ==trueは略すことができます(テクニックとして覚えておくと便利です)　if(isNewRecord){}
        if (isNewRecord==true)
        {
             // Debug.Log("isNewRecord2"+ isNewRecord );
            highScore.Save(t_Score);
        }

        // ゲーム開始前の状態に戻す
        Initialize();
    }
}
