/***************
 * 担　当：小林*
 * 制作日：    *
 * *************/

//20190719 長嶋　確認

using UnityEngine;
using System.Collections;
using NCMB;
using System.Collections.Generic;

public class UserAuth : MonoBehaviour
{

    private string currentPlayerName;

    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief mobile backendに接続してログイン ------------------------
    /// </summary>
    /// <param name="id">プレイヤーのID</param>
    /// <param name="pw">プレイヤーが設定したパスワード</param>
    public void LogIn(string id, string pw)
    {

        NCMBUser.LogInAsync(id, pw, (NCMBException e) => {
            // 接続成功したら
            if (e == null)
            {
                currentPlayerName = id;
            }
        });
    }

    //@Nagashima 関数名は単語の区切り1文字目を大文字にしてください
    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief mobile backendに接続して新規会員登録 ------------------------
    /// </summary>
    /// <param name="id">プレイヤーのID</param>
    /// <param name="pw">プレイヤーが設定したパスワード</param>
    /// <param name="mail">プレイヤーが入力したメールアドレス</param>
    public void SignUp(string id, string mail, string pw)
    {

        NCMBUser user = new NCMBUser();
        user.UserName = id;
        user.Email = mail;
        user.Password = pw;
        user.SignUpAsync((NCMBException e) => {

            if (e == null)
            {
                currentPlayerName = id;
            }
        });
    }

    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief  mobile backendに接続してログアウト ------------------------
    /// </summary>
    public void LogOut()
    {

        NCMBUser.LogOutAsync((NCMBException e) => {
            if (e == null)
            {
                currentPlayerName = null;
            }
        });
    }

    //@Nagashima 変数を取得したいときはアクセサーを使用してください
    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 現在のプレイヤー名を返す --------------------
    /// </summary>
    public string CurrentPlayer()
    {
        return currentPlayerName;
    }

    /// <summary>
    /// @brief シングルトン化する ------------------------
    /// </summary>
    //@Nagashima 変数名は2単語以上にしてください
    private UserAuth userAuthInstance = null;
    void Awake()
    {
        if (userAuthInstance == null)
        {
            userAuthInstance = this;
            DontDestroyOnLoad(gameObject);

            string name = gameObject.name;
            gameObject.name = name + "(Singleton)";

            GameObject duplicater = GameObject.Find(name);
            if (duplicater != null)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.name = name;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

}