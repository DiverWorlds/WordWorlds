using UnityEngine;

public class AttentionSpot : MonoBehaviour
{
    private IEffectable effect;
    private bool isFocused = false;
    void Start()
    {
        effect = GetComponent<IEffectable>();
    }

    void Update()
    {
        if (!isFocused) return;
        if (Input.GetMouseButtonDown(0))
        {
            effect.PlayEffect();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        isFocused = true;
        Logger.Log("Enter");
    }
    void OnTriggerExit2D(Collider2D other)
    {
        isFocused = false;
        Logger.Log("Exit");
    }
}