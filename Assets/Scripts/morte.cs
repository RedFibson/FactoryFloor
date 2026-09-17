using UnityEngine;
using UnityEngine.SceneManagement;


public class morte : MonoBehaviour
{
    private Vector2 posicaoInicial;
    private Rigidbody2D rb;
    public bool morreu = false;

    private void Start()
    { 
    
    rb= GetComponent<Rigidbody2D>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        morreu = true;

        if (collision.gameObject.CompareTag("morte"))
        { 
        SceneManager.LoadScene("game over");
        
        }
            
            
        
        //ResetarTwist();
    }

    private void ResetarTwist()
    {

        SceneManager.LoadScene("game over");
        Time.timeScale = 1f;
        rb.linearVelocity = Vector2.zero;
        transform.position = posicaoInicial;
        
    }

    
    
   
}