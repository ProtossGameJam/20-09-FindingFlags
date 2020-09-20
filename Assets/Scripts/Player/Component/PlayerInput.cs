using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IMoveInput
{
    [SerializeField] private PhotonView pView;

    [ReadOnly] [SerializeField] private GameObject nearNPC; //가까운 NPC

    public Vector2 MoveVector { get; set; }

    private void Awake()
    {
        if (pView == null)
        {
            pView = GetComponent<PhotonView>();
        }

        InputHandler.MovePerformActionRegister(MoveInput);
        InputHandler.MoveCanceledActionRegister((ctx) =>
        {
            MoveVector = Vector2.zero;
        });
    }

    private void MoveInput(InputAction.CallbackContext ctx)
    {
        if (pView.IsMine)
        {
            MoveVector = ctx.ReadValue<Vector2>().normalized;
        }
    }


    private void Update()
    {
        if (Keyboard.current[Key.F].wasPressedThisFrame)
        {
            if(nearNPC != null)
            {
                nearNPC.GetComponent<DialogueManager>().Interact();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "NPC")
        {
            nearNPC = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (nearNPC == collision.gameObject)
        {
            nearNPC = null;
        }
    }
}