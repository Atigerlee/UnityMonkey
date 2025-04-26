using System.Collections;
using UnityEngine;
using UnityEngine.UI; 

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime = 3f;
    public Camera playerCamera;
    
    public int maxMagazineSize = 30;   
    private int currentAmmo;            
    public float reloadTime = 2f;       
    private bool isReloading = false;   
    public float fireRate = 0.1f; 
    private float nextFireTime = 0f;

    public Text ammoUIText;

    void Start()
    {
        // init ammo
        currentAmmo = maxMagazineSize;
        UpdateAmmoUI();
    }

    


    void Update()
    {
        if (isReloading)
            return;

        // shot single
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo > 0 && Time.time >= nextFireTime)
        {
            FireWeapon();
            nextFireTime = Time.time + fireRate;
        }

        // shoot conti
        if (Input.GetKey(KeyCode.Mouse0) && currentAmmo > 0 && Time.time >= nextFireTime)
        {
            FireWeapon();
            nextFireTime = Time.time + fireRate;
        }

        // R to reload
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxMagazineSize && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void FireWeapon()
    {
    // get the middle
    Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    Vector3 shootDirection = ray.direction;

    // generating bullet
    GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.LookRotation(shootDirection));
    Rigidbody rb = bullet.GetComponent<Rigidbody>();
    rb.velocity = shootDirection * bulletVelocity;

    currentAmmo--;
    Debug.Log("剩餘子彈: " + currentAmmo);

    UpdateAmmoUI();
    StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
    }


    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("正在裝填...");

        yield return new WaitForSeconds(reloadTime);

        // reload
        currentAmmo = maxMagazineSize;
        isReloading = false;
        

        
        UpdateAmmoUI();
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    // update UI
    private void UpdateAmmoUI()
    {
        if (ammoUIText != null)
        {
            ammoUIText.text = "子彈: " + currentAmmo.ToString();
        }
    }

    

}
