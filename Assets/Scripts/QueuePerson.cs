using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QueuePerson : MonoBehaviour
{
    private float yOffsetBasedOnSizeTall = 0.5f;
    private float yOffsetBasedOnSizeNorm = 0.5f;
    private float yOffsetBasedOnSizeSmall = 0.5f;
    private float yScaleBasedOnSizeSmall = 1.0f;
    private float yScaleBasedOnSizeNorm = 1.15f;
    private float yScaleBasedOnSizeTall = 1.3f;


    public string UID;
    public bool isPlayer;
    public HeightType height;
    public ShirtType shirt;
    public PantsType pants;
    public float yOffset = 0.0f;
    public GameObject arrow;
    // TODO: Variations? 

    private SkinnedMeshRenderer _skinnedMeshRenderer;

    public Vector3 currentTargetPos;
    public float moveSpeed;
    public bool move;

    // Start is called before the first frame update

    private void Awake()
    {
        // Get the renderer
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Start()
    {
        // Give them a random ID
        UID = System.Guid.NewGuid().ToString();
    }

    public void LoadFromScriptableObject ( QueuePersonData data )
    {        

        // Set the objects isPlayer to what was passed in
        isPlayer = data.isPlayer;
        if(isPlayer == true)
		{
            Debug.Log("THIS IS A PLAYER");
            arrow.SetActive(true);
		}

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
                transform.localScale = new Vector3(1.15f, yScaleBasedOnSizeTall, 1.15f);
                yOffset = yOffsetBasedOnSizeTall;
                break;
            case HeightType.NORM:
                transform.localScale = new Vector3(1.15f, yScaleBasedOnSizeNorm, 1.15f);
                yOffset = yOffsetBasedOnSizeNorm;
                break;
            case HeightType.SMALL:
                transform.localScale = new Vector3(1.15f, yScaleBasedOnSizeSmall, 1.15f);
                yOffset = yOffsetBasedOnSizeSmall;
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
        if(move == true)
		{
            if (this.transform.position != currentTargetPos)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, currentTargetPos, moveSpeed * Time.deltaTime);
			}
			else
			{
                move = false;
			}
		}
    }
       
}
