using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour
{
    private DragHandler dragHandler;


    private GameObject firstChild;
    // Start is called before the first frame update
    void Start()
    {
        dragHandler = GameObject.FindGameObjectWithTag("DragHandler").GetComponent<DragHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleClick();
        //HandleOutline();
    }

    bool click = false;
    void HandleClick()
    {
        if(dragHandler==null)
            dragHandler = GameObject.FindGameObjectWithTag("DragHandler").GetComponent<DragHandler>();


        if (!PointerOverUI())
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == transform.name && !dragHandler.Get())
                {
                    if (Input.GetMouseButton(0))
                    {
                        click = true;
                        dragHandler.Set(true);
                    }
                    else
                    {
                        if (click)
                        {
                            if (GetComponent<TriggerAction>() != null)
                                GetComponent<TriggerAction>().Action();
                        }
                        click = false;
                        dragHandler.Set(false);
                    }
                }
            }
        }
    }

    public bool PointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }


    void HandleOutline()
    {
        /*if(firstChild == null)
            firstChild = transform.GetChild(0).gameObject;
        if (firstChild == null && transform.childCount > 0)
            firstChild = transform.GetChild(0).gameObject;
        else
            firstChild = this.gameObject;

        if (firstChild.GetComponent<Outline>() == null)
        {
            Outline childOutline= firstChild.AddComponent<Outline>();
            childOutline.OutlineWidth = 5f;

        }*/
    }

}
