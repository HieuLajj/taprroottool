using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeVfx : MonoBehaviour
{
    public GameObject vfxGameObject;
    private void Start()
    {
        // Lấy MeshRenderer của khối 3D VFX
        MeshRenderer vfxRenderer = vfxGameObject.GetComponent<MeshRenderer>();

        // Lấy kích thước chính xác của khối 3D VFX
        Vector3 vfxSize = vfxRenderer.bounds.size;

        // In kích thước chính xác của khối 3D VFX
        Debug.Log("Kích thước chính xác của khối 3D VFX: " + vfxSize);
    }
}
