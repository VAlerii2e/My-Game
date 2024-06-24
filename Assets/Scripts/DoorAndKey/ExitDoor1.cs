using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor1 : MonoBehaviour
{

    public bool locked;
    float distance;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        locked = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (player != null) 
        {
            distance = Vector2.Distance(player.transform.position, transform.position);
        } 
        if (!locked && distance < 0.5f) 
        {
            SceneManager.LoadScene(2);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            locked = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            locked = true;
        }
    }
}
