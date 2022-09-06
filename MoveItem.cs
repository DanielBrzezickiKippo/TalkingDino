using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItem : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float speed = 0.2f;
    [SerializeField] public float zDistance = 7.5f;
    [SerializeField] public Transform mainPlace;
    private DragHandler dragHandler;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        dragHandler = GameObject.FindGameObjectWithTag("DragHandler").GetComponent<DragHandler>();
        //mainPlace = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Check();
        HandleMouse();
    }

    void Check()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == this.transform.name && !dragHandler.Get())
            {
                hold = true;
                dragHandler.Set(true);
            }
        }
    }

    [SerializeField] bool hold = false;
    void HandleMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        if (Input.GetMouseButton(0) && mousePos.y >= Screen.height * 0.265f)
        {
            if (hold)
            {
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDistance)), speed);
            }
        }
        else
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, mainPlace.localPosition, speed);
            if (hold)
            {
                if (GetComponent<TriggerAction>() != null)
                    GetComponent<TriggerAction>().Action();
            }

            hold = false;
            dragHandler.Set(false);
        }

    }

    public bool GetState()
    {
        return hold;
    }
}
