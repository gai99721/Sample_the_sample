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
    public class tHightScore
    {

        public  double score { get; set; }
        public  string name { get; private set; }

        // コンストラクタ
        public tHightScore(double _score, string _name)
        {
            Debug.Log("_score : " + _score);
            score = _score;
            Debug.Log("score : " + score);
            name = _name;
        }

        // サーバーにハイスコアを保存
        public void Save(double score)
        {
            // データストアの「HighScore」クラスから、Nameをキーにして検索
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
            query.WhereEqualTo("Name", name);
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                Debug.Log("tHighScore.score : "+score);
                //検索成功したら
                if (e == null)
                {
                    Debug.Log("tHighScore Save");
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
            query.WhereEqualTo("Name", name);
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {

                //検索成功したら
                if (e == null)
                {
                    // ハイスコアが未登録だったら
                    if (objList.Count == 0)
                    {
                        NCMBObject obj = new NCMBObject("HighScore");
                        obj["Name"] = name;
                        obj["Score"] = 9999;
                        obj.SaveAsync();
                        score = 9999;
                    }
                    // ハイスコアが登録済みだったら
                    else
                    {
                        score = System.Convert.ToInt32(objList[0]["Score"]);
                    }
                }
            });
        }
        public string print()
        {
            return name + ' ' + score;
        }
    }
}
