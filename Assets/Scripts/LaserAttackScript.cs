using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class LaserAttackScript : MonoBehaviour
{

    [SerializeField] private float MaxDistance = 5;
    private Transform laserFirePoint;
    private LineRenderer m_lineRenderer;
    public float lineDisplayTime = 0.2f; // How long the line will be visible
    public string targetTag = "canBeAttacked"; // Tag of objects to destroy
    bool canShoot = true;
    public float fireRate = 0.01f;
    public Vector2 direction;

    // TODO :
    // fix collision shenanigins 
    // Make enemies only collision interactable

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the LineRenderer component, ensure it's present
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    public void setlaserFirePoint(Transform t)
    {
        laserFirePoint = t;
    }


    // Update is called once per frame
    void Update()
    {


    }

    public void initLaserShoot()
    {
        if (canShoot)
        {
            shootlaser();
            canShoot = false;
            StartCoroutine(ResetShootTimer());
        }
    }


    void shootlaser()
    {
        if (Physics2D.Raycast(laserFirePoint.position, direction))
        {
            RaycastHit2D _hit = Physics2D.Raycast(laserFirePoint.position, transform.right);
            Draw2DRay(laserFirePoint.position, _hit.point);
            // Check if the hit object has the correct tag
            if (_hit.collider.gameObject.tag == targetTag)
            {
                Debug.LogError("destroyed");
                Destroy(_hit.collider.gameObject); // Destroy the GameObject
            }
            else
            {
                // Calculate the endpoint of the ray
                Vector3 endPoint = (Vector3)laserFirePoint.position + (Vector3)direction * MaxDistance;
                Draw2DRay(laserFirePoint.position, endPoint);
            }
        }
        else
        {
            // Calculate the endpoint of the ray
            Vector3 endPoint = (Vector3)laserFirePoint.position + (Vector3)direction * MaxDistance;
            Draw2DRay(laserFirePoint.position, endPoint);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 EndPos)
    {
        m_lineRenderer.positionCount = 2; // Set the number of points in the line
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, EndPos);
        StartCoroutine(ClearLine()); // Start the coroutine to clear the line
    }

    IEnumerator ClearLine()
    {
        yield return new WaitForSeconds(lineDisplayTime); // Wait for the specified time
        m_lineRenderer.positionCount = 0;             // Clear the line by setting point count to 0.  More efficient than disabling/enabling.
    }

    IEnumerator ResetShootTimer()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

}
