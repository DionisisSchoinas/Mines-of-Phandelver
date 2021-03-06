using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public CharacterController controller;
    public Transform indicatorWheel;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float speed = 6f;
    public float maxRunSpeed = 12f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 2f;

    public float smoothing = 0.1f;
    protected float smoothVelocity;
    public float runspeed = 0f;

    public Vector3 direction;
    protected Vector3 velocity;

    public bool isGrounded;
    public bool canMove;
    public bool casting;

    protected float horizontal;
    protected float vertical;
    protected bool running;
    protected bool jump;

    // Controlled inputs which lock other if one is pressed
    public bool mousedown_1;
    public bool mousedown_2;
    // Raw inputs
    public bool mouse_1;
    public bool mouse_2;
    // Raw input - hold
    public bool mousePressed_1;
    // ---------------
    public bool lockMouseInputs;

    public bool playerLocked;

    private SkinnedMeshRenderer[] skinnedMeshRenderers;
    private MeshRenderer[] meshRenderers;
    private ParticleSystem[] particleSystems;

    public void Start()
    {
        canMove = true;
        casting = false;
        mousedown_1 = false;
        mousedown_2 = false;
        mouse_1 = false;
        mouse_2 = false;
        mousePressed_1 = false;
        lockMouseInputs = false;
        playerLocked = false;

        horizontal = 0f;
        vertical = 0f;
        running = false;
        jump = false;

        skinnedMeshRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();

        UIEventSystem.current.onSkillListUp += PlayerLock;
        UIEventSystem.current.onPlayerDied += LockPlayer;
        UIEventSystem.current.onGameEnded += LockPlayer;
    }

    private void OnDestroy()
    {
        UIEventSystem.current.onSkillListUp -= PlayerLock;
        UIEventSystem.current.onPlayerDied -= LockPlayer;
        UIEventSystem.current.onGameEnded -= LockPlayer;
    }

    private void LockPlayer()
    {
        PlayerLock(true);
    }

    public void HidePlayer(bool hide)
    {
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
            skinnedMeshRenderer.enabled = !hide;

        foreach (MeshRenderer meshRenderer in meshRenderers)
            meshRenderer.enabled = !hide;

        particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            if (particleSystem.gameObject.GetComponentInParent<DodgeSkill>() != null)
                continue;

            if (hide)
            {
                particleSystem.enableEmission = false;
                particleSystem.Clear(true);
            }
            else
            {
                particleSystem.enableEmission = true;
                particleSystem.Play(true);
            }

        }
    }

    public void PlayerLock(bool stop)
    {
        canMove = !stop;
        lockMouseInputs = stop;
        playerLocked = stop;
        UIEventSystem.current.PlayerLock(stop);
    }

    public void Update()
    {
        //get horizontal and vertical axes
        if (canMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
        else
        {
            horizontal = 0f;
            vertical = 0f;
        }

        // Controlled inputs which lock other if one is pressed
        if (Input.GetMouseButtonDown(0) && !mousedown_2 && !lockMouseInputs)
        {
            mousedown_1 = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mousedown_1 = false;
        }
        if (Input.GetMouseButtonDown(1) && !mousedown_1 && !lockMouseInputs)
        {
            mousedown_2 = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            mousedown_2 = false;
        }

        // Raw inputs
        if (Input.GetMouseButtonDown(0) && !lockMouseInputs)
        {
            mouse_1 = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouse_1 = false;
        }
        if (Input.GetMouseButtonDown(1) && !lockMouseInputs)
        {
            mouse_2 = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            mouse_2 = false;
        }
        if (Input.GetMouseButton(0) && !lockMouseInputs)
        {
            mousePressed_1 = true;
        }
        else
        {
            mousePressed_1 = false;
        }

        running = Input.GetKey(KeyCode.LeftShift);
        jump = Input.GetKey(KeyCode.L);
    }

    public IEnumerator Stun(float second)
    {
        canMove = false;
        yield return new WaitForSeconds(second);

        if (!playerLocked)
            canMove = true;
    }
}
