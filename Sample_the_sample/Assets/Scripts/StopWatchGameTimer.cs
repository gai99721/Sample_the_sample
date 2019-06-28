using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StopWatchGameTimer : MonoBehaviour
{
    public GameObject Timer_object = null;
    public GameObject Judg_object = null;
    public GameObject Text1_object = null;
    public GameObject Text2_object = null;
    public GameObject Title_object = null;
    float timer = 0f;//タイマー
    float judg = 0;//判定するための変数
    int num = 0;//目標の数字
    float Darkening = 0;//暗転させる時間
    bool Stopflg = false;
    bool Startflg = false;
    int i = 0;
    int num_flg = 0;
    public float point;

    private bool backButton;
    private bool retryButton;

    bool Gameflg = false;

    // Update is called once per frame
    void Update()
    {
        if (i == 0)
        {
            num = Random.Range(5, 31);//5以上31未満の数字をランダムで表示
            Darkening = num / 2;
            i++;
        }
        TextCheck();//テキストの入れ替え関数
        StartCheck();//スタートしたかどうかの関数
        if (Startflg == true)
        {
            StopCheck();//止めたかどうか
        }
    }
    void TextCheck()
    {
        Text Text1_text = Text1_object.GetComponent<Text>();
        Text Title_text = Title_object.GetComponent<Text>();
        Text Text2_text = Text2_object.GetComponent<Text>();
        if (Gameflg == true)
        {
            if (Startflg == false)
            {
                Text1_text.text = "タップすると始まります";
                Text2_text.text = "目標数" + num + " 暗転開始が" + Darkening;
                Title_text.text = "";
            }
            else
            {
                Text1_text.text = "タップすると止まります";

            }
        }

    }
    void StartCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            num_flg += 1;
        }
        switch (num_flg)
        {
            case 1:
                Gameflg = true;
                break;
            case 2:
                Startflg = true;
                break;
            default:
                break;
        }
    }
    void StopCheck()
    {
        Text Timer_text = Timer_object.GetComponent<Text>();
        Text Judg_text = Judg_object.GetComponent<Text>();

        if (Input.GetMouseButtonDown(0) && Startflg == true)
        {
            Stopflg = true;
        }

        if (Stopflg == false)
        {

            timer += Time.deltaTime;//0.01000f;
            if (timer < Darkening)
            {
                Timer_text.text = "" + timer;
            }
            else
            {
                Timer_text.text = "XX.XXXXX";
            }
        }
        else
        {
            Judgment();//判定
            Timer_text.text = "" + timer;
            Judg_text.text = "" + judg;
            point = judg;
            FindObjectOfType<tScore>().AddPoint(point);
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

    }
    void OnGUI()
    {

        if (Stopflg == true)
        {
            drawMenu();
        }

        // 戻るボタンが押されたら
        if (backButton)
        {
            SceneManager.LoadScene("Stage");
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
