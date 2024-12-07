using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Engine Refs")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform firstCatPosition;
    [SerializeField] private Rigidbody2D firstCatRB;

    [Header("Const Values")]
    private const float maxSpeed = 3.5f;
    private const float acceleration = 500f;
    private const float moveThreshold = 0.2f;
    
    [Header("Dynamic Values")]
    private bool clickHeld = false;
    private bool moving = false;
    private bool canMove = true;
    private Vector2 screenPosition = Vector2.zero;
    private Vector2 clickedPoint = Vector2.zero;
    private Vector2 moveTo = Vector2.zero;
    private Vector2 moveDir = Vector2.zero;
    private GameObject interactObject;

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

    private void OnTriggerEnter2D(Collider2D _collider)
    {
        Debug.Log("entered a trigger");
        if (_collider == null) return;

        if (_collider.gameObject.CompareTag("House"))
        {
            interactObject = _collider.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D _collider)
    {
        if (_collider == null) return;

        if (_collider.gameObject.CompareTag("House"))
        {
            interactObject = null;
        }
    }

#endregion

#region class methods

    private void Interact()
    {
        if (interactObject == null) return;

        Collider2D[] overlapObjects = Physics2D.OverlapPointAll(clickedPoint);

        if (overlapObjects.Length > 0)
        {
            foreach (Collider2D col in overlapObjects)
            {
                if (col.gameObject == interactObject)
                {
                    interactObject.GetComponent<IInteractable>().Interact();
                }
            }
        }
    }

    private void HandleMove()
    {
        if (canMove && Vector2.Distance(firstCatPosition.position, moveTo) > moveThreshold)
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

    private void ClickedUICheck()
    {
        bool clickedUI;
        RectTransform[] uiObjects = FindObjectsByType<RectTransform>(FindObjectsSortMode.None);

        foreach(RectTransform rect in uiObjects)
        {
            clickedUI = (screenPosition.x > rect.rect.min.x && screenPosition.x < rect.rect.max.x) ||
                        (screenPosition.y > rect.rect.min.y && screenPosition.y < rect.rect.max.y);

            if (clickedUI)
            {
                canMove = false;
                break;
            }
        }
    }

#endregion

#region input handlers

    private void HandleClickStart()
    {
        clickHeld = true;
        canMove = true;
        ClickedUICheck();
    }

    private void HandleClickStop()
    {
        clickHeld = false;
        clickedPoint = moveTo;
        Interact();
    }

    private void GetPosition(Vector2 _position)
    {
        screenPosition = _position;
        moveTo = Camera.main.ScreenToWorldPoint(_position);
        CalcDir();
    }

#endregion
}
