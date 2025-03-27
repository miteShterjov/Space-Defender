using UnityEngine;

public interface IPerk 
{
    void MoveGameObject();
    void ApplyPerkEffect(GameObject player);
    void OnBecameInvisible();
}
