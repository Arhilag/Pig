using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private GameObject bomb;
    [SerializeField] private JoisticController controller;

    void FixedUpdate()
    {
        var y = controller.Vertical();
        var x = controller.Horizontal();

        int dx = 0;
        if (x > 0) dx = 1;
        else if (x < 0) dx = -1;

        int dy = 0;
        if (y > 0) dy = 1;
        else if (y < 0) dy = -1;

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if(dx > 0) GetComponent<SpriteRenderer>().sprite = sprites[0];
            else if (dx < 0) GetComponent<SpriteRenderer>().sprite = sprites[1];
            transform.position += new Vector3(0.1f * speed * dx, 0, 0);
        }
        else if (Mathf.Abs(x) < Mathf.Abs(y))
        {
            if (dy > 0) GetComponent<SpriteRenderer>().sprite = sprites[3];
            else if (dy < 0) GetComponent<SpriteRenderer>().sprite = sprites[2];
            transform.position += new Vector3(0, 0.1f * speed * dy, 0);
        }

    }

    public void SpawnBomb()
    {
        Instantiate(bomb, transform.position, transform.rotation);

    }

    public void Damage()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        StartCoroutine(Timer(3));
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
