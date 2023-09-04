using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementStatusNetworkLateSync : NetworkBehaviour
{
    struct transform_network : INetworkSerializeByMemcpy
    {
        public Vector3 position;
        public Vector3 scale;
        public Quaternion rotation;
    }

    [SerializeField] private Transform Camera;

    public float AbsoluteFaceAngle = 0f;
    public float RotateSpeed = 2000f;
    public float TargetFaceAngle = 0f;
    public bool AbleToMove = false;
    private Vector2 lastInputFaceValue = Vector2.zero;
    private Animator _animator;

    private Rigidbody _rigidbody;
    public Vector3 CurrentSpeed;
    public Vector2 TargetSpeed = Vector2.zero;
    public float AccelerateRate = 100f;
    public float MaxSpeed = 20f;
    private Vector2 AccVec = Vector2.zero;
    public Vector3 NetworkFixSpeed = Vector3.zero;
    private float NetworkFixAccelerateRate = 1f;

    private bool focusMode = false;

    private bool DefendMode = false;

    // Start is called before the first frame update
    void Start()
    {
        //Camera = GameObject.FindWithTag("MainCamera").transform;
        Camera = GetComponentInParent<NetworkCameraManager>()._camera.transform;
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        CurrentSpeed = Vector3.zero;

        var a = IsServer || (!IsOwner || !IsClient);
        GetComponentInChildren<NetworkTransform>().enabled = a;
        GetComponentInChildren<NetworkAnimator>().enabled = a;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (IsOwner && IsClient)
        {
            UpdateTargetSpeed(Camera.eulerAngles.y);
            if (!IsServer)
                SyncTransformServerRpc(transform.position, transform.rotation, CurrentSpeed);
        }
        else
        {
            //SyncTransformClientRpc(transform.position , transform.rotation);
            //SyncOtherTransformServerRpc()
        }

    }


    [ServerRpc]
    void SyncTransformServerRpc(Vector3 pos, Quaternion rota, Vector3 _currentSpeed)
    {
        //Debug.Log("transform sync server 1" + transform.position + transform.rotation);

        //transform.position = pos;

        CurrentSpeed = _currentSpeed;
        transform.rotation = rota;
        if ((pos - transform.position).magnitude > 0.05f)
        {
            NetworkFixSpeed = (pos - transform.position).normalized * NetworkFixAccelerateRate;
        }
        else
        {
            NetworkFixSpeed = Vector3.zero;
            CurrentSpeed = Vector3.zero;
        }

    }


    /*
    [ServerRpc]
    void SyncOtherTransformServerRpc(transform_network _transform)
    {
        
    }*/
    [ClientRpc]
    void SyncTransformClientRpc(Vector3 pos, Quaternion rota)
    {
        Debug.Log("transform sync client 1" + transform.position + transform.rotation);
        transform.position = pos;
        transform.rotation = rota;
    }

    //[ServerRpc]
    void UpdateTargetSpeed(float _canmera_transform_rotation_eulerAngles_y)
    {

        AbsoluteFaceAngle = FixAngle(transform.rotation.eulerAngles.y);
        //当输入不为0时更新
        if (lastInputFaceValue != Vector2.zero || focusMode)
            //TargetFaceAngle = FixAngle(Camera.transform.rotation.eulerAngles.y - Vector2.SignedAngle(Vector2.up, lastInputFaceValue));
            TargetFaceAngle = FixAngle(_canmera_transform_rotation_eulerAngles_y -
                                       Vector2.SignedAngle(Vector2.up, lastInputFaceValue));

        //TargetSpeed = RotationMatrix(lastInputFaceValue.normalized * MaxSpeed, Camera.transform.rotation.eulerAngles.y);
        TargetSpeed = (lastInputFaceValue == Vector2.zero || DefendMode)
            ? Vector2.zero
            : RotationMatrix(Vector2.up * MaxSpeed, TargetFaceAngle);
        //目标速度和当前速度差
        var deltaSpeed = new Vector2(TargetSpeed.x - CurrentSpeed.x, TargetSpeed.y - CurrentSpeed.z);

        //变速      
        if (deltaSpeed.sqrMagnitude > 2 * deltaSpeed.normalized.sqrMagnitude * AccelerateRate * Time.deltaTime)
        {
            Vector2 a = deltaSpeed.normalized * AccelerateRate * Time.deltaTime;
            //_rigidbody.velocity += new Vector3(a.x, 0, a.y);
            CurrentSpeed += new Vector3(a.x, 0, a.y);
            _animator.SetFloat("CurrentSpeed", CurrentSpeed.magnitude);
        }
        else
        {
            CurrentSpeed = new Vector3(TargetSpeed.x, CurrentSpeed.y, TargetSpeed.y);
            _animator.SetFloat("CurrentSpeed", CurrentSpeed.magnitude);
        }

        //转向
        if (Mathf.Abs(AbsoluteFaceAngle - TargetFaceAngle) >
            2 * RotateSpeed * Time.deltaTime /*4 * RotateSpeed * Time.deltaTime*/)
        {
            var t = TargetFaceAngle - AbsoluteFaceAngle;
            if (t >= 180 || (t > -180 && t <= 0))
            {
                AbsoluteFaceAngle = FixAngle(AbsoluteFaceAngle - RotateSpeed * Time.deltaTime);
            }
            else
            {
                AbsoluteFaceAngle = FixAngle(AbsoluteFaceAngle + RotateSpeed * Time.deltaTime);
            }

        }
        else
        {
            AbsoluteFaceAngle = TargetFaceAngle;
        }

        //以后加focusMode
        /*if (lastInputFaceValue != Vector2.zero || focusMode)*/
        transform.rotation = Quaternion.Euler(0, AbsoluteFaceAngle, 0);

    }

    Vector2 RotationMatrix(Vector2 v, float angle)
    {
        var x = v.x;
        var y = v.y;
        var sin = System.Math.Sin(System.Math.PI * angle / 180);
        var cos = System.Math.Cos(System.Math.PI * angle / 180);
        var newX = x * cos + y * sin;
        var newY = x * -sin + y * cos;
        return new Vector2((float)newX, (float)newY);
    }

    public void OnMove(InputValue value)
    {
        if (IsOwner && IsClient)
        {
            lastInputFaceValue = value.Get<Vector2>();
            //SetLastInputFaceValueServerRpc(value.Get<Vector2>());
        }

    }

    [ServerRpc]
    public void SetLastInputFaceValueServerRpc(Vector2 _lastInputFaceValue)
    {
        lastInputFaceValue = _lastInputFaceValue;
    }

    float FixAngle(float angle)
    {
        while (true)
        {
            if (angle < -180)
            {
                angle += 360;
            }
            else if (angle >= 180)
            {
                angle -= 360;
            }
            else
            {
                return angle;
            }
        }
    }

    public void FixedUpdate()
    {
        transform.position += (CurrentSpeed + NetworkFixSpeed) * Time.fixedDeltaTime;

    }

    public void Update()
    {


        /*if (Mathf.Abs(FaceAngle - TargetFaceAngle) <  2 * RotateSpeed * Time.deltaTime)
        {
            FaceAngle = TargetFaceAngle;
        }
        else
        {
            var t = TargetFaceAngle - FaceAngle;
            if (t >= 180 || (t >= -180 && t < 0))
            {
                FaceAngle -= RotateSpeed * Time.deltaTime;
                if (FaceAngle <= -180)
                {
                    FaceAngle *= -1;
                }
            }
            else
            {
                FaceAngle += RotateSpeed * Time.deltaTime;
                if (FaceAngle >= 180)
                {
                    FaceAngle *= -1;
                }
            }
        }*/

        /*else
        {
            AbsoluteFaceAngle = TargetFaceAngle;
        }*/


    }

    public void OnDefend(InputValue value)
    {
        var a = value.Get<float>();
        DefendMode = a > 0;
        // Debug.Log(a);
        if (IsOwner && IsClient && !IsServer)
        {
            _animator.SetBool("isDefend",DefendMode);
        }


    }

    [ServerRpc]
    void DefendServerRpc()
    {
        _animator.SetBool("isDefend",DefendMode);
    }

    public void OnAttack(InputValue value)
    {
        _animator.SetTrigger("Attack");
    }
}
