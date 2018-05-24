using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Example of control application for drag and drop events handle
/// </summary>
public class DummyControlUnit : MonoBehaviour
{
    public string SelectedDrag;
    public string SelectedDrop;
    public GameObject Selected;
    public GameObject Effect;
    bool Destroy;
    int counter;
    /// <summary>
    /// Operate all drag and drop requests and events from children cells
    /// </summary>
    /// <param name="desc"> request or event descriptor </param>
    void OnSimpleDragAndDropEvent(DragAndDropCell.DropEventDescriptor desc)
    {
        // Get control unit of source cell
        DummyControlUnit sourceSheet = desc.sourceCell.GetComponentInParent<DummyControlUnit>();
        // Get control unit of destination cell
        DummyControlUnit destinationSheet = desc.destinationCell.GetComponentInParent<DummyControlUnit>();
        switch (desc.triggerType)                                               // What type event is?
        {
            case DragAndDropCell.TriggerType.DropRequest:                       // Request for item drag (note: do not destroy item on request)
                Debug.Log("Request " + desc.item.name + " from " + sourceSheet.name + " to " + destinationSheet.name);
                SelectedDrop = desc.item.transform.GetComponent<DragAndDropItem>().type.ToString();
                break;
            case DragAndDropCell.TriggerType.DropEventEnd:                      // Drop event completed (successful or not)
                if (desc.permission == true)                                    // If drop successful (was permitted before)
                {
                    Debug.Log("Successful drop " + desc.item.name + " from " + sourceSheet.name + " to " + destinationSheet.name);
                   
                   
                }
                else                                                            // If drop unsuccessful (was denied before)
                {
                    Debug.Log("Denied drop " + desc.item.name + " from " + sourceSheet.name + " to " + destinationSheet.name);
                }
                break;
            case DragAndDropCell.TriggerType.ItemAdded:                         // New item is added from application
                Debug.Log("Item " + desc.item.name + " added into " + destinationSheet.name);
                break;
            case DragAndDropCell.TriggerType.ItemWillBeDestroyed:               // Called before item be destructed (can not be canceled)
                Debug.Log("Item " + desc.item.name + " will be destroyed from " + sourceSheet.name);
                SelectedDrag = desc.item.transform.GetComponent<DragAndDropItem>().type.ToString();
                Selected = desc.item.transform.gameObject;
                if (SelectedDrag == SelectedDrop)
                {

                    GameObject parent = desc.item.gameObject.transform.parent.gameObject;
                    Debug.Log(desc.item.gameObject.transform.parent.gameObject.name);

                    for (int i =0; i < desc.item.gameObject.transform.parent.transform.childCount; i++)
                    {

                      Destroy(desc.item.gameObject.transform.parent.GetChild(i).gameObject);

                    }
                     
                   
                    StartCoroutine(IntiateEffect(parent));
                }
                else {

                   Destroy = true;
                    
                }
                break;
            default:
                Debug.Log("Unknown drag and drop event");
                break;
        }
    }

    IEnumerator IntiateEffect(GameObject Parent )

    {

        yield return new WaitForEndOfFrame();

        GameObject effect = GameObject.Instantiate(Effect);
       
        effect.transform.parent = Parent.transform;

#if UNITY_EDITOR

        effect.transform.localPosition = new Vector3(0, 0f, 0);
#endif
#if UNITY_ANDROID

        effect.transform.localPosition = new Vector3(0, 0, 0);
#endif
        counter++;
        yield return new WaitForSeconds(1f);

        Destroy(effect.gameObject);
      
        if (gameObject.transform.GetChild(0).transform.childCount == counter)
        {

            Camera.main.GetComponent<LoadFromAsset>().LoadAsset();
        }
    }

    public void Update()
    {
        if (Selected != null) {

            Destroy(Selected.transform.parent.GetChild(1).gameObject);
            Selected = null;
            Destroy = false;
        }

    }
    /// <summary>
    /// Add item in first free cell
    /// </summary>
    /// <param name="item"> new item </param>
    public void AddItemInFreeCell(DragAndDropItem item)
    {
        foreach (DragAndDropCell cell in GetComponentsInChildren<DragAndDropCell>())
        {
            if (cell != null)
            {
				if (cell.GetItem() == null)
                {
                    cell.AddItem(Instantiate(item.gameObject).GetComponent<DragAndDropItem>());
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Remove item from first not empty cell
    /// </summary>
    public void RemoveFirstItem()
    {
        foreach (DragAndDropCell cell in GetComponentsInChildren<DragAndDropCell>())
        {
            if (cell != null)
            {
				if (cell.GetItem() != null)
                {
                    cell.RemoveItem();
                    break;
                }
            }
        }
    }
}
