using UnityEditor.Experimental.GraphView;
using UnityEngine;

using UnityEngine;

public class BossAI : MonoBehaviour
{
    [HideInInspector] public BossManager bossManager;

    public Animator Animator;
    public float velocidadMovimiento = 2f;
    public float distanciaMinima = 1.5f;

    private Transform objetivoJugador;
    private BossState currentState;

    [Header("Melee Attack")]
    public float radiusAttack = 1.2f;
    public Transform pointAttack;
    public LayerMask playerLayer;
    public int MeleeDamage = 1;

    private bool _possibleDamage = false;

    void Start()
    {
        Animator = GetComponent<Animator>();
        currentState = new IdleState(this);
        currentState.Enter();
    }

    void Update()
    {
        currentState.Update();
    }

    public void ChangeState(BossState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void MoveTowardsPlayer()
    {
        if (objetivoJugador == null)
            objetivoJugador = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (objetivoJugador != null)
        {
            Vector2 dir = (objetivoJugador.position - transform.position).normalized;
            transform.position += (Vector3)dir * velocidadMovimiento * Time.deltaTime;
        }
    }

    public bool InAttackRange()
    {
        if (objetivoJugador == null)
            return false;

        return Vector2.Distance(transform.position, objetivoJugador.position) <= distanciaMinima;
    }

    public bool ShouldDisappear()
    {
        return objetivoJugador != null;
    }

    public void PerformAttack()
    {
        Collider2D player = Physics2D.OverlapCircle(pointAttack.position, radiusAttack, playerLayer);
        if (player != null)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(MeleeDamage);
        }
       
        
    }
    public void OrientTowardsPlayer()
    {
        if (objetivoJugador == null)
            return;

        float dirX = objetivoJugador.position.x - transform.position.x;

        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * (dirX >= 0 ? -1 : 1);
        transform.localScale = escala;
    }

    private void OnDrawGizmosSelected()
    {
        if (pointAttack != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pointAttack.position, radiusAttack);
        }
    }

    public void AllowDamage()
    {
        _possibleDamage = true;
    }

    public void ActiveDamage()
    {
        if (_possibleDamage && currentState is AttackState)
        {
            PerformAttack();
            _possibleDamage = false;
        }

    }

    public void EnableDamage()
    {
        BossManager.Instance?.EnableDamage();
    }

    public void DisableDamage()
    {
        BossManager.Instance?.DisableDamage();
    }

}

