using Photon.Pun;
using UnityEngine;

public class PlayerNetworkMove : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private Rigidbody2D playerRigidbody;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float networkPosFixDistance;

    private IMoveInput moveInput;

    private Vector3 networkPosition;

    private void Awake() {
        if (playerRigidbody == null) playerRigidbody = GetComponent<Rigidbody2D>();

        moveInput = GetComponent<PlayerInput>();
    }

    private void Update() {
        if (photonView.IsMine) return;

        var position = transform.position;
        position = (position - networkPosition).sqrMagnitude >= Mathf.Pow(networkPosFixDistance, 2.0f) ?
                       networkPosition :
                       Vector3.Lerp(position, networkPosition, Time.deltaTime * networkPosFixDistance);
        transform.position = position;
    }

    private void FixedUpdate() {
        if (photonView.IsMine) RigidMove(transform.position, moveInput.MoveVector, moveSpeed * Time.deltaTime);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting)
            stream.SendNext(transform.position);
        else
            networkPosition = (Vector3) stream.ReceiveNext();
    }

    private void RigidMove(Vector2 currentPos, Vector2 vec, float speed) {
        playerRigidbody.MovePosition(currentPos + vec * speed);
    }
}