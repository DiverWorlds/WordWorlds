using UnityEngine;
using UnityEngine.SceneManagement;

public class WakeToEndingButton : MonoBehaviour
{
    public void OnClick()
    {
        //エンディングに向かう処理
        Logger.Log("エンディングへGO!!");
        SceneManager.LoadScene("Dev_Ending");
    }
}
