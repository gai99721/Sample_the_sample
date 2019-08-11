/*
 *担  当 : 原口友稀
 *制作日 : 2019/6
 * 
 *変更日
 * 原口友稀 2019/6/28 変数名関数名の変更
 */


//20190719 長嶋　確認

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StopWatchGameTimer : MonoBehaviour
{
    //@Nagashima 変数はstart関数で初期化するようにしてください

    //@Nagashima [SerializeField] private GameObject　のようにするとprivateでもInspectorから参照できます
    public GameObject timerObject = null;
    public GameObject judgObject  = null;
    public GameObject text1Object = null;
    public GameObject text2Object = null;
    public GameObject titleObject = null;

    //@Nagashima 変数はできる限りprivateにしてください
    //@Nagashima 変数名はできる限りわかりやすく２単語以上使用してください
    //@Nagashima メンバ変数は基本的にprivateを使用してください。他クラスで使用したい場合はアクセサーを使用してください
    private float mainGameTimer;//タイマー
    private float mainGameJudg;//判定するための変数
    private int   targetNumber;//目標の数字
    private float darkeningTime;//暗転させる時間
    private int   clickCount;

    //@Nagashima Flgだとどういう状態かがわかりにくいのでisなどを使用してください
    private int  isStartFlg;
    private bool isStopFlg;
    private bool isStartTimerFlg;
    private bool isBackButton;
    private bool isRetryButton;
    private bool isGameFlg;

    void Start()
    {
       /* timerObject = null;
        judgObject = null;
        text1Object = null;
        text2Object = null;
        titleObject = null;*/

        mainGameTimer = 0f;
        mainGameJudg = 0;
        targetNumber = 0;
        darkeningTime = 0;
        clickCount = 0;


        isStartFlg = 0;
        isStopFlg = false;
        isStartTimerFlg = false;
        isGameFlg = false;

    }
// Update is called once per frame
void Update()
    {
        if (clickCount == 0)
        {
            targetNumber = Random.Range(5, 31);//5以上31未満の数字をランダムで表示
            darkeningTime = targetNumber / 2;
            clickCount++;
        }
        TextCheck();//テキストの入れ替え関数
        StartCheck();//スタートしたかどうかの関数
        if (isStartTimerFlg == true)
        {
            StopCheck();//止めたかどうか
        }
    }

    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 基本的なゲームの説明を行う
    /// </summary>
    private void TextCheck()
    {
        Text text1 = text1Object.GetComponent<Text>();
        Text text2 = text2Object.GetComponent<Text>();
        Text title = titleObject.GetComponent<Text>();
        if (isGameFlg == true)
        {
            if (isStartTimerFlg == false)
            {
                text1.text = "タップすると始まります";
                text2.text = "目標数" + targetNumber + " 暗転開始が" + darkeningTime;
                title.text = "";
            }
            else
            {
                text1Object.GetComponent<Text>().text = "タップすると止まります";

            }
        }

    }

    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief ゲームが始まってから何回クリックしたかカウントする
    /// </summary>
    private void StartCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isStartFlg += 1;
        }
        switch (isStartFlg)
        {
            case 1:
                isGameFlg = true;
                break;
            case 2:
                isStartTimerFlg = true;
                break;
            default:
                break;
        }
    }

    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief mainGameTimerがdarkeningTimeを越えた時に暗転させる
    /// 　　　 isStopFlgがtrueになったらタイマーを止め判定を行う
    /// </summary>
    private void StopCheck()
    {
        Text timer = timerObject.GetComponent<Text>();
        Text judg = judgObject.GetComponent<Text>();

        if (Input.GetMouseButtonDown(0) && isStartTimerFlg == true)
        {
            isStopFlg = true;
        }

        if (isStopFlg == false)
        {

            mainGameTimer += Time.deltaTime;//0.01000f;
            if (mainGameTimer < darkeningTime)
            {
                timer.text = "" + mainGameTimer;
            }
            else
            {
                timer.text = "XX.XXXXX";
            }
        }
        else
        {
            JudgmentTime();//判定
            timer.text = "" + mainGameTimer;
            judg.text = "" + mainGameJudg;
            //time = judg;
            //FindObjectOfType<tScore>().AddPoint(time);
        }
    }

    //@Nagashima 関数名は2単語以上にしてください
    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 目標の数字から止めた数字がどれだけ離れているかを調べる
    /// </summary>
    private void JudgmentTime()
    {
        if (mainGameTimer > targetNumber)
        {
            mainGameJudg = mainGameTimer - targetNumber;

        }
        else if (mainGameTimer < targetNumber)
        {
            mainGameJudg = targetNumber - mainGameTimer;

        }
        else
        {
            mainGameJudg = mainGameTimer;
        }

        FindObjectOfType<ScoreManager>().AddScore(mainGameJudg);
        /*
         実行するとScoreにmainGamejudgが送られずエラーが発生する
         */
        //Score.AddPoint(mainGameJudg);

    }

    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief ゲーム終了後にクリックしたボタンによって違うシーンへ移動する
    /// </summary>
    private void OnGUI()
    {

        if (isStopFlg == true)
        {
            DrawMenu();
        }

        // 戻るボタンが押されたら
        if (isBackButton)
        {
            SceneManager.LoadScene("StopWatchGame");
        }

        if (isRetryButton)
        {
            SceneManager.LoadScene("StopWatchGame");
        }
    }

    //@Nagashima 関数名は単語の区切り1文字目を大文字にしてくださいｄ
    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief シーン移動時に使うボタンの生成
    /// </summary>
    private void DrawMenu()
    {
        // ボタンの設置
        int btnW = 170, btnH = 30;
        GUI.skin.button.fontSize = 20;
        isBackButton = GUI.Button(new Rect(Screen.width * 1 / 4 - btnW * 1 / 4, Screen.height * 7 / 8 - btnH * 1 / 2, btnW, btnH), "Back");
        isRetryButton = GUI.Button(new Rect(Screen.width * 1 / 4 - btnW * 1 / 4 + 300, Screen.height * 7 / 8 - btnH * 1 / 2, btnW, btnH), "Retry");
    }
}