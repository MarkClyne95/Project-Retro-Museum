using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healhRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable)
        {
            MetroidScoring.totalScore += 1000;
            bool wasHealed = damageable.Heal(healhRestore);

            if (wasHealed)
                Destroy(gameObject);

        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

}
