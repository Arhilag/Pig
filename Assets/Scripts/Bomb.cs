using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] public float time;
    [SerializeField] private List<GameObject> characters;
    void Start()
    {
        StartCoroutine(BombLife());
    }

    private IEnumerator BombLife()
    {
        yield return new WaitForSeconds(time);
        for(int i = 0; i < characters.Count; i++)
        {
            if(characters[i].tag == "Player")
            {
                characters[i].GetComponent<Player>().Damage();
            }
            if (characters[i].tag == "enemy")
            {
                characters[i].GetComponent<Enemy>().Crap();
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "enemy")
        {
            characters.Add(col.gameObject);
        }
        else if (col.tag == "Player")
        {
            characters.Add(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "enemy")
        {
            characters.Remove(col.gameObject);
        }
        else if (col.tag == "Player")
        {
            characters.Remove(col.gameObject);
        }
    }
}
