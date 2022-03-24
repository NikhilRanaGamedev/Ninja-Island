using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] Pause pause;

    Player player;
    Animator anim;

    PlayerStates state = PlayerStates.IDLE;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (state == PlayerStates.IDLE)
        {
            if (player.GetOnGround())
            {
                anim.Play("Idle");
            }
            else
            {
                anim.Play("Falling");
            }

            if (player.Attack())
            {
                state = PlayerStates.ATTACK;
            }
        }
        else if (state == PlayerStates.ATTACK)
        {
            anim.Play("Attack");
        }
        else if (state == PlayerStates.TELEPORT)
        {
            anim.Play("Teleport");
            state = PlayerStates.PLAY_REMAINING_ANIMATION;
            StartCoroutine(ChageState(3));
        }
        else if (state == PlayerStates.HURT)
        {
            if (player.GetLives() > 1)
            {
                anim.Play("Hurt");
                player.Hurt();
                state = PlayerStates.PLAY_REMAINING_ANIMATION;
                StartCoroutine(ChageState(20));
            }
            else
            {
                player.Hurt();
                state = PlayerStates.DEATH;
            }
        }
        else if (state == PlayerStates.DEATH)
        {
            anim.Play("Death");
            player.DeathSound();

            StartCoroutine(ExitGame());
        }
        else if (state == PlayerStates.PLAY_REMAINING_ANIMATION)
        {
            
        }
    }

    IEnumerator ChageState(int time)
    {
        yield return new WaitForSeconds(time / 60f);

        state = PlayerStates.IDLE;
    }

    public void SetState(PlayerStates state)
    {
        this.state = state;
    }

    IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(3f);

        pause.ExitToMainMenu();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Arrow") || collision.CompareTag("Boulder")) && player.GetLives() > 0)
        {
            state = PlayerStates.HURT;
        }
    }
}

public enum PlayerStates
{
    IDLE,
    ATTACK,
    TELEPORT,
    HURT,
    DEATH,
    PLAY_REMAINING_ANIMATION
}
