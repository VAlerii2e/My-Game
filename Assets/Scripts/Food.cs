using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float healthRestoreAmount = 20f; // Кількість здоров'я, яку відновлює їжа

    // Коли гравець зіштовхується з їжею
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.RestoreHealth(healthRestoreAmount);
                Destroy(gameObject); // Знищити об'єкт їжі після з'їдання
            }
        }
    }
}
