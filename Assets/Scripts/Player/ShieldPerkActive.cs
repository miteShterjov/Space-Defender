using UnityEngine;

public class ShieldPerkActive : MonoBehaviour
{
    [SerializeField] Sprite[] ShieldSprites;
    [SerializeField] private float shieldMaxHp = 40.0f;

    private SpriteRenderer spriteRenderer;
    private bool hasShieldUp = false;

    public bool HasShieldUp { get => hasShieldUp; set => hasShieldUp = value; }
    public float ShieldMaxHp { get => shieldMaxHp; set => shieldMaxHp = value; }

    private void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ShieldSprites[2];
    }

    void Update()
    {
        if (hasShieldUp) ShieldsUp();
    }


    public void ShieldsUp()
    {
        float maxShieldHP = 40.0f;

        if (ShieldMaxHp == maxShieldHP)
        {
            ShieldSpriteActive(2);
        }
        if (ShieldMaxHp <= maxShieldHP / 2)
        {
            ShieldSpriteActive(1);
        }
        if (ShieldMaxHp <= maxShieldHP / 3)
        {
            ShieldSpriteActive(0);
        }
        if (maxShieldHP <= 0)
        {
            ShieldSpriteActive(-1);
        }
    }

    public void ShieldSpriteActive(int index)
    {
        if (index == -1)
        {
            hasShieldUp = false;
            spriteRenderer.sprite = null;
        }
        else
        {
            hasShieldUp = true;
            spriteRenderer.sprite = ShieldSprites[index];
        }
    }
}
