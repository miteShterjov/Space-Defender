using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Instance;
    private int killCounter = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object across scenes
        }
        else Destroy(gameObject);
    }

    void Update()
    {
        //print(killCounter);
    }

    public int GetScore() => killCounter;
    public void AddScore() => killCounter++;
    public void ResetScore() => killCounter = 0;

}
