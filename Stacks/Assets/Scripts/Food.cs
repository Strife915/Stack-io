using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum FoodType
{
    Small,
    Medium,
    Big

}
 public class Food : MonoBehaviour
{
    public FoodType FoodType;
    public float point;
    public Color FoodColor;
    public bool isEaten;
    public float radius { set { } get { return GetComponent<SphereCollider>().radius * transform.localScale.x; } }

    // Start is called before the first frame update
    void Start()
    {       
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }
    

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Debug.Log("Food collision");
            Destroy(collision.collider.gameObject);
        }
    }
    */
}
