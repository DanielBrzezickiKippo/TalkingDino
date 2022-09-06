using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideRoomManager : MonoBehaviour
{
    [SerializeField] private MapEditor mapEditor;
    [SerializeField] private int idOfObject;

    [SerializeField] private Transform defaultObject;
    [SerializeField] private Vector3 defaultPos;

    [SerializeField] private List<GameObject> objects;

    private Quaternion quaternion;

    int idCurr = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = defaultObject.localPosition;
        quaternion = defaultObject.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CheckCorrect()
    {
        int selected = mapEditor.selectables[idOfObject].pickedID;
        if (idCurr != selected)
        {
            GameObject obj = Instantiate(objects[selected], transform);
            obj.transform.localPosition = defaultPos;
            obj.transform.localRotation = quaternion;
            Destroy(defaultObject.gameObject);
            defaultObject = obj.transform;




            idCurr = selected;
        }
    }
}
