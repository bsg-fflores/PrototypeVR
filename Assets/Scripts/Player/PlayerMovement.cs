using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviourPun
    {
        [SerializeField] private float _speed = 5f;
        private Vector3 _movement;
        private Rigidbody _rb;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        
        private void Update()
        {
            if (!photonView.IsMine) return;
            
            _movement.x = Input.GetAxis("Horizontal");
            _movement.z = Input.GetAxis("Vertical");
            
            _movement = _movement.normalized;
        }
        
        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _movement * (_speed * Time.fixedDeltaTime));
        }
    }
}