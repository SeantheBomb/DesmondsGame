using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CombatAgent))]
public class CombatMeleeAttack : MonoBehaviour, IAttack
{

    public bool swordEquipped = false;

    public float swordRadius = 1f;

    public float swordDamage = 3f;

    public GameObject sword;

    public bool isPlayer;

    bool isAttacking;

    public AudioClip[] sfx;

    CombatAgent agent;

    Vector3 startRot;

    private event System.Action<string> _onAttack;

    public System.Action<string> OnAttack
    {
        get { return _onAttack; }
        set { _onAttack = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<CombatAgent>();
        startRot = sword.transform.localEulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartAttack();
        }
    }
    

    public void StartAttack(bool displayOnly = false)
    {
        if(isAttacking == false && isPlayer)
        {
            PlayAudio();
            if(displayOnly == false)OnAttack?.Invoke(GetType().Name);
            StartCoroutine (SwingSword(displayOnly));
        }
    }

    public void ResetSword()
    {
        isAttacking = false;
        StopAllCoroutines();
        sword.transform.localEulerAngles = startRot;
    }


    IEnumerator SwingSword(bool displayOnly = false)
    {
        isAttacking = true;
        Vector3 endRot = startRot + Vector3.forward * -45f;
        for (float i = 0; i < 0.25f; i += Time.deltaTime)
        {
            sword.transform.localEulerAngles = Vector3.Lerp(startRot, endRot, i/0.25f);
            yield return null;
        }
        if(displayOnly == false)agent.AttackOverlapCircle(swordDamage, swordRadius);
        for (float i = 0; i < 0.5f; i += Time.deltaTime)
        {
            sword.transform.localEulerAngles = Vector3.Lerp(endRot, startRot, i/0.5f);
            yield return null;
        }
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, swordRadius);
    }

    void PlayAudio()
    {
        if (sfx.Length == 0)
            return;
        AudioClip clip = sfx[Random.Range(0, sfx.Length)];
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
