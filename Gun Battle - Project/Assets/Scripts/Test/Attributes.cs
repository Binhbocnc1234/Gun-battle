using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[SelectionBase]
[ExecuteInEditMode]
public class Attributes : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    [Range(0,1)]
    [TextArea(minLines : 2, maxLines: 4)]
    [Tooltip("Health value between 0 and 100.")]
    public int something;
}
