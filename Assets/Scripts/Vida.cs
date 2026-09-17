using UnityEngine;
using UnityEngine.SceneManagement;

public class Vida : MonoBehaviour
{
    [Header("Configurações de Vida")]
    [SerializeField] private int vidaMaxima = 5;
    private int vidaAtual;

    private void Start()
    {
        // Inicializa a vida atual com o valor máximo definido
        vidaAtual = vidaMaxima;
    }


    /// Método público chamado por scripts externos para aplicar dano ao Player.
    public void ReceberDano(int valorDano)
    {
        // Garante que não processará dano se o jogador já estiver morto ou se o valor for inválido
        if (vidaAtual <= 0 || valorDano <= 0) return;

        // Subtrai o valor do dano recebido
        vidaAtual -= valorDano;
        

        // Verifica se a vida chegou a zero ou menos
        if (vidaAtual <= 0)
        {
            vidaAtual = 0;
            Morrer();
        }
        Debug.Log($"[PLAYER] Recebeu {valorDano} de dano! Vida restante: {vidaAtual}");

    }

    private void Morrer()
    {
        Debug.Log("[PLAYER] O jogador morreu!");
        // Recarrega a cena atual quando o jogador morre
        SceneManager.LoadScene("game over");
    }
}

