using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Agent ownerAgent;
    public Slider healthSlider;
    public float showTime = 0.5f;
    //time at which it should start fading out
    public float fadeoutTime = 0.3f;

    public Image bgImage;
    public Image fillImage;

    private Camera activeCamera;
    private float lastValue;
    private float lastValueChangeTime;

    private bool isVisible;

    private void Awake()
    {
        lastValue = GetSliderValue();
        lastValueChangeTime = Time.time;
        activeCamera = Camera.allCameras[0];
        healthSlider.gameObject.SetActive(false);
    }
    void Update()
    {
        if(ShouldShow() && !isVisible)
        {
            healthSlider.gameObject.SetActive(true);
            isVisible = true;
        }
        else if(!ShouldShow() && isVisible)
        {
            healthSlider.gameObject.SetActive(false);
            isVisible = false;
        }
        healthSlider.value = GetSliderValue();
        transform.LookAt(activeCamera.transform.position);

        SetSliderVisibility();
    }

    public bool ShouldShow()
    {
        float newSliderValue = GetSliderValue();
        if(newSliderValue != lastValue)
        {
            lastValue = newSliderValue;
            lastValueChangeTime = Time.time;
        }
        return (Time.time - lastValueChangeTime < showTime);
    }

    public virtual float GetSliderValue()
    {
        return (float)ownerAgent.CurrentHP / (float)ownerAgent.MaxHP;
    }

    void SetSliderVisibility()
    {
        float activeTime = Time.time - lastValueChangeTime;
        float timeLeft = Mathf.Max(showTime - activeTime,0);
        if(timeLeft<fadeoutTime)
        {
            float opacity = timeLeft/ fadeoutTime;
            bgImage.color = new Color(bgImage.color.r, bgImage.color.g, bgImage.color.b, opacity);
            fillImage.color = new Color(fillImage.color.r, fillImage.color.g, fillImage.color.b, opacity);
        }
        else
        {
            bgImage.color = new Color(bgImage.color.r, bgImage.color.g, bgImage.color.b, 1f);
            fillImage.color = new Color(fillImage.color.r, fillImage.color.g, fillImage.color.b, 1f);
        }
    }
}
