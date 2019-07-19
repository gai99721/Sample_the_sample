/***************
 * 担　当：小林*
 * 制作日：    *
 * *************/

//20190719 長嶋　確認

using NCMB;
using System.Collections.Generic;

public class LeaderBoard
{
    //@Nagashima 変数はstart関数で初期化するようにしてください
    public int currentRank = 0;
    public List<NCMB.HighScore> topRankers = null;
    public List<NCMB.HighScore> neighbors = null;

    // 現プレイヤーのハイスコアを受けとってランクを取得 ---------------
    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 関数の簡単な説明
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

    // サーバーからトップ5を取得 --------------- 
    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 関数の簡単な説明
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

    // サーバーからrankの前後2件を取得 ---------------
    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 関数の簡単な説明
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