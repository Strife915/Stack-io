using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_ : Actor  
{
    enum Ai_State { Patrol, Attack };
    Ai_State ai_state = Ai_State.Patrol;
    private Transform target;
    public Rigidbody rb;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 250;  
        target = GameObject.Find("Player").GetComponent<Transform>();
        point = 0;
    }
    public void Update()
    {
        if(GamePlayManager.Instance.GameState == State.Playing)
        {
            Ai_Decide();
            updateScale();
        }   
    }
    public void FixedUpdate()
    {
        //Debug.Log(rb.velocity);
        if (GamePlayManager.Instance.GameState == State.Playing)
        {
            if (ai_state == Ai_State.Patrol)
            {
                Ai_Feed();
            }
            else if (ai_state == Ai_State.Attack)
            {
                Ai_Attack();
            }
        }          
    }
    public void Ai_Decide()
    {
        if (point <= GameObject.Find("Player").GetComponent<Player>().point ) //findlar degisecek
        {
            //Debug.Log("Ai stop attack and start to feed");
            ai_state = Ai_State.Patrol;
        }
        //else if (point > GameObject.Find("Player").GetComponent<Player>().point+10)
        //{
        //    state = State.Attack;
        //}      
        
    }
    public void Ai_Attack()
    {
        if(point > FindObjectOfType<Player>().point+15)
        {
            Vector3 TargetPlayer = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            Movement(rb,TargetPlayer-transform.position);
        }
    }
    public void Ai_Feed()
    {
        float distanceToClosestFood = Mathf.Infinity;
        Food closestFood = null;
        Food[] allFoods = GameObject.FindObjectsOfType<Food>();
        foreach (Food currentFood in allFoods)
        {
            float distanceToFood = (currentFood.transform.position - this.transform.position).sqrMagnitude;
            while (distanceToFood < distanceToClosestFood && currentFood.GetComponent<Food>().radius < radius)
            {
                distanceToClosestFood = distanceToFood;
                closestFood = currentFood;
            }
            //Debug.DrawLine(this.transform.position, closestFood.transform.position);                    
        }
        Vector3 FoodVector = new Vector3(closestFood.transform.position.x, transform.position.y, closestFood.transform.position.z);
        Movement(rb, (FoodVector-transform.position));
    }
    void OnCollisionEnter(Collision collision)
    {
        //Ai collide with other Ai
        if (collision.gameObject.tag == "Ai" && radius > collision.gameObject.GetComponent<Ai_>().radius )
        {
            Debug.Log("Main ai point " + point + "Other ai point " + collision.gameObject.GetComponent<Ai_>().point);
            point -= collision.gameObject.GetComponent<Ai_>().point;
            Spawner.Instance.Explode(collision.gameObject.GetComponent<Ai_>().point, collision.transform.localPosition.x, collision.transform.localPosition.z,collision.gameObject.GetComponent<Ai_>().color);
            Spawner.Instance.Explode(collision.gameObject.GetComponent<Ai_>().point, collision.transform.localPosition.x, collision.transform.localPosition.z, gameObject.GetComponentInChildren<Renderer>().material.color);
            collision.gameObject.SetActive(false);
        }
        //Ai collide with Food
        else if (collision.gameObject.tag == "Food" && collision.gameObject.GetComponent<Food>().radius < radius && !collision.gameObject.GetComponent<Food>().isEaten)
        {
            collision.gameObject.GetComponent<Food>().isEaten = true;
            OnFoodTouch(collision.gameObject.GetComponent<Food>().point);
            //updateScale();
            //Debug.Log("+ " + collision.gameObject.GetComponent<Food>().point + "points");
            Destroy(collision.gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food" && other.gameObject.GetComponent<Food>().radius < radius)
        {
            OnFoodTouch(other.gameObject.GetComponent<Food>().point);
            Destroy(other.gameObject);
        }
    }
}
