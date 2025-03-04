using UnityEngine;
using UnityEngine.SceneManagement;

public class WakeToHomeButton : MonoBehaviour
{
    public void OnClick()
    {
        if (FlagManager.Instance.Get("WordGet_a") && FlagManager.Instance.Get("WordGet_b"))
        {
            WakeToHome();
        }
    }

    private void WakeToHome()
    {
        if (SceneManager.GetActiveScene().name.Contains("Dev_"))
        {
            SceneManager.LoadScene("Dev_HomeScene");
        }
        else
        {
            SceneManager.LoadScene("HomeScene");
        }
    }
}
