using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Engine Refs")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform firstCatPosition;
    [SerializeField] private Rigidbody2D firstCatRB;

    [Header("Const Values")]
    private const float maxSpeed = 3f;
    private const float acceleration = 300f;
    private const float moveThreshold = 0.2f;
    
    [Header("Dynamic Values")]
    private bool clickHeld = false;
    private bool moving = false;
    private Vector2 moveTo = Vector2.zero;
    private Vector2 moveDir = Vector2.zero;

#region standard methods

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputReader.ClickEvent += HandleClickStart;
        inputReader.ClickCanceledEvent += HandleClickStop;
        inputReader.PositionEvent += GetPosition;

        inputReader.EnableGameplay();
    }

    // FixedUpdate is called once every 0.2 seconds
    void FixedUpdate()
    {
        //if click is held, do things related to moving
        if (clickHeld)
        HandleMove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#endregion

#region class methods

    private void HandleMove()
    {
        if (Vector2.Distance(firstCatPosition.position, moveTo) > moveThreshold)
        {
            moving = true;
            Move();
        }
        else
        {
            moving = false;
        }
    }

    private void Move()
    {
        firstCatRB.AddForce(moveDir * acceleration * Time.deltaTime);

        if (firstCatRB.linearVelocity.magnitude > maxSpeed)
        {
            firstCatRB.linearVelocity = firstCatRB.linearVelocity.normalized * maxSpeed;
        }
    }

    private void CalcDir()
    {
        moveDir = (moveTo - (Vector2)firstCatPosition.position).normalized;
    }

#endregion

#region input handlers

    private void HandleClickStart()
    {
        clickHeld = true;
    }

    private void HandleClickStop()
    {
        clickHeld = false;
    }

    private void GetPosition(Vector2 _position)
    {
        moveTo = Camera.main.ScreenToWorldPoint(_position);
        CalcDir();
    }

#endregion
}
