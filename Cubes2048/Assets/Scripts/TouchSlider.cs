using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction OnPointerDownEvent;
    public UnityAction<float> OnPointerDragEvent;
    public UnityAction OnPointerUpEvent;

    private Slider uiSlider;

    private void Awake()
    {
        uiSlider = GetComponent<Slider>();
        uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke();
        OnPointerDragEvent?.Invoke(uiSlider.value);
    }

    public void OnSliderValueChanged(float slidePosition)
    {
        OnPointerDragEvent?.Invoke(slidePosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent?.Invoke();

        uiSlider.value = 0f;
    }

    private void OnDestroy()
    {
        uiSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
