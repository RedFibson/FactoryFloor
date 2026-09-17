using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public enum TipoArmadilha
    {
        Espetos,
        Torreta
    }

    [Header("Configuração Geral")]
    [SerializeField] private TipoArmadilha tipoArmadilha;
    private Animator animator;



    [Header("Configuração dos Espetos")]
    [SerializeField] private float atrasoInicial = 1f;
    [SerializeField] private Collider2D colisorDano;
    [SerializeField] private float tempoAtivo = 2f;
    [SerializeField] private float tempoInativo = 2f;

    [Header("Configuração da Torreta")]
    [SerializeField] private float tempoEntreDisparos = 3f;
    [SerializeField] private GameObject prefabProjetil;
    [SerializeField] private Transform pontoDisparo;
    [SerializeField] private float velocidadeProjetil = 5f;
    
    private AudioSource tocadorAudio;
    public AudioClip disparo;

    private float contadorTempo;
    private bool iniciou;
    private int estadoEspetos = 1;
    private int estadoTorreta = 0;
    private int estadoAtual = 0;

    private void Start()
    {
        tocadorAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        contadorTempo = atrasoInicial;

        if (colisorDano != null)
        {
            colisorDano.enabled = false;
        }

        if (animator != null)
        {
            animator.SetInteger("estado", 0);
        }
    }

    private void Update()
    {
        if (!iniciou)
        {
            contadorTempo -= Time.deltaTime;

            if (contadorTempo <= 0f)
            {
                iniciou = true;
                contadorTempo = 0f;
            }

            return;
        }

        switch (tipoArmadilha)
        {
            case TipoArmadilha.Espetos:
                ControlarEspetos();
                break;

            case TipoArmadilha.Torreta:
                ControlarTorreta();
                break;
        }
    }

    private void ControlarEspetos()
    {
        contadorTempo += Time.deltaTime;

        if (contadorTempo <= tempoAtivo)
        {
            animator.SetInteger("estado", 1);
            colisorDano.enabled = true;

        }
        else if (contadorTempo <= tempoAtivo + tempoInativo)
        {
            animator.SetInteger("estado", 2);
            colisorDano.enabled = false;
        }
        else
        {
            contadorTempo = 0f;
        }

    }

    private void ControlarTorreta()
    {
        contadorTempo += Time.deltaTime;

        if (contadorTempo >= tempoEntreDisparos)
        {
            
            MudarEstadoAnimacao(1); // Ativa animação de disparo
            contadorTempo = 0f;
        }
        else if (contadorTempo >= 0.3f) // Retorna ao Idle após um breve tempo de disparo
        {
            MudarEstadoAnimacao(0);
        }
    }

    public void Disparar()
    {
        if (prefabProjetil == null || pontoDisparo == null)
        {
            Debug.LogWarning("Torreta sem prefabProjetil ou pontoDisparo configurado.");
            return;
        }

        GameObject projetil = Instantiate(prefabProjetil, pontoDisparo.position, pontoDisparo.rotation);
        tocadorAudio.PlayOneShot(disparo);
        Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();

        
    }

    private void MudarEstadoAnimacao(int novoEstado)
    {
        if (animator == null || estadoAtual == novoEstado) return;

        estadoAtual = novoEstado;
        animator.SetInteger("estado", novoEstado);
    }
}
