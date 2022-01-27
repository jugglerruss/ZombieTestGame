using UnityEngine;

internal class AmmoBox : MonoBehaviour
{
    private int Ammo;
    public void Init(int ammo)
    {
        Ammo = ammo;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out Hero hero))
        {
            hero.TakeAmmoBox(Ammo);
            Destroy(gameObject);
        }
            
    }
}