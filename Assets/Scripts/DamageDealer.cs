using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    public int GetDamage() => damage;
    public void Hit() => Destroy(gameObject);
}
