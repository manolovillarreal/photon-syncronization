using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float Speed=10f;

    private Vector3 Direction = new Vector3(1,0,0);
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Direction*Time.deltaTime*Speed);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            other.gameObject.SendMessage("BulletHit", 0.1f, SendMessageOptions.DontRequireReceiver);
        }
        Destroy(this.gameObject);
    }

}
