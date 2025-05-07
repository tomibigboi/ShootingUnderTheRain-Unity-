using UnityEngine;
using UnityEngine.UIElements;

public class LaserGun : MonoBehaviour
{
    public GameObject bulletObject;
    public GameObject laserObject;
    private GameObject[] bulletsInst;
    private GameObject laserInst;
    public int nbrOfBullets;
    private int nbrOfBulletsInst;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < nbrOfBullets; i++)
        {
            bulletsInst[i] = Instantiate(bulletObject);
            bulletsInst[i].transform.position = new Vector3(50, 50, 0);

        }
        laserInst = Instantiate(laserObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePositionScreen = Input.mousePosition;

        // Convert screen coordinates to world coordinates, for 2D
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        // We only care about the X and Y coordinates in 2D, so we set Z to 0.
        Vector2 mousePosition2D = new Vector2(mousePositionWorld.x, mousePositionWorld.y);

        // Get the rotation of the object this script is attached to.
        // In 2D, we usually work with a single Z rotation.
        float objectRotationZ = transform.eulerAngles.z;

        // Get the local rotation of the object.
        float objectLocalRotationZ = transform.localEulerAngles.z;

        // Calculate the angle towards the mouse position
        Vector2 direction = mousePosition2D - (Vector2)transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
        }

    }
}
