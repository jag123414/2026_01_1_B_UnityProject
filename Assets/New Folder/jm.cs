using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class jm : MonoBehaviour
{
    Rigidbody rigidbody;
    public float power = 200f;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            power = power + Random.Range(-100, 200);
            rigidbody.AddForce(transform.up * power);
        }

        if (this.gameObject.transform.position.y > 5 || this.gameObject.transform.position.y < -3)
        {
            Destroy(this.gameObject);
        }
    }
}
