using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGunController : MonoBehaviour
{
    [SerializeField] float range = 100f;
    [SerializeField] float fireRate = 15f;
    [SerializeField] LineRenderer shootLine;
    [SerializeField] float rayLingeringTime = 1f;

    private float nextTimeToFire = 0f;


    // Start is called before the first frame update
    void Start()
    {
        shootLine.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot ()
    {
        if (Time.time >= nextTimeToFire)
        {
            // fire rate update
            nextTimeToFire = Time.time + 1f / fireRate;

            // trying hit
            RaycastHit hit;
            bool didHit = Physics.Raycast(this.transform.position, this.transform.forward, out hit, range);
            if (didHit)
            {
                Debug.Log($"Successful hit: {hit.transform.name}");
                WeaponTarget target = hit.transform.GetComponent<WeaponTarget>();
                if (target != null)
                {
                    target.GetShot();
                }
            }

            // shooting raycast
            StartCoroutine(RenderRay());
            
        }
    }

    public IEnumerator RenderRay()
    {
        shootLine.SetPosition(0, this.transform.position);
        shootLine.SetPosition(1, this.transform.position + this.transform.forward.normalized * range);
        shootLine.enabled = true;

        yield return new WaitForSeconds(rayLingeringTime);

        shootLine.enabled = false;
    }
}
