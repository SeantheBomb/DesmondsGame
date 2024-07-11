using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageVFX : MonoBehaviour, IDamageReceiver
{

    public SpriteRenderer sprite;

    public float blinkDuration = 0.1f;
    public float blinkAmount = 6;

    public float Priority => 0;

    public DamageInfo ReceiveDamage(Health health, DamageInfo damageInfo)
    {
        StartCoroutine(BlinkVFX());
        return damageInfo;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        if (sprite == null) sprite = GetComponent<SpriteRenderer>(); 
        sprite.enabled = false;
    }

    IEnumerator BlinkVFX()
    {
        for (int i = 0; i < blinkAmount; i++)
        {
            sprite.enabled = true;
            yield return new WaitForSeconds(blinkDuration);
            sprite.enabled = false;
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    [Button]
    void Test()
    {
        StartCoroutine(BlinkVFX());
    }

}
