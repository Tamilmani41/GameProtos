using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class AmmoUI : MonoBehaviour
{
    public PlayerGunController gun;
    public TMP_Text ammo_Text;
    // Start is called before the first frame update
    void Start()
    {
        if (gun != null)
        {
            gun.OnAmmoChanged += UpdateAmmo;
        }
        UpdateAmmo(gun.CurrentAmmo, gun.MaxAmmo);
    }

    // Update is called once per frame
    public void UpdateAmmo(int current, int max)
    {
        ammo_Text.text = $"{current}/{max}";
    }
    private void OnDestroy()
    {
        if (gun != null)
        {
            gun.OnAmmoChanged -= UpdateAmmo;
        }
    }
}
