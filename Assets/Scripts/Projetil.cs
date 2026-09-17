using UnityEngine;

public class Projetil : MonoBehaviour
{
    private Rigidbody2D rb;// Start is called once before the first execution of Update after the MonoBehaviour is created
   public float velocidade = 4f;
    public float tempoDestruicao = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.left * velocidade;
        Destroy(gameObject, tempoDestruicao);

    }

 }
