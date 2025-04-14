using System.Collections;
using UnityEngine;

public class PowerPerk : MonoBehaviour, IPerk
{
    [SerializeField] private string perkName = "PowerPerk";
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float delay = 10f;
    

    public string PerkName { get => perkName; set => perkName = value; }

    void Update()
    {
        MoveGameObject();
        OnBecameInvisible();
    }

    public void ApplyPerkEffect(GameObject player)
    {
        //while this perk is active the player will shoot 3 projectiles at once
        //if the player already has this perk, don't apply it again
        player.GetComponent<Shooter>().HasPowerPerk = true;
        Destroy(gameObject);
    }

    public void MoveGameObject()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject, delay);
    }

}
