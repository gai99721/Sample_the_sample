/*
 *担  当 : 原口友稀
 *制作日 : 2019/6
 * 
 *変更日
 * 原口友稀 2019/6/28 変数名関数名の変更
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StopWatchGameTimer : MonoBehaviour
{
    public GameObject timerObject = null;
    public GameObject judgObject = null;
    public GameObject text1Object = null;
    public GameObject text2Object = null;
    public GameObject titleObject = null;
    float timer = 0f;//タイマー
    public float judg = 0;//判定するための変数
    int num = 0;//目標の数字
    float darkeinng = 0;//暗転させる時間
    bool stopFlg = false;
    bool startFlg = false;
    int i = 0;
    int numFlg = 0;

    private bool backButton;
    private bool retryButton;

    bool gameFlg = false;

    // Update is called once per frame
    void Update()
    {
        if (i == 0)
        {
            num = Random.Range(5, 31);//5以上31未満の数字をランダムで表示
            darkeinng = num / 2;
            i++;
        }
        TextCheck();//テキストの入れ替え関数
        StartCheck();//スタートしたかどうかの関数
        if (startFlg == true)
        {
            StopCheck();//止めたかどうか
        }
    }
    void TextCheck()
    {
        Text text1Text = text1Object.GetComponent<Text>();
        Text text2Text = text2Object.GetComponent<Text>();
        Text titleText = titleObject.GetComponent<Text>();
        if (gameFlg == true)
        {
            if (startFlg == false)
            {
                text1Text.text = "タップすると始まります";
                text2Text.text = "目標数" + num + " 暗転開始が" + darkeinng;
                titleText.text = "";
            }
            else
            {
                text1Text.text = "タップすると止まります";

            }
        }

    }
    void StartCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            numFlg += 1;
        }
        switch (numFlg)
        {
            case 1:
                gameFlg = true;
                break;
            case 2:
                startFlg = true;
                break;
            default:
                break;
        }
    }
    void StopCheck()
    {
        Text timerText = timerObject.GetComponent<Text>();
        Text judgText = judgObject.GetComponent<Text>();

        if (Input.GetMouseButtonDown(0) && startFlg == true)
        {
            stopFlg = true;
        }

        if (stopFlg == false)
        {

            timer += Time.deltaTime;//0.01000f;
            if (timer < darkeinng)
            {
                timerText.text = "" + timer;
            }
            else
            {
                timerText.text = "XX.XXXXX";
            }
        }
        else
        {
            Judgment();//判定
            timerText.text = "" + timer;
            judgText.text = "" + judg;
            //time = judg;
            //FindObjectOfType<tScore>().AddPoint(time);
        }
    }


    void Judgment()
    {
        if (timer > num)
        {
            judg = timer - num;

        }
        else if (timer < num)
        {
            judg = num - timer;

        }
        else
        {
            judg = timer;
        }

        FindObjectOfType<tScore>().AddPoint(judg);
        //tScore.AddPoint(judg);

    }
    void OnGUI()
    {

        if (stopFlg == true)
        {
            drawMenu();
        }

        // 戻るボタンが押されたら
        if (backButton)
        {
            SceneManager.LoadScene("StopWatchGame");
        }

        if (retryButton)
        {
            SceneManager.LoadScene("StopWatchGame");
        }
    }

    private void drawMenu()
    {
        // ボタンの設置
        int btnW = 170, btnH = 30;
        GUI.skin.button.fontSize = 20;
        backButton = GUI.Button(new Rect(Screen.width * 1 / 4 - btnW * 1 / 4, Screen.height * 7 / 8 - btnH * 1 / 2, btnW, btnH), "Back");
        retryButton = GUI.Button(new Rect(Screen.width * 1 / 4 - btnW * 1 / 4 + 300, Screen.height * 7 / 8 - btnH * 1 / 2, btnW, btnH), "Retry");
    }
}