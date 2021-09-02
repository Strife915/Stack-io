using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    
    
    private void Update()
    {
        
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    public  void Restart()
    {
        Debug.Log("basildi");
        SceneManager.LoadScene("SampleScene");
    }
   
}
