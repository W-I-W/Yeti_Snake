using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class YetiFood : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isCharacter = collision.TryGetComponent(out PlayerCharacter player);
        if (isCharacter)
        {
            player.AddBody();
            transform.position = new Vector2(
                Random.Range(-5, 5),
                Random.Range(-7, 7)) *
                new Vector2(0.4f, 0.4f);

            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
    }
}
