using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DragAndShoot : MonoBehaviour
{
    [SerializeField] private Trojectory trojectory;
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;

    private Rigidbody rb;

    private bool isShoot;

    BasketballMG basketballMG;
    void Start()
    {
        basketballMG = GetComponentInParent<BasketballMG>();
        GetComponent<BallTrigger>().basketballMG = basketballMG;
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        mousePressDownPos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        /*Vector3 forceInit = Input.mousePosition - mousePressDownPos;
        Vector3 forceV = new Vector3(forceInit.x, forceInit.y, forceInit.y) * forceMultiplier;

        if (!isShoot)
            trojectory.UpdateTrojectory(forceV, rb, transform.position);*/
            
    }

    private void OnMouseUp()
    {
        rb.useGravity = true;
        mouseReleasePos = Input.mousePosition;
        Shoot(mouseReleasePos-mousePressDownPos);
        //trojectory.ClearTrojectory();

        Destroy(this.gameObject, 8f);
        basketballMG.NewBall(3f);

    }

    [SerializeField] private float forceMultiplier = 2;
    void Shoot(Vector3 Force)
    {
        if (isShoot)
            return;

        rb.AddForce(new Vector3(Force.x, Force.y*1.5f, Force.y) * forceMultiplier);
        isShoot = true;
        Camera.main.gameObject.GetComponent<PlayerStats>().AddFun(1f);
    }

}