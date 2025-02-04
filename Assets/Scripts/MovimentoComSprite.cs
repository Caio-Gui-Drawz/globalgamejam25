using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public Transform balloon;  // O bal�o (objeto pai) que controla o movimento
    public float swingStrength = 5f;  // Intensidade do movimento de p�ndulo
    public float smoothSpeed = 0.1f;  // Suavidade do movimento

    private Rigidbody rb;  // Refer�ncia ao Rigidbody do objeto pendurado
    private Vector3 initialOffset;  // Dist�ncia inicial entre o bal�o e o objeto pendurado

    void Start()
    {
        // Obter o Rigidbody do objeto pendurado
        rb = GetComponent<Rigidbody>();

        // Congelar a rota��o do Rigidbody para que o objeto pendurado n�o gire livremente
        rb.freezeRotation = true;

        // Armazenar o offset inicial entre o bal�o e o objeto pendurado
        initialOffset = transform.position - balloon.position;
    }

    void FixedUpdate()
    {
        // Calcular a posi��o alvo com base na posi��o do bal�o + o offset inicial
        Vector3 targetPosition = balloon.position + initialOffset;

        // Calcular a dist�ncia horizontal entre o bal�o e o objeto pendurado
        float distanceX = targetPosition.x - transform.position.x;

        // Calcular o �ngulo do p�ndulo com base na dist�ncia (quanto mais o bal�o se move, maior o movimento do p�ndulo)
        float targetAngle = Mathf.Clamp(distanceX * swingStrength, -30f, 30f);  // Limita o �ngulo de oscila��o

        // Interpola��o suave do �ngulo atual para o �ngulo alvo
        float currentAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, smoothSpeed * Time.deltaTime);

        // Aplicar o movimento pendular ao objeto pendurado (no eixo Z, j� que estamos simulando um p�ndulo)
        rb.MoveRotation(Quaternion.Euler(0, 0, currentAngle));

        // Manter o objeto pendurado preso ao bal�o (sem permitir que ele se desloque no eixo X e Y)
        rb.MovePosition(targetPosition);
    }
}
