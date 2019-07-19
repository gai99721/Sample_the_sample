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

    // mobile backendに接続してログイン ------------------------

    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 関数の簡単な説明
    /// </summary>
    /// <param name="id">変数の説明</param>
    /// <param name="pw">変数の説明</param>
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

    // mobile backendに接続して新規会員登録 ------------------------
    //@Nagashima 関数名は単語の区切り1文字目を大文字にしてください
    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 関数の簡単な説明
    /// </summary>
    /// <param name="id">変数の説明</param>
    /// <param name="mail">変数の説明</param>
    /// <param name="pw">変数の説明</param>
    public void signUp(string id, string mail, string pw)
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

    // mobile backendに接続してログアウト ------------------------
    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 関数の簡単な説明
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

    // 現在のプレイヤー名を返す --------------------
    //@Nagashima 変数を取得したいときはアクセサーを使用してください
    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 関数の簡単な説明
    /// </summary>
    public string CurrentPlayer()
    {
        return currentPlayerName;
    }

    // シングルトン化する ------------------------
    //@Nagashima 変数名は2単語以上にしてください
    private UserAuth instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
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