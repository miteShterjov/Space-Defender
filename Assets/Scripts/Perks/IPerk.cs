using UnityEngine;

public interface IPerk 
{
    string PerkName { get; set; }
    void MoveGameObject();
    void ApplyPerkEffect(GameObject player);
    void OnBecameInvisible();
}
