using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WizardStatus
{
    TALKING,
    IDLE,
    MELEE,
    LIGHTNINGMELEE,
    LIGHTNINGSHOOT,
    AOE,
    TORNADOATTACK,
    DEAD
}

public class IS_WizardScript : S_Enemy
{
    public static IS_WizardScript instance;
    public WizardStatus status;
    Animator anim;
    public float statusCh;
    [Header("Lightning on ground")]
    public GameObject clouds;
    public GameObject electrifiedGround;
    public float elecTime;
    [Header("Lightning Shoot attack")]
    public Transform lightningPos;
    public GameObject lightningGO;
    public float lightingTime = 1.18f;
    [Header("Tornado attack")]
    public Transform tornPos;
    public GameObject tornado;
    public float tornadoTime = 0.9f;
    [Header("Lightning from Above")]
    public bool onPadua;
    public GameObject lightningFA;
    public float startLFA = 1.10f;
    public float lfaTime = 2f;
    public float[] targets;
    [Header("Health")]
    public bool isDying;
    public bool isDead;
    public GameObject cutSceneStart;
    [Header("Dialog")] 
    
    [Tooltip("Lighting on Padua")]public bool Lop;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        status = WizardStatus.TALKING;
        anim = GetComponent<Animator>();
    }

    public void StartFight()
    {
        status = WizardStatus.IDLE;
        StartCoroutine(WizardStatuses());
    }

    void Update()
    {
        
    }

   IEnumerator WizardStatuses()
    {
        if (!isDead)
        {
            var randomAttack = Random.Range(1, 7);
            yield return new WaitForSeconds(statusCh);

            switch (randomAttack)
            {
                case 1:
                    //  status = WizardStatus.IDLE;
                    status = WizardStatus.LIGHTNINGMELEE;
                    break;
                case 2:
                    status = WizardStatus.AOE;
                    break;
                case 3:
                    status = WizardStatus.LIGHTNINGMELEE;
                    break;
                case 4:
                    status = WizardStatus.LIGHTNINGSHOOT;
                    break;
                case 5:
                    status = WizardStatus.TORNADOATTACK;
                    break;
                case 6:
                    status = WizardStatus.LIGHTNINGSHOOT;
                   // status = WizardStatus.MELEE;
                    break;
                default:
                    break;
            }
            StatusChanger();
        }
       
    }

    public void playLightning()
    {
    }
    public void StatusChanger()
    {
        switch (status)
        {
            
            case WizardStatus.IDLE:
                anim.SetBool("Idle", true);
                StartCoroutine(WizardStatuses());
                break;
            case WizardStatus.MELEE:
                anim.SetTrigger("Melee");
                StartCoroutine(WizardStatuses());
                break;
            case WizardStatus.LIGHTNINGMELEE:
                anim.SetTrigger("LightningMelee");
                StartCoroutine(HitOnGround());

                StartCoroutine(WizardStatuses());
                break;
            case WizardStatus.LIGHTNINGSHOOT:
                anim.SetTrigger("LightningShoot");
                StartCoroutine(LightningShoot());

                StartCoroutine(WizardStatuses());
                break;
            case WizardStatus.AOE:
                anim.SetTrigger("AOE");
                StartCoroutine(LightningFromAbove());
                
                StartCoroutine(WizardStatuses());
                break;
            case WizardStatus.TORNADOATTACK:
                anim.SetTrigger("TornadoAttack");
                StartCoroutine(TornadoAttack());
                
                StartCoroutine(WizardStatuses());
                break;
            case WizardStatus.DEAD:
                isDying = true;
                anim.SetBool("Dead", true);
                StopCoroutine(WizardStatuses());
                break;

            default:
                break;
        }
    }


    IEnumerator HitOnGround()
    {
        statusCh = 8;
        clouds.gameObject.SetActive(true);
        yield return new WaitForSeconds(elecTime);
        electrifiedGround.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        electrifiedGround.gameObject.SetActive(false);
        //foreach (var item in clouds.GetComponentsInChildren<CloudScript>())
        //{
        //    item.OnDisable();
        //}
        clouds.gameObject.SetActive(false);
        statusCh = 5;
    }

    IEnumerator LightningShoot()
    {
        yield return new WaitForSeconds(lightingTime);

        GameObject lightGO = Instantiate(lightningGO, lightningPos.position, Quaternion.identity);
        lightGO.transform.SetParent(lightningPos);
        lightGO.transform.localPosition = new Vector2(0, 0);
        lightGO.transform.SetParent(null);

        yield return new WaitForSeconds(0.7f);// tiempo en que el rayo dura para tocar el piso.

        electrifiedGround.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        electrifiedGround.gameObject.SetActive(false);
    }
    IEnumerator TornadoAttack()
    {
        yield return new WaitForSeconds(tornadoTime);
        GameObject torn = Instantiate(tornado, tornPos.position, Quaternion.identity);
        torn.transform.SetParent(tornPos);
        torn.transform.localPosition = new Vector2(0, 0);
        torn.transform.SetParent(null);
    }

    IEnumerator LightningFromAbove()
    {
        statusCh = 16;
        yield return new WaitForSeconds(startLFA);
        StartCoroutine(ShootingLightnings());

    }

    IEnumerator ShootingLightnings()
    {
        int i = Random.Range(1, 3);
        if(i == 1)
        {
            onPadua = true;
        }
        else
        {
            onPadua = false;
        }

        if (Lop)
        {
            onPadua = true;
        }

        int counter = 0;
        while (counter < 10)
        {
            var position = Random.Range(0, 11);
            yield return new WaitForSeconds(lfaTime);

            if (onPadua)
            {
                Instantiate(lightningFA, new Vector2(S_MetroidVaniaPlayerController.instance.transform.position.x, 382.869f), Quaternion.identity);
            }
            else
            {
                Instantiate(lightningFA, new Vector2(targets[position], 382.869f), Quaternion.identity);
            }
            
            counter++;
            if (counter > 13)
            {
                break;
            }
        }
        statusCh = 5;

    }

    public void ZeusDead()
    {
        isDying = true;

    }

    IEnumerator DeadAnim()
    {
        
        // ZeuzAct.instance.endDialogue.SetActive(true);
        // yield return new WaitUntil( ()=> isDead);
        yield return new WaitForSeconds(3f);

        anim.SetBool("Dead", false);
        anim.SetTrigger("DeathBolt");

        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);
        gameObject.SetActive(false);
        GetComponent<IS_WizardScript>().enabled = false;
        ActivateDeathBolt();


    }

    public void ActivateDeathBolt()
    {
        isDead = true;
        cutSceneStart.SetActive(true);
    }
}
