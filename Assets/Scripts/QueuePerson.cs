using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueuePerson : MonoBehaviour
{
    private Material _material;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    public Material material {
        get
        {
            if (_skinnedMeshRenderer != null)
            {
                return _skinnedMeshRenderer.material;
            }
            return null;
        }

            set { 
            SetMaterial(value);        
        } }

    // Start is called before the first frame update

    private void Awake()
    {
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SetMaterial( Material material)
    {
        // If no renderer, try to get it
        if (_skinnedMeshRenderer == null)
        {
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }

        // If we have a renderer (either this time or from before)
        if (_skinnedMeshRenderer != null)
        {
            // Set the material
            _skinnedMeshRenderer.material = material;
        }
    }
}
