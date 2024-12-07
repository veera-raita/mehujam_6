using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    [SerializeField] private Transform anchorA;
    [SerializeField] private Transform anchorB;
    [SerializeField] private int segmentCount = 10;
    [SerializeField] private float ropeLength = 5f;
    private float segmentLength; //calculated at runtime
    private Vector3[] ropeSegments;
    [SerializeField] private LineRenderer lineRenderer;
    public float curveAmount = 0.5f; // Amount of curve (for sag in the vertical direction)


    void Start()
    {
        ropeSegments = new Vector3[segmentCount];
        segmentLength = ropeLength / segmentCount;

        UpdateRope();
    }

    void Update()
    {
        UpdateRope();
    }

    void UpdateRope()
    {
        // Calculate the direction vector between the anchors
        Vector3 direction = anchorB.position - anchorA.position;
        float distance = direction.magnitude;

        // Normalize the direction vector
        direction.Normalize();

        // Place the first anchor
        ropeSegments[0] = anchorA.position;

        // Calculate the positions of each segment based on the anchors' positions
        for (int i = 1; i < segmentCount - 1; i++)
        {
            float t = (float)i / (segmentCount - 1);  // Interpolation factor (0 to 1)
            ropeSegments[i] = Vector3.Lerp(anchorA.position, anchorB.position, t);
        }

        // Place the second anchor
        ropeSegments[segmentCount - 1] = anchorB.position;

        // Update the LineRenderer to show the rope
        lineRenderer.positionCount = segmentCount;
        lineRenderer.SetPositions(ropeSegments);
    }
}