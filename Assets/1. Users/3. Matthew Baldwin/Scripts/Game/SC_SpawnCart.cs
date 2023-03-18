using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using System.Collections.Generic;

public class SC_SpawnCart : MonoBehaviour
{
    [Header("Cartridge Info")]
    private Vector3 spawnPos;
    [SerializeField] private GameObject atariCart;
    [SerializeField] private GameObject badtariCart;
    [SerializeField] private GameObject cartridgeHolder;
    public byte toSpawn;
    public byte cartsAlive;


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
        if (toSpawn > 0)
        {
            RunRandomizer();

            spawnPos = this.gameObject.transform.position;

            switch (randomVal)
            {
                case 1:
                    SpawnAtari();
                    toSpawn--;
                    cartsAlive++;
                    break;
                case 2:
                    SpawnBadtari();
                    toSpawn--;
                    cartsAlive++;
                    break;
            }
        }
    }

    private void SpawnAtari() => Instantiate(atariCart, spawnPos, this.gameObject.transform.rotation, cartridgeHolder.transform);

    private void SpawnBadtari() => Instantiate(badtariCart, spawnPos, this.gameObject.transform.rotation, cartridgeHolder.transform);
}
