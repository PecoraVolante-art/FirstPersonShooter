using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject impactPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    { 
    RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
        {
            Debug.Log(hit.transform.name);
            Vector3 spawnPos = hit.point + (hit.normal * 0.01f);

            Quaternion spawnRotation = Quaternion.LookRotation(hit.normal);

            GameObject impact = Instantiate(impactPrefab, spawnPos, spawnRotation);

            Destroy (impact, 5f);
        }
    }
}
