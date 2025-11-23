using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragableImage : MonoBehaviour, IDragHandler, IDropHandler,IBeginDragHandler,IEndDragHandler
{
    public delegate void ItemDraggedEventHandler(ItemData item);

    public static ItemDraggedEventHandler OnItemDragged;

    private bool _isDragging;

    public ItemData CurrentItem;

    private Image _image;
    private RectTransform _inventoryUI;
    private Transform _itemHolder;

    private Vector3 _offset= Vector3.zero;

    private Vector2 _startingPos;

    private Slot _currentInventorySlot;

    LayerMask ClickableLayer;

    public void Start()
    {
        ClickableLayer = LayerMask.GetMask("Clickable", "Enemy","Player");
        _image = gameObject.GetComponent<Image>();
        _inventoryUI = Inventory.Singleton.OuterRingUI.GetComponent<RectTransform>();
        _itemHolder = Inventory.Singleton.ItemHolder.GetComponent<Transform>();
    }

    public void Setup(Slot CurrentInventorySlot)
    {
        _currentInventorySlot = CurrentInventorySlot;
    }

    

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Stores starting position

        _startingPos = transform.position;
        transform.SetParent(_itemHolder);

        _image.raycastTarget = false;

        OnItemDragged?.Invoke(CurrentItem);

        _offset = eventData.position - new Vector2(transform.position.x, transform.position.y);
        _isDragging = true;
    }


    // Hide inventory UI when item goes out of the outer ring
    public void OnDrag(PointerEventData eventData)
    {
        if (_inventoryUI != null && !RectTransformUtility.RectangleContainsScreenPoint(_inventoryUI, eventData.position))
        {
            _inventoryUI.localScale = new Vector3(0, 0, 0);
            GameManager.instance.isItemMenuOn = false;
        }
    }

    private void Update()
    {
        if (_isDragging)
        {
            transform.position = Input.mousePosition -_offset;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject currentGo = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log(currentGo);
        if (currentGo != null)
        {
            OnDropUI(currentGo);
        }
        else 
        {
            OnDropEnvironmentAsset();
        }

        _image.raycastTarget = true;
        _isDragging = false;
    }

    private void ResetIconPosition()
    {
        transform.position = _startingPos;
        _currentInventorySlot.FillSlot(this.gameObject, CurrentItem);
    }

    private void OnDropUI(GameObject currentGo)
    {
        Slot slot = currentGo.GetComponent<Slot>();

        if (slot == null || slot.IsSlotFilled)
        {
            ResetIconPosition();
            if(slot is CraftableSlot craftableSlot && craftableSlot.ShowDialog)
            {
                GameManager.instance.ShowDialogue("Hmm... It seems I have to remove this combined item to create a new one.");
            }
            return;
        }

        if (slot is CraftableSlot && CurrentItem.Item.CanBeCombined)
        {
            Setup(slot);
            slot.FillSlot(this.gameObject, CurrentItem);

        }

        else if (slot is StorageSlot)
        {

            Setup(slot);
            slot.FillSlot(this.gameObject, CurrentItem);

        }
        else
        {
            ResetIconPosition();
        }
    }

    private void OnDropEnvironmentAsset()
    {
        Ray ray = GameManager.instance.mainCamera.ScreenPointToRay(Input.mousePosition);
        ResetIconPosition();
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ClickableLayer))
        {
            Debug.Log("HIT");

            if (CurrentItem.Item.ID == 1 && hit.collider.gameObject.GetComponent<EnemyCombat>())
            {
                Debug.Log("SHOOTING");
                GameManager.instance.player.GetComponent<PlayerCommands>().CreateShootTask(hit.collider.gameObject.GetComponent<EnemyCombat>(), CurrentItem.Item.itemObject.GetComponent<GunScript>().totalDamage);
            }
            else if (hit.collider.gameObject.GetComponent<ItemBase>() != null)
            {
                Debug.Log("DRAG AND DROP");

                GameManager.instance.player.GetComponent<PlayerCommands>().CreateDragAndDropTask(hit.collider.gameObject.GetComponent<ItemBase>(), CurrentItem);
            }
            else if (hit.collider.gameObject.CompareTag("Player"))
            {
                GameManager.instance.ShowDialogue(CurrentItem.Item.Description);
            }

            Debug.Log($"Hit object: {hit.collider.gameObject.name}");
            GameManager.instance.isItemMenuOn = false;
        }
    }

}
