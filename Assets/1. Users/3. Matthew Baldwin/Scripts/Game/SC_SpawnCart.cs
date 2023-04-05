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
    private byte toSpawnReset;
    public byte cartsAlive;


    [Header("Randomizer")]
    [SerializeField] private int range;
    private int randomVal;
    private float randomPos;
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RunRandomizer() => randomVal = Random.Range(0, range);

    public void RunPositionRandomizer() => randomPos = Random.Range(-7.5f, 7.5f);

    private void SpawnCart()
    {
        if (toSpawn > 0)
        {
            RunRandomizer();
            RunPositionRandomizer();

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

    private void SpawnAtari() => Instantiate(atariCart, new Vector3(randomPos, spawnPos.y, spawnPos.z), this.gameObject.transform.rotation, cartridgeHolder.transform);
    
    private void SpawnBadtari() => Instantiate(badtariCart, new Vector3(randomPos, spawnPos.y, spawnPos.z), this.gameObject.transform.rotation, cartridgeHolder.transform);

    private void OnDisable()
    {
        toSpawn = toSpawnReset;
        cartsAlive = 0;
    }

    private void OnEnable()
    {
        toSpawnReset = toSpawn;
        cartsAlive = 0;
    }
}
