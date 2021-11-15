public interface IWeaponGun : IWeapon{
    int SupplyAmmo{
        get;
    }
    int MaxAmmo{
        get;
    }

    void Shoot();
    void FillAmmo();
}
