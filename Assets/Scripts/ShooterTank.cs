using UnityEngine;

public class ShooterTank : Shooter
{
    public GameObject bulletPrefab;
    public float cooldownTimer;
    public float cooldownAmount;
    public float damageAmount;
    public PawnTank pawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pawn = GetComponent<PawnTank>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0f)
            {
                cooldownTimer = 0f;
            }
        }
    }

    public override void Shoot()
    {
        if (cooldownTimer > 0f)
            return;
        cooldownTimer = cooldownAmount;
        GameObject bulletObject = Instantiate<GameObject>(bulletPrefab, muzzleLocation.position, muzzleLocation.rotation);
        DamageOnCollision bullet = bulletObject.GetComponent<DamageOnCollision>();
        if (bullet != null)
            bullet.objectFiredBy = pawn.gameObject;
            bullet.damageAmount = damageAmount;
        Rigidbody rb = bulletObject.GetComponent<Rigidbody>();
        rb.AddForce(muzzleLocation.forward * pawn.shootForce);
    }
}
