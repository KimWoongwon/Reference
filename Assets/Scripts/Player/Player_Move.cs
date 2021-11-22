using CUSTOM_ReadOnly;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    // Start is called before the first frame update

    [ReadOnly] public Vector2 MoveVector = Vector2.zero;
    public float MoveSpeed;
    Rigidbody2D rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetMoveVector();
    }

    private void FixedUpdate()
    {
        rbody.velocity = MoveVector.normalized * MoveSpeed;
    }

    void SetMoveVector()
    {
        MoveVector = Vector2Int.zero;

        if (Input.GetKey(KeyCode.W))
            MoveVector.y = 1;
        else if (Input.GetKey(KeyCode.S))
            MoveVector.y = -1;

        if (Input.GetKey(KeyCode.D))
            MoveVector.x = 1;
        else if (Input.GetKey(KeyCode.A))
            MoveVector.x = -1;
    }


}
