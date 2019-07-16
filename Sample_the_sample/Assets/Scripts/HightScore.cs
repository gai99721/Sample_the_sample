/***************
 * 担　当：小林*
 * 制作日：    *
 * *************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

namespace NCMB
{
    public class HightScore
    {

        public  double Score { get; set; }
        public  string Name { get; private set; }

        // コンストラクタ
        public HightScore(double _score, string _Name)
        {
            //Debug.Log("_score : " + _score);
            Score = _score;
            //Debug.Log("score : " + Score);
            Name = _Name;
        }

        // サーバーにハイスコアを保存
        public void Save(double score)
        {
            // データストアの「HighScore」クラスから、Nameをキーにして検索
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
            query.WhereEqualTo("Name", Name);
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

        // サーバーからハイスコアを取得
        public void Fetch()
        {
            // データストアの「HighScore」クラスから、Nameをキーにして検索
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
            query.WhereEqualTo("Name", Name);
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {

                //検索成功したら
                if (e == null)
                {
                    // ハイスコアが未登録だったら
                    if (objList.Count == 0)
                    {
                        NCMBObject obj = new NCMBObject("HighScore");
                        obj["Name"] = Name;
                        obj["Score"] = 10;
                        obj.SaveAsync();
                        Score = 10;
                    }
                    // ハイスコアが登録済みだったら
                    else
                    {
                        Score = System.Convert.ToDouble(objList[0]["Score"]);
                    }
                }
            });
        }
        public string Print()
        {
            return Name + ' ' + Score.ToString("f3") + "sec";
        }
    }
}
