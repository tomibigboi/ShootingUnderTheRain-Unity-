using UnityEngine;

public class ResetObjs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if it has the required comp
        if(collision.gameObject.GetComponent<BulletScript>())
        {
            BulletScript inst =  collision.gameObject.GetComponent<BulletScript>();
            inst.ResetObject();
        }
    }

}
