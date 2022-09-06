using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private float zDistance = 7.5f;
    [SerializeField] public Vector3 mainPlace;

    private DragHandler dragHandler;
    // Start is called before the first frame update
    void Start()
    {
        dragHandler = GameObject.FindGameObjectWithTag("DragHandler").GetComponent<DragHandler>();
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
            if (hit.transform.tag == "Player" && !dragHandler.Get())
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
                this.transform.position = Vector3.Lerp(transform.position, mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDistance)), speed);
                this.GetComponent<Rigidbody>().useGravity = false;
            }
        }
        else
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            hold = false;
            dragHandler.Set(false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            this.transform.position = Vector3.Lerp(transform.position, mainPlace, speed * 2);
            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(Vector3.zero), speed * 2);
        }
    }

}
