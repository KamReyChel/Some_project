using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderModification : MonoBehaviour
{

    [SerializeField] private MeshRenderer meshRenderer;
    
    [SerializeField] private float horizontalOffset;
    [SerializeField] private Color newColor;
    //[SerializeField] private Texture2D newTexture;
    
    private static readonly int Color = Shader.PropertyToID("_Color");
    private static readonly int HorizontalOffset = Shader.PropertyToID("_HorizontalOffset");
    //private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    
    // Start is called before the first frame update
    void Start()
    {
        //meshRenderer.material.SetTexture(MainTex, newTexture);
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.SetFloat(HorizontalOffset, horizontalOffset);
        meshRenderer.material.SetColor(Color, newColor);
        
        
    }
}
