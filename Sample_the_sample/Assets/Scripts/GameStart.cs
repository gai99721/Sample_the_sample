/*
 *担  当 : 原口友稀
 *制作日 : 2019/6
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

    ////@Nagashima 関数にコメントを振る場合この形式にしてください(/を3つ入力すると勝手に挿入されます)
    /// <summary>
    /// @brief 関数の簡単な説明
    /// </summary>
    public void OnClick()
    {
        //Debug.Log("ok");
        SceneManager.LoadScene("StopWatchGame");
    }

}
