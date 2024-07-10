using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class OnTriggerLoadScene : MonoBehaviour
{

    public string sceneName;

    public AudioClip sfx;

    //public int delay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnTriggerActive();
        }        
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    [Button]
    public void OnTriggerActive()
    {
        AudioSource.PlayClipAtPoint(sfx, transform.position);
        Invoke(nameof(LoadScene), sfx.length);
    }
}
