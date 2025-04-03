using UnityEngine;
using UnityEngine.EventSystems;

public class HelpButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject help;

    public void OnPointerEnter(PointerEventData eventData)
    {
        help.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        help.SetActive(false); // 마우스를 떼면 숨기기
    }
}