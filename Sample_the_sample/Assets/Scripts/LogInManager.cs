/***************
 * 担　当：小林*
 * 制作日：    *
 * *************/

 //20190719 長嶋　確認

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LogInManager : MonoBehaviour
{

    private GameObject guiTextLogIn;   // ログインテキスト
    private GameObject guiTextSignUp;  // 新規登録テキスト

    // ログイン画面のときtrue, 新規登録画面のときfalse
    private bool isLogIn;

    // ボタンが押されると対応する変数がtrueになる
    private bool isLogInButton;
    private bool isSignUpMenuButton;
    private bool isSignUpButton;
    private bool isBackButton;

    // テキストボックスで入力される文字列を格納
    public string userId;
    public string userPw;
    public string mailAddress;

    void Start()
    {

        FindObjectOfType<UserAuth>().LogOut();

        // ゲームオブジェクトを検索し取得する
        guiTextLogIn = GameObject.Find("GUITextLogIn");
        guiTextSignUp = GameObject.Find("GUITextSignUp");

        isLogIn = true;
        guiTextSignUp.SetActive(false);
        guiTextLogIn.SetActive(true);

    }
    
    /// <summary>
    /// @brief GUI類の表示関数
    /// </summary>
    void OnGUI()
    {

        // ログイン画面 
        if (isLogIn)
        {

            DrawLogInMenu();

            // ログインボタンが押されたら
            if (isLogInButton)
                FindObjectOfType<UserAuth>().LogIn(userId, userPw);

            // 新規登録画面に移動するボタンが押されたら
            if (isSignUpMenuButton)
                isLogIn = false;
        }

        // 新規登録画面
        else
        {

            DrawSignUpMenu();

            // 新規登録ボタンが押されたら
            if (isSignUpButton)
                FindObjectOfType<UserAuth>().SignUp(userId, mailAddress, userPw);

            // 戻るボタンが押されたら
            if (isBackButton)
                isLogIn = true;
        }

        // currentPlayerを毎フレーム監視し、ログインが完了したら
        if (FindObjectOfType<UserAuth>().CurrentPlayer() != null)
            //Debug.Log("ログイン成功");
            SceneManager.LoadScene("Stage");

    }

    /// <summary>
    /// @brief ログインメニューの表示
    /// </summary>
    private void DrawLogInMenu()
    {
        // テキスト切り替え
        guiTextSignUp.SetActive(false);
        guiTextLogIn.SetActive(true);

        // テキストボックスの設置と入力値の取得
        GUI.skin.textField.fontSize = 20;
        int txtW = 150, txtH = 40;
        userId = GUI.TextField(new Rect(Screen.width * 1 / 2, Screen.height * 1 / 3 - txtH * 1 / 2, txtW, txtH), userId);
        userPw = GUI.PasswordField(new Rect(Screen.width * 1 / 2, Screen.height * 1 / 2 - txtH * 1 / 2, txtW, txtH), userPw, '*');

        // ボタンの設置
        int btnW = 180, btnH = 50;
        GUI.skin.button.fontSize = 20;
        isLogInButton = GUI.Button(new Rect(Screen.width * 1 / 4 - btnW * 1 / 2, Screen.height * 3 / 4 - btnH * 1 / 2, btnW, btnH), "Log In");
        isSignUpMenuButton = GUI.Button(new Rect(Screen.width * 3 / 4 - btnW * 1 / 2, Screen.height * 3 / 4 - btnH * 1 / 2, btnW, btnH), "Sign Up");

    }

    /// <summary>
    /// @brief 会員登録画面の表示
    /// </summary>
    private void DrawSignUpMenu()
    {
        // テキスト切り替え
        guiTextLogIn.SetActive(false);
        guiTextSignUp.SetActive(true);

        // テキストボックスの設置と入力値の取得
        int txtW = 150, txtH = 35;
        GUI.skin.textField.fontSize = 20;
        userId = GUI.TextField(new Rect(Screen.width * 1 / 2, Screen.height * 1 / 4 - txtH * 1 / 2, txtW, txtH), userId);
        userPw = GUI.PasswordField(new Rect(Screen.width * 1 / 2, Screen.height * 2 / 5 - txtH * 1 / 2, txtW, txtH), userPw, '*');
        mailAddress = GUI.TextField(new Rect(Screen.width * 1 / 2, Screen.height * 11 / 20 - txtH * 1 / 2, txtW, txtH), mailAddress);

        // ボタンの設置
        int btnW = 180, btnH = 50;
        GUI.skin.button.fontSize = 20;
        isSignUpButton = GUI.Button(new Rect(Screen.width * 1 / 4 - btnW * 1 / 2, Screen.height * 3 / 4 - btnH * 1 / 2, btnW, btnH), "Sign Up");
        isBackButton = GUI.Button(new Rect(Screen.width * 3 / 4 - btnW * 1 / 2, Screen.height * 3 / 4 - btnH * 1 / 2, btnW, btnH), "Back");
    }

}