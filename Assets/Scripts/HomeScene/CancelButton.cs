using UnityEngine;

public class CancelButton : MonoBehaviour
{
    [SerializeField] RecallButton recallButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        recallButton.ResetSelect();
    }
}
