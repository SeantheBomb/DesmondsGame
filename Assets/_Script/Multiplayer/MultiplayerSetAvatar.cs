using Fusion;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class MultiplayerSetAvatar : NetworkBehaviour
{

    SpriteRenderer renderer;

    [SerializeField] Sprite[] sprites;

    int index;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        yield return new WaitUntil(()=>Object != null);
        SetSprite(Object.InputAuthority.AsIndex - 1);
    }


    public void SetSprite(int index)
    {
        Debug.Log($"MultiplayerSetAvatar: {name} set sprite to index {index}");
        renderer.sprite = sprites[index];
        this.index = index;
    }

    [ContextMenu("Next Avatar")]
    public void NextSprite()
    {
        if(index >= sprites.Length)
        {
            SetSprite(0);
            return;
        }
        SetSprite(index + 1);
    }


}
