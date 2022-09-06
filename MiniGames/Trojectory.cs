using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trojectory : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    private int lineSegmentCount = 20;

    private List<Vector3> linePoints = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTrojectory(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVector / rigidbody.mass) * Time.fixedDeltaTime;
        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;
        Debug.Log(FlightDuration);
        float stepTime = FlightDuration / lineSegmentCount;

        linePoints.Clear();

        for(int i = 0; i < lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i;
            Vector3 movementVector = new Vector3
            (
                velocity.x * stepTimePassed,
                velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                velocity.z * stepTimePassed
            );

            linePoints.Add(-movementVector+startingPoint);

        }
        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());

    }

    public void ClearTrojectory()
    {
        lineRenderer.positionCount = 0;
    }


}
