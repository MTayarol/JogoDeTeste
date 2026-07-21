using UnityEngine;

public class PlayerAnime : MonoBehaviour
{
    private PlayerMovement player;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {  
        player = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player._moveInput.sqrMagnitude > 0){
            this.animator.SetInteger("transicao", 1);
        }else{
            this.animator.SetInteger("transicao", 0);
        }

        if (player._moveInput.x > 0){
            transform.eulerAngles = new Vector2(0,0);
        }
        else if (player._moveInput.x < 0){
            transform.eulerAngles = new Vector2(0,180);
        }
    }
}
