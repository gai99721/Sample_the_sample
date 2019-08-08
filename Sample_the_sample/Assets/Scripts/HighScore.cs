/***************
 * 担　当：小林*
 * 制作日：    *
 * *************/

//20190719 長嶋　確認

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

namespace NCMB
{
    public class HighScore
    {
        /// <summary>
        /// @brief ゲーム内スコア
        /// </summary>
        public double GameScore { get; set; }
        /// <summary>
        /// @brief Playerの名前情報
        /// </summary>
        public string PlayerName { get; private set; }

        // コンストラクタ
        public HighScore(double _score, string _Name)
        {
            //Debug.Log("_score : " + _score);
            GameScore = _score;
            //Debug.Log("score : " + Score);
            PlayerName = _Name;
        }

        /// <summary>
        /// @brief サーバーにハイスコアを保存
        /// </summary>
        /// <param name="score">変数の説明</param>
        public void ServerSave(double score)
        {
            // データストアの「HighScore」クラスから、Nameをキーにして検索
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
            query.WhereEqualTo("Name", PlayerName);
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                //Debug.Log("tHighScore.score : " + score);
                //検索成功したら
                if (e == null)
                {
                    //Debug.Log("tHighScore Save");
                    objList[0]["Score"] = score;
                    objList[0].SaveAsync();
                }
            });
        }

        /// <summary>
        /// @brief サーバーからハイスコアを取得
        /// </summary>
        public void SarverFetch()
        {
            // データストアの「HighScore」クラスから、Nameをキーにして検索
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
            query.WhereEqualTo("Name", PlayerName);
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {

                //検索成功したら
                if (e == null)
                {
                    // ハイスコアが未登録だったら
                    if (objList.Count == 0)
                    {
                        NCMBObject obj = new NCMBObject("HighScore");
                        obj["Name"] = PlayerName;
                        obj["Score"] = 10;
                        obj.SaveAsync();
                        GameScore = 10;
                    }
                    // ハイスコアが登録済みだったら
                    else
                    {
                        GameScore = System.Convert.ToDouble(objList[0]["Score"]);
                    }
                }
            });
        }

        /// <summary>
        /// @brief 画面への表示内容の設定
        /// </summary>
        public string ScreenPrint()
        {
            //return PlayerName + ' ' + ScoreManager.ToString("f3") + " sec";
            return PlayerName + ' ' + GameScore.ToString("f3") + " sec";
        }
    }
}
