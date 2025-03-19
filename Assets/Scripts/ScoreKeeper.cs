using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int killCounter = 0;

    void Update()
    {
        print(killCounter);
    }

    public int GetScore() => killCounter;
    public void AddScore() => killCounter++;
    public void ResetScore() => killCounter = 0;
    
}
