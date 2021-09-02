using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;
    public int xPos, zPos;
    public bool Spawn = true;
    public GameObject Food;
    public Collider[] colliders;
    public float radius;
    public LayerMask FoodLayer;

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private float nextTimeToWork = 0;
    public void SpawnFood()
    {
        if(nextTimeToWork > Time.time)
        {
            return;
        }
        nextTimeToWork = Time.time + 0.2f;

        xPos = Random.Range(-54, 126);
        zPos = Random.Range(-47, 132);
        int dice = Random.Range(1, 101);
        if(dice <= 90)
        {
            if (Physics.OverlapSphere(new Vector3(xPos, 0, zPos), 0.25f, FoodLayer).Length != 0)
            {
                return;
            }
                GameObject smallGameObject = Instantiate(Resources.Load("Food"), new Vector3(xPos, 0.05f, zPos), Quaternion.identity) as GameObject;
                smallGameObject.GetComponent<Renderer>().material.color = Color.green;
                smallGameObject.transform.localScale = new Vector3(0.5f, 0.05f, 0.5f);
                Food FoodScript = smallGameObject.GetComponent<Food>();
                FoodScript.FoodType = FoodType.Small;
                FoodScript.point = 1;
                Food.transform.Translate(new Vector3(0.5f, 0.5f, 0));
        }
        else if(dice > 90 && dice <= 98)
        {          
            if (Physics.OverlapSphere(new Vector3(xPos, 0, zPos), 1.26f,FoodLayer).Length != 0)
            {
                //Debug.Log("Food Collide");
                return;
            }           
               GameObject mediumgameObject = Instantiate(Resources.Load("Food"), new Vector3(xPos, 0.05f, zPos), Quaternion.identity) as GameObject;
               //mediumgameObject.GetComponent<Renderer>().material.color = Color.blue;
               mediumgameObject.GetComponent<SphereCollider>().isTrigger = false;
               mediumgameObject.transform.localScale = new Vector3(1.5f, 0.05f, 1.5f);
               Food FoodScript = mediumgameObject.GetComponent<Food>();
               FoodScript.FoodType = FoodType.Medium;
               FoodScript.point = 3;        
               Food.transform.Translate(new Vector3(0.5f, 0.5f, 0));
        }
        else if(dice > 98)
        {
            if (Physics.OverlapSphere(new Vector3(xPos, 0, zPos), 1.6f, FoodLayer).Length != 0)
            {
                //Debug.Log("Food Collide");
                return;
            }
                GameObject BiggameObject = Instantiate(Resources.Load("Food"), new Vector3(xPos, 0.08f, zPos), Quaternion.identity) as GameObject;
                //BiggameObject.GetComponent<Renderer>().material.color = Color.yellow;
                BiggameObject.GetComponent<SphereCollider>().isTrigger = false;
                BiggameObject.transform.localScale = new Vector3(2.5f, 0.08f, 2.5f);
                Food FoodScript = BiggameObject.GetComponent<Food>();
                FoodScript.FoodType = FoodType.Big;
                FoodScript.point = 5;
                Food.transform.Translate(new Vector3(0.5f, 0.5f, 0));
        }
    }
    public void Explode(float point,float x_pos,float y_pos,Color color_)
    {
        
        for (int i = 0; i < point; i++)
        {
            //Debug.Log("Explode calisti");
            Explosion(x_pos,y_pos,color_);
        }
    }
        private void Explosion(float a,float b,Color color_)
        {
            //Debug.Log("Explosion calisti");
            var randomPos = Random.insideUnitCircle * Random.Range(3,4);
            GameObject smallGameObject = Instantiate(Resources.Load("Food"), new Vector3(a + randomPos.x, 3, b + randomPos.y), Quaternion.identity) as GameObject;
            //Debug.Log(smallGameObject.transform.position
            smallGameObject.GetComponent<Rigidbody>().velocity = Random.insideUnitSphere * 3f;
            smallGameObject.GetComponent<Rigidbody>().useGravity = true;
            smallGameObject.GetComponent<SphereCollider>().isTrigger = false;
            smallGameObject.GetComponent<Rigidbody>().isKinematic = false;
            smallGameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            smallGameObject.GetComponent<Renderer>().material.color = color_;
            smallGameObject.transform.localScale = new Vector3(0.5f, 0.1f, 0.5f);
            Food FoodScript = smallGameObject.GetComponent<Food>();
            FoodScript.FoodType = FoodType.Small;
            FoodScript.point = 1;
        }
    
}
