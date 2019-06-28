/*
 *担  当 : 原口友稀
 *制作日 : 2019/6
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

    public void OnClick()
    {
        //Debug.Log("ok");
        SceneManager.LoadScene("StopWatchGame");
    }

}
