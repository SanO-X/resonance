using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal1 : MonoBehaviour
{
    bool isPlayerInside;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
         
    }


   private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
   }


}

// This script handles the portal functionality, allowing the player to enter a new scene when they collide with the portal.