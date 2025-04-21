using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy_Controller : MonoBehaviour
{
    [Header("Datos base del enemigo")]
    public float speed = 2f;
    public float detectionRadius = 5f;
    public float attackRange = 1f;
    public float attackDelay = 0.5f; // Tiempo de anticipación antes de atacar
    public int damagePerHit = 25;

    [Header("Referencias")]
    public Transform target; // El jugador
    private Rigidbody2D rb;
    private Enemy_State_Machine stateMachine;

    public Enemy_Wander_State wanderState;
    public Enemy_Chase_State chaseState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        wanderState = new Enemy_Wander_State(this);
        chaseState = new Enemy_Chase_State(this);
        stateMachine = new Enemy_State_Machine();
    }

    private void Start()
    {
        stateMachine.ChangeState(wanderState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void ChangeState(IEnemy_State newState)
    {
        stateMachine.ChangeState(newState);
    }

    public void MoveTowards(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction.normalized * speed * Time.fixedDeltaTime);
    }

    public bool CanSeePlayer()
    {
        if (target == null) return false;
        float distance = Vector2.Distance(transform.position, target.position);
        return distance <= detectionRadius;
    }

    public bool IsPlayerInRange()
    {
        if (target == null) return false;
        float distance = Vector2.Distance(transform.position, target.position);
        return distance <= attackRange;
    }

    public void Attack()
    {
        // Calcular la dirección hacia el jugador
        Vector2 direction = (target.position - transform.position).normalized;

        // Realizar un raycast hacia el jugador
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackRange);
        
        // Verificar si el raycast impacta al jugador
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerHit);
                Debug.Log("Ataque exitoso al jugador");
            }
        }
        else
        {
            Debug.Log("El ataque falló, el jugador no está en rango");
        }
    }
}