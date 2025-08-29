using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
abstract public class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected Button button;
    protected Image image;
    [SerializeField] protected Vector3 enlargedScale;
    [SerializeField] protected Color newColor;
    protected Vector3 originalScale;
    protected Color originalColor;
    protected void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.onClick.AddListener(OnClick);
    }
    protected void Start()
    {
        originalScale = transform.localScale;
        originalColor=image.color;
    }
    protected abstract void OnClick();
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = enlargedScale; 
        image.color = newColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        image.color = originalColor;
    }
}
