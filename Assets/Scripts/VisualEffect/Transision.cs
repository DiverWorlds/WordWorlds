using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    private Material material;
    private float sliderValue = 0;
    public float SliderValue
    {
        get { return sliderValue; }
        set
        {
            sliderValue = value;
            if (sliderValue >= 1) onFilled?.Invoke();
        }
    }
    [SerializeField] private float fillTime = 1f;
    [SerializeField] private float refreshTime = 0.01f;
    [SerializeField] private UnityEvent onFilled;
    private void Start()
    {
        // 初期化
        this.material = gameObject.GetComponent<Image>().material;
        SliderValue = 0;
        this.material.SetFloat("_slider", SliderValue);
    }

    public void StartFill()
    {
        //フィルをスタート
        StartCoroutine(Fill());
    }

    public IEnumerator Fill()
    {
        SliderValue += 1 / (fillTime / refreshTime);
        this.material.SetFloat("_slider", SliderValue);
        if (SliderValue > 1f) yield break;
        yield return new WaitForSeconds(refreshTime);
        StartCoroutine(Fill());
    }
}
