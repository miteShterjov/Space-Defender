using UnityEngine;

public class ShieldPerk : MonoBehaviour, IPerk
{
    public static event System.Action OnShieldObjectActive;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float delay = 10f;
    [SerializeField] private string perkName = "ShieldPerk";

    public string PerkName { get => perkName; set => PerkName = value; }

    void Update()
    {
        MoveGameObject();
        OnBecameInvisible();
    }

    public void ApplyPerkEffect(GameObject player)
    {
        // If the player already has a shield, don't apply another one
        // if (GameObject.Find("shield") != null) return;
        // Create a shield object and attach it to the player
        GameObject shield = Instantiate(shieldPrefab, player.transform.position, Quaternion.identity);
        shield.transform.parent = player.transform;
        // Invoke the event that the shield object is active
        //OnShieldObjectActive?.Invoke();
        // Destroy the perk object
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
