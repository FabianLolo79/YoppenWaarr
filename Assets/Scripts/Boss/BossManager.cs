using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using FMODUnity;
using System.Collections;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;

    [Header("Vida del Boss")]
    public int vidaMaxima = 100;
    private int vidaActual;
    public Image barraEnergia;

    [Header("IA y daño visual")]
    public BossAI bossAI;
    public SpriteRenderer spriteRenderer;
    public GameObject damageParticlePrefab;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.15f;
    private NavMeshAgent navAgent;

    private bool bossActivado = false;
    public bool esInvulnerable = false;


    [Header("FMOD")]
    [SerializeField] private EventReference damageSFX;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {

        vidaActual = vidaMaxima;
        navAgent = GetComponent<NavMeshAgent>();
        ActualizarBarra();
        ActivarBoss();
    }

    // Reducido por eliminación de grupos
    public void ReducirVida(int cantidad)
    {
        vidaActual = Mathf.Max(vidaActual - cantidad, 0);
        ActualizarBarra();

        if (!bossActivado && vidaActual <= vidaMaxima / 2)
        {
            ActivarBoss();
        }

        if (vidaActual <= 0)
        {
            Muerte();
        }
    }

    // Daño directo (espada, proyectil)
    public void RecibirDaño(int cantidad, Vector2 knockbackDir)
    {
        if (vidaActual <= 0 || esInvulnerable) return;

        StartCoroutine(FlashDamage());
        SpawnDamageParticles();

        RuntimeManager.PlayOneShot(damageSFX, transform.position);
        StartCoroutine(SimulateKnockback(knockbackDir, knockbackDuration));

        vidaActual = Mathf.Max(vidaActual - cantidad, 0);
        ActualizarBarra();

        /*if (!bossActivado && vidaActual <= vidaMaxima / 2)
        {
            ActivarBoss();
        }*/

        if (vidaActual <= 0)
        {
            Muerte();
        }
    }

    private void ActivarBoss()
    {
        bossActivado = true;
        bossAI?.MoveTowardsPlayer();
        Debug.Log("¡Boss activado!");
    }

    private void ActualizarBarra()
    {
        if (barraEnergia != null)
            barraEnergia.fillAmount = (float)vidaActual / vidaMaxima;
    }

    private void Muerte()
    {
        Debug.Log("¡Boss derrotado!");
        GameManager.Instance?.Victory();
        Destroy(gameObject); // o animación previa
    }

    private void SpawnDamageParticles()
    {
        if (damageParticlePrefab != null)
        {
            GameObject particles = Instantiate(damageParticlePrefab, transform.position, Quaternion.identity);
            Destroy(particles, 1f);
        }
    }

    private IEnumerator FlashDamage()
    {
        Color originalColor = spriteRenderer.color;
        for (int i = 0; i < 2; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator SimulateKnockback(Vector2 direction, float duration)
    {
        if (navAgent != null) navAgent.enabled = false;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position += (Vector3)(direction * knockbackForce * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (navAgent != null) navAgent.enabled = true;
    }
}


