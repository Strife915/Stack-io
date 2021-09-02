using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    
    public int  kill;
    public float moveSpeed;
    public float point;
    public Color color;
    public Renderer ActorGraphics;
    protected float TurnAbility => 10;

    public float radius { set { } get { return GetComponentInChildren<SphereCollider>().radius * transform.localScale.x; } }
    private void Awake()
    {
        SetColor();
    }

    private void SetColor()
    {
        ActorGraphics = GetComponentInChildren<Renderer>();
        ActorGraphics.material.color = color;
    }    
    public void updateScale_(float point_x, float point_y, float point_z)
    {
        transform.localScale = new Vector3(point_x, point_y, point_z);
    }
    public void updateScale()
    {
        if (point >= 1 && point <= 9)
        {
            updateScale_(1, point / 20, 1);
        }
        else if (point == 9)
        {
            updateScale_(1, 0.45f, 1);
        }
        else if (point > 9)
        {
            updateScale_(point / 10, point / 20, point / 10);
        }
    }
    public void OnFoodTouch(float score)
    {
        point += score; 
    }
    public  void Movement(Rigidbody rb,Vector3 movementDirection)
    {
        rb.velocity = (movementDirection.normalized * moveSpeed * Time.deltaTime);
    }
    

    
}
