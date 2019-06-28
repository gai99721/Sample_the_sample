﻿/***************
 * 担　当：小林*
 * 制作日：    *
 * *************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LeaderBoardManager : MonoBehaviour
{

    private LeaderBoard lBoard;
    private NCMB.tHightScore currentHighScore;
    public GameObject[] top = new GameObject[5];
    public GameObject[] nei = new GameObject[5];

    bool isScoreFetched;
    bool isRankFetched;
    bool isLeaderBoardFetched;

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
        currentHighScore = new NCMB.tHightScore(-1, name);
        currentHighScore.Fetch();
    }

    void Update()
    {
        // 現在のハイスコアの取得が完了したら1度だけ実行
        if (currentHighScore.score != -1 && !isScoreFetched)
        {
            lBoard.FetchRank(currentHighScore.score);
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
            int offset = 2;
            if (lBoard.currentRank == 1) offset = 0;
            if (lBoard.currentRank == 2) offset = 1;

            // 取得したトップ5ランキングを表示
            for (int i = 0; i < lBoard.topRankers.Count; ++i)
            {
                top[i].GetComponent<GUIText>().text = i + 1 + ". " + lBoard.topRankers[i].print();
            }

            // 取得したライバルランキングを表示
            for (int i = 0; i < lBoard.neighbors.Count; ++i)
            {
                nei[i].GetComponent<GUIText>().text = lBoard.currentRank - offset + i + ". " + lBoard.neighbors[i].print();
            }
            isLeaderBoardFetched = true;
        }
    }

    void OnGUI()
    {
        DrawMenu();
        // 戻るボタンが押されたら
        if (backButton)
            SceneManager.LoadScene("Stage");
    }

    private void DrawMenu()
    {
        // ボタンの設置
        int btnW = 170, btnH = 30;
        GUI.skin.button.fontSize = 20;
        backButton = GUI.Button(new Rect(Screen.width * 1 / 2 - btnW * 1 / 2, Screen.height * 7 / 8 - btnH * 1 / 2, btnW, btnH), "Back");
    }
}