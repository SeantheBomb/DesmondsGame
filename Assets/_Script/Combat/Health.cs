using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public float health;
    public float maxHealth;

    public UnityEvent OnTakeDamageEvent;
    public UnityEvent OnDeathEvent;

    bool immune = false;

    public System.Action<DamageInfo> OnTakeDamage, OnDeath;

    public System.Action<float> OnHeal;

    public AudioClip sfx;


    private void OnEnable()
    {
        immune = false;
    }


    public float TakeDamage(DamageInfo damage)
    {
        Debug.Log($"{name} took {damage.healthTaken} damage from {damage.source.name}", damage.source);
        if (immune)
            return health;
        StartCoroutine(PlayIFrames());
        if(sfx)AudioSource.PlayClipAtPoint(sfx, transform.position);
        damage = ProcessDamage(damage);
        health -= damage.healthTaken;
        if(health <= 0)
        {
            OnDeathEvent?.Invoke();
            OnDeath?.Invoke(damage);
        }
        else
        {
            OnTakeDamageEvent?.Invoke();
            OnTakeDamage?.Invoke(damage);
        }
        return health;
    }

    public float Heal(float healthGiven)
    {
        health += healthGiven;
        if(health > maxHealth)
            health = maxHealth;
        OnHeal?.Invoke(health);
        return health;
    }

    public float ResetHealth()
    {
        health = maxHealth;
        OnHeal?.Invoke(health);
        return health;
    }

    DamageInfo ProcessDamage(DamageInfo damage)
    {
        IDamageReceiver[] receivers = GetComponentsInChildren<IDamageReceiver>();
        receivers = receivers.OrderByDescending((x)=>x.Priority).ToArray();
        foreach(IDamageReceiver receiver in receivers)
        {
            damage = receiver.ReceiveDamage(this, damage);
        }
        return damage;
    }

    IEnumerator PlayIFrames()
    {
        immune = true;
        yield return new WaitForSeconds(1f);
        immune = false;
    }

    [Button]
    void TakeDamage()
    {
        TakeDamage(new DamageInfo() { hitPoint = (Vector2)transform.position + Random.insideUnitCircle, source = GetComponent<CombatAgent>(), healthTaken = 10 });
    }
    
}
