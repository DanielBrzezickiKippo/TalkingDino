using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    public List<GameObject> meshes;
    public GameObject meshObject;
    [SerializeField] public Transform target;
    [SerializeField] private GameObject resetObject;
    [SerializeField] private DestType type;
    public enum DestType
    {
        balloon,
        megaBallon,
        stuffed
    }


    private void Start()
    {
        ChangeSkin();
    }

    public void ChangeSkin()
    {
        MeshFilter filter = meshObject.GetComponent<MeshFilter>();
        MeshRenderer renderer = meshObject.GetComponent<MeshRenderer>();

        int r = Random.Range(0, meshes.Count);
        filter.mesh = meshes[r].GetComponent<MeshFilter>().sharedMesh;
        for(int i=0;i<renderer.sharedMaterials.Length;i++)
        {
            renderer.materials[i] = meshes[r].GetComponent<MeshRenderer>().sharedMaterials[i];
        }

        Restart();
    }

    public void Restart()
    {
        switch (type)
        {
            case DestType.balloon:
                resetObject.GetComponent<Balloon>().RestartPos();
                break;
            case DestType.megaBallon:
                Animator animator = resetObject.GetComponent<Animator>();
                animator.Play("ABIdle", -1, 0f);
                break;
            case DestType.stuffed:
                resetObject.GetComponent<LandedStuffed>().RestartPos();
                break;
        }
    }


}
