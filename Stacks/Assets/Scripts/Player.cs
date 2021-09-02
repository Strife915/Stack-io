using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public  class Player : Actor
{
    public Text score_Text, Kill_Count;
    Rigidbody rb;
    public Vector3 movementDirection_;
    public VariableJoystick variableJoystick;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        kill = 0;
        moveSpeed = 300;
        point = 0;       
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (GamePlayManager.Instance.GameState == State.Playing)
        {
            MovePlayer();
            PlayerTextUpdate();
            updateScale();
        }     
    }
    void MovePlayer()
    {
        Vector3 currentDirection = Vector3.zero;    
        if (variableJoystick.Direction != Vector2.zero)
        {
            if (variableJoystick.Direction.normalized.magnitude <= 0)
            {
                Movement(rb, currentDirection);
            }
            else
            {
                var nextDirection = new Vector3(variableJoystick.Horizontal, 0, variableJoystick.Vertical).normalized;
                Movement(rb, Vector3.Lerp(currentDirection, nextDirection, Time.deltaTime * TurnAbility));
            }
        }
        else
        {
            Movement(rb, currentDirection);
        }
    }
    private void PlayerTextUpdate()
    {
        score_Text.text = point.ToString();
        Kill_Count.text = "Kill : " + kill.ToString();
    } 
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food" && other.gameObject.GetComponent<Food>().radius < radius && !other.GetComponent<Food>().isEaten)
        {
            //Debug.Log("Player collide with food");
            OnFoodTouch(other.gameObject.GetComponent<Food>().point);
            other.GetComponent<Food>().isEaten = true;
            //camera.offset.y += other.gameObject.GetComponent<Food>().point/10;
            //camera.offset.z = -(camera.offset.y * 0.6f);
            //updateScale();
            //Debug.Log("+ " + other.gameObject.GetComponent<Food>().point + "points");
            Destroy(other.gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ai" && collision.gameObject.GetComponent<Ai_>().point < point)
        {
            point = point - collision.gameObject.GetComponent<Ai_>().point;
            //updateScaleAfterExplosion();
            Spawner.Instance.Explode(collision.gameObject.GetComponent<Ai_>().point, collision.transform.localPosition.x, collision.transform.localPosition.z, collision.gameObject.GetComponent<Ai_>().color);
            Spawner.Instance.Explode(collision.gameObject.GetComponent<Ai_>().point, collision.transform.localPosition.x, collision.transform.localPosition.z, gameObject.GetComponentInChildren<Renderer>().material.color);
            kill += 1;
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Food" && collision.gameObject.GetComponent<Food>().radius < radius)
        {
            OnFoodTouch(collision.gameObject.GetComponent<Food>().point);
            //updateScale();
            //Debug.Log("+ " + collision.gameObject.GetComponent<Food>().point + "points");
            Destroy(collision.gameObject);
        }
        //Ai kills
        else if (collision.gameObject.tag == "Ai" && collision.gameObject.GetComponent<Ai_>().point > point)
        {
            GamePlayManager.Instance.GameState = State.End;
        }      
    }
}
