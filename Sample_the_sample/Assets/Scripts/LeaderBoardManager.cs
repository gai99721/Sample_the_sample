/***************
 * 担　当：小林*
 * 制作日：    *
 * *************/

//20190719 長嶋　確認

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderBoardManager : MonoBehaviour
{
    //@Nagashima lなど先頭単語を1文字だけにしないでください　lenderBoard
    private LeaderBoard lBoard;
    private  NCMB.HighScore currentHighScore;

    //@Nagashima 変数はできる限りprivateを使用するようにしてください
    //@Nagashima 配列を初期化する際の要素数は定数を用いてください　管理が楽になります
    public GameObject[] top = new GameObject[5];
    public GameObject[] nei = new GameObject[5];

    private bool isScoreFetched;
    private bool isRankFetched;
    private bool isLeaderBoardFetched;

    // ボタンが押されると対応する変数がtrueになる
    private bool backButton;

    void Start()
    {
        lBoard = new LeaderBoard();

        // テキストを表示するゲームオブジェクトを取得
        for (int i = 0; i < 5; ++i)
        {
            top[i] = GameObject.Find("Top" + i);
            nei[i] = GameObject.Find("Neighbor" + i);
        }

        // フラグ初期化
        isScoreFetched = false;
        isRankFetched = false;
        isLeaderBoardFetched = false;

        // 現在のハイスコアを取得
        string name = FindObjectOfType<UserAuth>().CurrentPlayer();
        currentHighScore = new NCMB.HighScore(10, name);
        currentHighScore.Fetch();
    }

    void Update()
    {
        // 現在のハイスコアの取得が完了したら1度だけ実行
        //@Nagashima -1はどういう状態なのかがわからないので定数を用いるなどするといいです
        if (currentHighScore.Score != -1 && !isScoreFetched)
        {
            lBoard.FetchRank(currentHighScore.Score);
            isScoreFetched = true;
        }

        // 現在の順位の取得が完了したら1度だけ実行
        if (lBoard.currentRank != 0 && !isRankFetched)
        {
            lBoard.FetchTopRankers();
            lBoard.FetchNeighbors();
            isRankFetched = true;
        }

        // ランキングの取得が完了したら1度だけ実行
        if (lBoard.topRankers != null && lBoard.neighbors != null && !isLeaderBoardFetched)
        {
            // 自分が1位のときと2位のときだけ順位表示を調整
            /*int offset = 2;
            if (lBoard.currentRank == 1) offset = 0;
            if (lBoard.currentRank == 2) offset = 1;*/

            // 取得したトップ5ランキングを表示
            for (int i = 0; i < lBoard.topRankers.Count; ++i)
            {
                top[i].GetComponent<Text>().text = i + 1 + ". " + lBoard.topRankers[i].Print();
            }

            // 取得したライバルランキングを表示
            /*for (int i = 0; i < lBoard.neighbors.Count; ++i)
            {
                nei[i].GetComponent<Text>().text = lBoard.currentRank - offset + i + ". " + lBoard.neighbors[i].Print();
            }*/
            isLeaderBoardFetched = true;
        }
    }

    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 関数の簡単な説明
    /// </summary> 
    private void OnGUI()
    {
        DrawMenu();
        // 戻るボタンが押されたら
        if (backButton)
            SceneManager.LoadScene("StopWatchGame");
    }

    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 関数の簡単な説明
    /// </summary> 
    private void DrawMenu()
    {
        // ボタンの設置
        int btnW = 170, btnH = 30;
        GUI.skin.button.fontSize = 20;
        backButton = GUI.Button(new Rect(Screen.width * 1 / 2 - btnW * 1 / 2, Screen.height * 7 / 8 - btnH * 1 / 2, btnW, btnH), "Back");
    }
}