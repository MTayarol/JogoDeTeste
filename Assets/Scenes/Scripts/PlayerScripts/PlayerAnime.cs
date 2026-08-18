using UnityEngine;

public class PlayerAnime : MonoBehaviour
{
    private PlayerMovement player;
    private Animator animator;

    private readonly int isRunningHash = Animator.StringToHash("isRunning");

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {  
        player = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool running = player._moveInput.sqrMagnitude > 0;
        animator.SetBool(isRunningHash, running);

        if (player._moveInput.x > 0){
            transform.eulerAngles = new Vector2(0,0);
        }
        else if (player._moveInput.x < 0){
            transform.eulerAngles = new Vector2(0,180);
        }
    }
}
