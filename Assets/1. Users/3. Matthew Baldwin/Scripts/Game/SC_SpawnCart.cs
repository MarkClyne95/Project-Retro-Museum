using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SC_SpawnCart : MonoBehaviour
{
    [Header("Cartridge Info")]
    [SerializeField] private int toSpawn;
    private Vector3 spawnPos;
    [SerializeField] private GameObject atariCart;
    [SerializeField] private GameObject badtariCart;
    [SerializeField] private GameObject level;

    [Header("Randomizer")]
    [SerializeField] private int range;
    private int randomVal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunRandomizer() => randomVal = Random.Range(0, range);

    private void SpawnCart()
    {
        RunRandomizer();

        switch (randomVal)
        {
            case 1:
                SpawnAtari();
                break;
            case 2:
                SpawnBadtari();
                break;
        }
    }

    private void SpawnAtari()
    {
        spawnPos = this.gameObject.transform.position;
        Instantiate(atariCart, spawnPos, this.gameObject.transform.rotation, level.transform);
    }

    private void SpawnBadtari()
    {
        spawnPos = this.gameObject.transform.position;
        Instantiate(badtariCart, spawnPos, this.gameObject.transform.rotation, level.transform);
    }
}
