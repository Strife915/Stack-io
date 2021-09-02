using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    

    public Transform target;
    Player player;



    public float smoothSpeed = 5f;
    public Vector3 offset;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        player = target.GetComponent<Player>();
        
        
    }
    void LateUpdate()
    {       
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        transform.LookAt(target);
        if (player.point < 30)
        {
            offset.y = 12;
            offset.z = -15;
        }
        if (player.point > 30)
        {
            offset.y = player.point / 2.5f;
        }
        if (player.point > 50)
        {
            offset.y = player.point / 2.5f;
            offset.z = -(player.point / 3.5f);
        }
    }

}