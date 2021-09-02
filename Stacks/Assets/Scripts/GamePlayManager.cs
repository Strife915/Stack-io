using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Starting, Playing, End };
public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance;
    public List<Actor> Actors = new List<Actor>();
    public List<Actor> DeadActors = new List<Actor>();
    public State GameState;
    public float startTimer = 4;
    public float timeLeft = 30.0f;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        Actors.AddRange(FindObjectsOfType<Actor>());
        GameState = State.Starting;
        
    }
    public void Update()
    {
        switch (GameState)
        {
            case State.Starting:
                Spawner.Instance.Spawn = false;
                break;
            case State.Playing:
                Spawner.Instance.Spawn = true;
                Spawner.Instance.SpawnFood();
                break;
            case State.End:
                break; 
        }

        if (startTimer <= 0)
        {
            GameState = State.Playing;
        }
        if(timeLeft <= 0)
        {
            GameState = State.End;
        }
        
        
            
    }

}


