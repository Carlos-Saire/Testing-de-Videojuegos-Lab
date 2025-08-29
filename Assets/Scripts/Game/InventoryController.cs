using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private DoublyLinkedCircularList<GameObject> objectInteractive=new DoublyLinkedCircularList<GameObject>();
    private DoublyLinkedCircularList<GameObject>.Node currentObject;
    private Image tmp;
    [SerializeField] private Image[] panel;
    private void Scroll(Vector2 value)
    {
        if (objectInteractive.GetCount() > 0 && currentObject != null)
        {
            currentObject.Value.SetActive(false);
            if (value.y > 0)
            {
                currentObject = currentObject.Next;
            }
            else if (value.y < 0)
            {
                currentObject = currentObject.Previos;
            }
            currentObject.Value.SetActive(true);
        }
    }
    private void Information(GameObject go,Sprite sprite)
    {
        if(go.GetComponent<WeaponController>()!=null)
        {
            UpdateInformation(go,sprite,0);
        }
        else if (go.GetComponent<GrenadeController>() != null)
        {
            UpdateInformation(go, sprite, 1);
        }
        else if (go.GetComponent<MedikitController>() != null)
        {
            UpdateInformation(go, sprite, 2);
        }
    }
    private void UpdateInformation(GameObject go,Sprite sprite,int priority)
    {
        tmp = panel[priority];
        if (sprite == null)
        {
            tmp.sprite = null;
            if(objectInteractive.GetCount() > 0)
            {
                currentObject = currentObject.Next;
                currentObject.Value.SetActive(true) ;
            }
            return;
        }
        if (tmp.sprite == null)
        {
            tmp.sprite = sprite;
            objectInteractive.InsertAtEnd(go);
            if (objectInteractive.GetCount() == 1)
            {
                currentObject = objectInteractive.GetAtHeat();
                currentObject.Value.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
    }
    private void Remove(GameObject go)
    {
        objectInteractive.Remove(go);
        Information(go, null);
    }
    private void OnEnable()
    {
        InputReader.scroll += Scroll;
        Item.eventInformation += Information;
        Item.eventRemove += Remove;
    }
    private void OnDisable()
    {
        InputReader.scroll -= Scroll;
        Item.eventInformation -= Information;
        Item.eventRemove -= Remove;
    }
}
