using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoisticController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image joistickBg;
    private Image joystick;
    private Vector2 inputVector;

    private void Start()
    {
        joistickBg = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joistickBg.rectTransform, eventData.position,eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joistickBg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joistickBg.rectTransform.sizeDelta.x);

            inputVector = new Vector2(pos.x * 2, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joistickBg.rectTransform.sizeDelta.x / 2), inputVector.y * (joistickBg.rectTransform.sizeDelta.y / 2));
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        if (inputVector.x != 0) return inputVector.x;
        else return 0;
    }

    public float Vertical()
    {
        if (inputVector.y != 0) return inputVector.y;
        else return 0;
    }
}
