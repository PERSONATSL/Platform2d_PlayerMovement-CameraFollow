using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

   
    public float moveSpeed;
    public float jumpPower;
    private Animator anim;
    private Rigidbody2D rigidbody2D;
    public AudioClip[] audioFootStep;
    public AudioClip audioJump;
    public AudioClip audioFall;
    public AudioClip audioNext;
    private AudioSource audioSource;
    private int jumpCount;
    private bool isOnGround = true;
    public Transform checkPoint;
    public float checkRedius;
    public LayerMask maskOfGround;
    public PlayerCamera pc;
    private bool allreadyPlay;
    public GameObject restartPanel;
    private bool unableToContrl;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(!unableToContrl)
            playerMovement();
    }
    private void playerMovement()
    {
        playerJump();

        if (Input.GetKeyDown(KeyCode.D))
            transform.localScale = new Vector3(22f, 22f);
        else if (Input.GetKeyDown(KeyCode.A))
            transform.localScale = new Vector3(-22f, 22f);

        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(moveSpeed*h*Time.deltaTime,0,0));

        anim.SetBool("Run", Input.GetAxisRaw("Horizontal") != 0);
    }
    private void playerJump()
    {
        isOnGround = Physics2D.OverlapCircle(checkPoint.position, checkRedius, maskOfGround);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount < 1)
            {
                rigidbody2D.AddForce(new Vector2(0, jumpPower * 100));

                anim.SetBool("Jump", true);

                jumpCount++;
            }

        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("Jump", false);
        }
        if (isOnGround)
            jumpCount = 0;

        checkDeath();
    }

    private void playFootStepSound()
    {
        audioSource.PlayOneShot(audioFootStep[Random.Range(0, audioFootStep.Length)]);
    }
    private void playJumpSound()
    {
        audioSource.PlayOneShot(audioJump);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkPoint.position, checkRedius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Damage") && !allreadyPlay)
        {
            pc.death = true;
            audioSource.PlayOneShot(audioFall);
            allreadyPlay = true;
            popDownTheMenu();
            unableToContrl = true;

            anim.SetBool("Run", false);
            anim.SetBool("Jump", false);
        }
        else if (other.CompareTag("Door1") && !allreadyPlay)
        {
            audioSource.PlayOneShot(audioNext);
            Invoke("LoadLevel2", 1f);
        }
        else if (other.CompareTag("Door2") && !allreadyPlay)
        {
            audioSource.PlayOneShot(audioNext);
            Invoke("LoadLevel3", 1f);
        }
        else if (other.CompareTag("Door3") && !allreadyPlay)
        {
            audioSource.PlayOneShot(audioNext);
            Invoke("LoadLevel4", 1f);
        }
        else if (other.CompareTag("Door4") && !allreadyPlay)
        {
            audioSource.PlayOneShot(audioNext);
            Invoke("LoadLevel0", 1f);
        }

    }

    private void checkDeath()
    {
        if (transform.position.y < -17f && !allreadyPlay)
        {
            pc.death = true;
            audioSource.PlayOneShot(audioFall);
            allreadyPlay = true;
            popDownTheMenu();
            unableToContrl = true;
            anim.SetBool("Run", false);
            anim.SetBool("Jump", false);
        }
    }
    private void LoadLevel2()
    {
        SceneManager.LoadScene(2);
    }
    private void LoadLevel3()
    {
        SceneManager.LoadScene(3);
    }
    private void LoadLevel4()
    {
        SceneManager.LoadScene(4);
    }
    private void LoadLevel0()
    {
        SceneManager.LoadScene(0);
    }

    private void popDownTheMenu()
    {
        restartPanel.transform.DOLocalMoveY(0, 0.5f);
    }
    public void ReLoadLevel(int index)
    {
        try
        {
            SceneManager.LoadScene(index);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }

    }
}
