/***************
 * 担　当：小林*
 * 制作日：    *
 * *************/

//20190719 長嶋　確認

using NCMB;
using System.Collections.Generic;

public class LeaderBoard
{
    public int currentRank;
    public List<NCMB.HighScore> topRankers;
    public List<NCMB.HighScore> neighbors;
    /// <summary>
    /// @brief 初期化処理
    /// </summary>
    void start()
    {
        currentRank = 0;
        topRankers = null;
        neighbors = null;
    }

    /// <summary>
    /// @brief 現プレイヤーのハイスコアを受けとってランクを取得 ---------------
    /// </summary> 
    /// <param name="currentScore"></param>
    public void FetchRank(double currentScore)
    {
        // データスコアの「HighScore」から検索
        NCMBQuery<NCMBObject> rankQuery = new NCMBQuery<NCMBObject>("HighScore");
        rankQuery.WhereLessThan("Score", currentScore);
        rankQuery.CountAsync((int count, NCMBException e) => {

            if (e != null)
            {
                //件数取得失敗
            }
            else
            {
                //件数取得成功
                currentRank = count + 1; // 自分よりスコアが上の人がn人いたら自分はn+1位
            }
        });
    }

    /// <summary>
    /// @brief サーバーからトップ5を取得 --------------- 
    /// </summary> 
    public void FetchTopRankers()
    {
        // データストアの「HighScore」クラスから検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
        query.OrderByAscending("Score");
        query.Limit = 5;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {

            if (e != null)
            {
                //検索失敗時の処理
            }
            else
            {
                //検索成功時の処理
                List<NCMB.HighScore> list = new List<NCMB.HighScore>();
                // 取得したレコードをHighScoreクラスとして保存
                foreach (NCMBObject obj in objList)
                {
                    double s = System.Convert.ToDouble(obj["Score"]);
                    string n = System.Convert.ToString(obj["Name"]);
                    list.Add(new HighScore(s, n));
                }
                topRankers = list;
            }
        });
    }

    /// <summary>
    /// @brief サーバーからrankの前後2件を取得 ---------------
    /// </summary>
    public void FetchNeighbors()
    {
        neighbors = new List<NCMB.HighScore>();

        // スキップする数を決める（ただし自分が1位か2位のときは調整する）
        int numSkip = currentRank - 3;
        if (numSkip < 0) numSkip = 0;

        // データストアの「HighScore」クラスから検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
        query.OrderByAscending("Score");
        query.Skip = numSkip;
        query.Limit = 5;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {

            if (e != null)
            {
                //検索失敗時の処理
            }
            else
            {
                //検索成功時の処理
                List<NCMB.HighScore> list = new List<NCMB.HighScore>();
                // 取得したレコードをHighScoreクラスとして保存
                foreach (NCMBObject obj in objList)
                {

                    double s = System.Convert.ToDouble(obj["Score"]);
                    string n = System.Convert.ToString(obj["Name"]);
                    list.Add(new HighScore(s, n));
                }
                neighbors = list;
            }
        });
    }
}