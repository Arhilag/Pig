using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<Sprite> spritesCrap;
    private List<Sprite> spritesTemprorary;

    public List<Node> nodes;
    [SerializeField] private int i;
    private float progress;
    [SerializeField] private float step;
    private int napr;
    void Start()
    {
        spritesTemprorary = sprites;
        transform.position = new Vector3(nodes[i].x, nodes[i].y, transform.position.z);
        napr = nodes[i].ConnectedNode[UnityEngine.Random.Range(0, nodes[i].ConnectedNode.Length)];
        switchTexture();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(new Vector3(nodes[i].x,
            nodes[i].y, transform.position.z), new Vector3(
                nodes[napr].x,
                nodes[napr].y,
                transform.position.z), progress);
        progress += step;

        if (transform.position.x == nodes[napr].x &&
            transform.position.y == nodes[napr].y)
        {
            i = napr;
            progress = 0;
            napr = nodes[i].ConnectedNode[UnityEngine.Random.Range(0, nodes[i].ConnectedNode.Length)];
            switchTexture();
        }
    }

    private void switchTexture()
    {
        if (nodes[i].x < nodes[napr].x)
            GetComponent<SpriteRenderer>().sprite = spritesTemprorary[0];
        else if (nodes[i].x > nodes[napr].x)
            GetComponent<SpriteRenderer>().sprite = spritesTemprorary[1];
        else if (nodes[i].y < nodes[napr].y)
            GetComponent<SpriteRenderer>().sprite = spritesTemprorary[3];
        else if (nodes[i].y > nodes[napr].y)
            GetComponent<SpriteRenderer>().sprite = spritesTemprorary[2];
    }

    public void Crap()
    {
        spritesTemprorary = spritesCrap;
        switchTexture();
        StartCoroutine(Timer(3));
        
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        spritesTemprorary = sprites;
        switchTexture();
    }
}

[Serializable]
public class Node
{
    public float x;
    public float y;
    public int[] ConnectedNode;
}