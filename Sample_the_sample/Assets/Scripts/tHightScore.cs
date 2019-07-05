﻿/***************
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

        public  float score { get; set; }
        public  string name { get; private set; }

        // コンストラクタ
        public tHightScore(float _score, string _name)
        {
            score = _score;
            name = _name;
        }

        // サーバーにハイスコアを保存
        public  void Save()
        {
            Debug.Log("score Save:" + score);
            Debug.Log("name Save:" + name);

            // データストアの「HighScore」クラスから、Nameをキーにして検索
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
            query.WhereEqualTo("Name", name);
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {

                       Debug.Log("score:" + score);
                    Debug.Log("name:" + name);
             //検索成功したら
                if (e == null)
                {
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
                        obj["Score"] = 0;
                        obj.SaveAsync();
                        score = 0;
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
