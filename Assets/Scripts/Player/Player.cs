using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerStateMachine stateMachine;
    AudioSource audioSource;

    [SerializeField] Object kunai;
    [SerializeField] Object heartPrefab;
    [SerializeField] List<AudioClip> audioList;

    Stack<Object> hearts = new Stack<Object>();

    bool onGround;
    bool kunaiThrown;
    bool invincible;
    int lives = 3;

    void Start()
    {
        stateMachine = GetComponent<PlayerStateMachine>();
        audioSource = GetComponent<AudioSource>();

        onGround = true;
        kunaiThrown = false;
        invincible = false;

        hearts.Clear();
        
        DrawHearts();
    }

    void Update()
    {
        CheckIfOnGround();
        //Gravity();
    }

    public bool Attack()
    {
        float xPos = Input.GetAxisRaw("Horizontal");
        float yPos = Input.GetAxisRaw("Vertical");

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F)) && !kunaiThrown && !(xPos == 0 && yPos == 0) && (yPos >= 0))
        {
            audioSource.PlayOneShot(audioList[0]);

            kunaiThrown = true;
            invincible = true;

            Vector3 angleKunai = Vector3.zero; // Top Right

            if (xPos == -1 && yPos == 1) // Top Left
                angleKunai = new Vector3(0f, 0, 90f);
            else if (xPos == 0 && yPos == 1) // Up
                angleKunai = new Vector3(0f, 0, 45f);
            else if (xPos == -1 && yPos == 0) // Left
                angleKunai = new Vector3(0f, 0, 135f);
            else if (xPos == 1 && yPos == 0) // Right or Centre
                angleKunai = new Vector3(0f, 0, -45f);

            //else if (xPos == -1 && yPos == -1) // Bottom Left
            //    angleKunai = new Vector3(0f, 0, 180f);
            //else if (xPos == 0 && yPos == -1) // Down
            //    angleKunai = new Vector3(0f, 0, -135f);
            //else if (xPos == -1 && yPos == -1) // Bottom Right
            //    angleKunai = new Vector3(0f, 0, -90f);

            if (xPos == -1)
                transform.localScale = new Vector3(-1, 1, 1);
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            float distance = 0f;

            if (Input.GetKeyDown(KeyCode.Space))
                distance = 8f;
            else if (Input.GetKeyDown(KeyCode.F))
                distance = 4f;

            GameObject kunaiObj = Instantiate(kunai, transform.position, Quaternion.Euler(angleKunai)) as GameObject;
            kunaiObj.GetComponent<Kunai>().SetValues(new Vector3(xPos, yPos, 0), transform.position, distance, this);

            return true;
        }

        return false;
    }

    void Gravity()
    {
        if (onGround == false)
        {
            if (transform.position.y < -4f)
            {
                transform.position = new Vector3(-2.5f, 0.062f, 0f);
                stateMachine.SetState(PlayerStates.HURT);
            }
            else
                transform.position += Vector3.down * 9.8f * Time.deltaTime;
        }
    }

    void CheckIfOnGround()
    {
        Bounds boxBounds = GetComponent<BoxCollider2D>().bounds;
        Vector2 origin = new Vector2(boxBounds.center.x, boxBounds.center.y - boxBounds.extents.y);

        float rayLength = .1f;
        int layerMask = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayLength, layerMask);

        if (hit)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        Debug.DrawRay(origin, Vector2.down * rayLength, Color.red);
    }

    public void Teleport(Vector3 pos)
    {
        stateMachine.SetState(PlayerStates.TELEPORT);
        transform.position = pos;

        StartCoroutine(KunaiTimer());
        StartCoroutine(KunaiInvincible());
    }

    IEnumerator KunaiTimer()
    {
        yield return new WaitForSeconds(6f / 60f);

        kunaiThrown = false;
    }

    IEnumerator KunaiInvincible()
    {
        yield return new WaitForSeconds(3 / 60f);

        invincible = false;
    }

    IEnumerator HurtInvincible()
    {
        yield return new WaitForSeconds(30 / 60f);

        invincible = false;
    }

    public bool GetInvincible()
    {
        return invincible;
    }

    public void Hurt()
    {
        audioSource.PlayOneShot(audioList[1]);

        invincible = true;

        lives--;

        Destroy(hearts.Pop());

        StartCoroutine(HurtInvincible());
    }

    public void DeathSound()
    {
        audioSource.PlayOneShot(audioList[2], 0.2f);
    }

    public void SetOnGround(bool value)
    {
        onGround = value;
    }

    public bool GetOnGround()
    {
        return onGround;
    }

    void DrawHearts()
    {
        for (int i = 0; i < lives; i++)
        {
            Object obj = Instantiate(heartPrefab, new Vector3(-11.5f + (1.25f * i), 6f, 0f), Quaternion.identity);

            hearts.Push(obj);
        }
    }

    public int GetLives()
    {
        return lives;
    }
}
 