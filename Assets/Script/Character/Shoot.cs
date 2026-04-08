using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject impactPrefab;

    private bool canShoot;
    public int magazineCapacity = 20;
    public float reloadTime;
    public float gundamage = 10f;
    [SerializeField]private int currentAmmo;
    [SerializeField] private bool isReloading;
    [SerializeField] private UImanager uiManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = magazineCapacity;
        uiManager.SetAmmo(currentAmmo, magazineCapacity);
    }

    // Update is called once per frame
    void Update()
    {

        if (isReloading) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentAmmo == magazineCapacity)
            {
                Debug.Log("Weapon Already Loaded");
            }

            else 
            {
                StartCoroutine(Reload());
                return;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (currentAmmo > 0)
            {
                Shoot();

            }
            else
            {
                StartCoroutine(Reload());
            }

            
        }
    }

    void Shoot()
    {
        currentAmmo--;
        uiManager.SetAmmo(currentAmmo, magazineCapacity);
        RaycastHit hit;
        if (GestioneSFX.Instance != null)
            GestioneSFX.Instance.PlaySFX(GestioneSFX.Instance.shoot);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
        {
          

            Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(gundamage); 
            }

            Vector3 spawnPos = hit.point + (hit.normal * 0.01f);

            Quaternion spawnRotation = Quaternion.LookRotation(hit.normal);

            GameObject impact = Instantiate(impactPrefab, spawnPos, spawnRotation);

            Destroy (impact, 2f);
        }
    }

    IEnumerator Reload()
    { 
        isReloading = true;
        Debug.Log("Reload");
        if (GestioneSFX.Instance != null)
            GestioneSFX.Instance.PlaySFX(GestioneSFX.Instance.reload);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineCapacity;
        uiManager.SetAmmo(currentAmmo, magazineCapacity);
        isReloading = false;
        Debug.Log("Reloaded!");

    }

}
