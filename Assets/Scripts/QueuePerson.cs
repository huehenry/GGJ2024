using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueuePerson : MonoBehaviour
{
    private float yOffsetBasedOnSizeTall = 0.25f;
    private float yOffsetBasedOnSizeNorm = 0.5f;
    private float yOffsetBasedOnSizeSmall = 0.85f;
    private float yScaleBasedOnSizeSmall = 0.85f;
    private float yScaleBasedOnSizeNorm = 1.0f;
    private float yScaleBasedOnSizeTall = 1.15f;


    public float UID;
    public bool isPlayer;
    public HeightType height;
    public ShirtType shirt;
    public PantsType pants;
    // TODO: Variations? 

    private SkinnedMeshRenderer _skinnedMeshRenderer;

    // Start is called before the first frame update

    private void Awake()
    {
        // Get the renderer
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

    }

    void Start()
    {
        // Give them a random ID
        UID = Time.time;
    }

    public void LoadFromScriptableObject ( QueuePersonData data, bool isPlayer = false )
    {        

        // Set the objects isPlayer to what was passed in
        this.isPlayer = isPlayer;

        // Set the object data
        height = data.height;
        shirt  = data.shirt;
        pants = data.pants;

        // Set their material
        SetVisuals();
    }

    public void SetVisuals()
    {

        switch (height)
        {
            case HeightType.TALL:
                transform.localScale = new Vector3(1, yScaleBasedOnSizeTall, 1);
                transform.position = new Vector3(transform.position.x, yOffsetBasedOnSizeTall, transform.position.z);
                break;
            case HeightType.NORM:
                transform.localScale = new Vector3(1, yScaleBasedOnSizeNorm, 1);
                transform.position = new Vector3(transform.position.x, yOffsetBasedOnSizeNorm, transform.position.z);
                break;
            case HeightType.SMALL:
                transform.localScale = new Vector3(1, yScaleBasedOnSizeSmall, 1);
                transform.position = new Vector3(transform.position.x, yOffsetBasedOnSizeSmall, transform.position.z);
                break;
        }

        string textureFilename = "";
        switch (shirt)
        {
            case ShirtType.RED:
                textureFilename += "R";
                break;
            case ShirtType.GREEN:
                textureFilename += "G";
                break;
            case ShirtType.BLUE:
                textureFilename += "B";
                break;
        }
        switch (pants)
        {
            case PantsType.JEANS:
                textureFilename += "J";
                break;
            case PantsType.SHORTS:
                textureFilename += "S";
                break;
            case PantsType.LEATHER:
                textureFilename += "L";
                break;
                case PantsType.NONE: 
                textureFilename += "N"; 
                break;
        }

        // TODO: Variations -- for now, everyone is variation 0
        textureFilename += 0;

        Texture2D texture2D = Resources.Load<Texture2D>("Skins/" + textureFilename);
        _skinnedMeshRenderer.material.mainTexture = texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
       
}
