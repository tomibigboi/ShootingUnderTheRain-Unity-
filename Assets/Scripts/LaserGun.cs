using UnityEngine;
using UnityEngine.UIElements;

public class LaserGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject laserPrefab;
    private GameObject SpawnPoint;
    private GameObject[] bulletInstnce;
    private GameObject laserInst;
    public int maxNbrOfBullets;
    private int activeBullet;
    private Vector2 direction;

    // TODO : add swpan point comp
    // TODO : Start working on laser

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.childCount > 0)
        {
            Transform childTransform = transform.GetChild(0);
            SpawnPoint = childTransform.gameObject;
        }
        PreapreBullets();
        PrepareLaser();
    }

    void PreapreBullets()
    {
        if (bulletPrefab)
        {
            activeBullet = 0;
            bulletInstnce = new GameObject[20];
            for (int i = 0; i < maxNbrOfBullets; i++)
            {
                bulletInstnce[i] = Instantiate(bulletPrefab);
                bulletInstnce[i].GetComponent<BulletScript>().setOwner(gameObject);
                bulletInstnce[i].SetActive(false);
            }
        }
    }

    // preapres the isntance of the laser which will be launched
    void PrepareLaser()
    {
        if (laserPrefab)
        {
            laserInst = Instantiate(laserPrefab, new Vector3(500,500,0),new Quaternion(0,0,0,0));
            laserInst.GetComponent<LaserAttackScript>().setlaserFirePoint(SpawnPoint.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Function that orients the gun towards the cursor
        FollowCursor();

        //
        if (Input.GetKeyDown(KeyCode.Mouse0) && maxNbrOfBullets > activeBullet && bulletPrefab)
        {
            LaunchBullet();   
        }
        else if(activeBullet >= maxNbrOfBullets)
        {
            activeBullet = 0;
            LaunchBullet();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            LaunchLaser();
        }
    }

    void LaunchBullet()
    {
        BulletScript bulletController = bulletInstnce[activeBullet].GetComponent<BulletScript>();
        bulletController.launchDirection = direction;
        bulletController.launchSpeed = 2;
        bulletInstnce[activeBullet].transform.rotation = transform.rotation;
        bulletInstnce[activeBullet].transform.position = SpawnPoint.transform.position;
        bulletInstnce[activeBullet].SetActive(true);
        activeBullet++;
    }

    void LaunchLaser()
    {
        laserInst.SetActive(true);
        laserInst.GetComponent<LaserAttackScript>().direction = direction;
        laserInst.GetComponent<LaserAttackScript>().initLaserShoot();
    }

    void FollowCursor()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePositionScreen = Input.mousePosition;

        // Convert screen coordinates to world coordinates, for 2D
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        // We only care about the X and Y coordinates in 2D, so we set Z to 0.
        Vector2 mousePosition2D = new Vector2(mousePositionWorld.x, mousePositionWorld.y);

        // Calculate the angle towards the mouse position
        direction = mousePosition2D - (Vector2)transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
    }

}
