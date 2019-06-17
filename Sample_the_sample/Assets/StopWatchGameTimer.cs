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
    float timer = 0f;//タイマー
    float judg = 0;//判定するための変数
    int num = 0;//目標の数字
    float Darkening = 0;//暗転させる時間
    bool Stopflg = false;
    bool Startflg = false;
    int i = 0;

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
        Text Text2_text = Text2_object.GetComponent<Text>();
        if (Startflg == false)
        {
            Text1_text.text = "タップすると始まります";
            Text2_text.text = "目標数"+num+" 暗転開始が"+ Darkening;

        }
        else
        {
            Text1_text.text = "タップすると止まります";

        }
        if(Stopflg == true)
        {
            Text1_text.text = "スペースでステージ選択へ";
            Text2_text.text = "エンターでもう一度";
            if(Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("StageCheck");

            }
            if (Input .GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("StopWatchGame");

            }

        }
    }
    void StartCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Startflg = true;
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
}
