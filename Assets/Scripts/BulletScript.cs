using UnityEngine;

public class BulletScript : MonoBehaviour
{

    // variables i guess 
    public Vector2 launchDirection;
    public float launchSpeed;
    private BoxCollider2D ColliderRef;
    private GameObject owner;

    // TODO :
    // fix collision shenanigins 
    // Make enemies only collision interactable

    public void setOwner(GameObject o)
    {
        owner = o;
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Calculate the target rotation using LookRotation
        Quaternion targetRotation = Quaternion.LookRotation(launchDirection);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1);
    }


    // Update is called once per frame
    void Update()
    {
        //movement direction
        Vector2 newLocaiton;
        newLocaiton.x = transform.position.x + (launchDirection.x * launchSpeed * Time.deltaTime) ;
        newLocaiton.y = transform.position.y + (launchDirection.y * launchSpeed * Time.deltaTime);
        transform.position = newLocaiton;
    }

    public void ResetObject()
    {
        transform.position = owner.transform.position;
        gameObject.SetActive(false);
    }
}
