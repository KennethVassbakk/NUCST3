
using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering.ShaderGraph;
using ShaderInput = UnityEditor.ShaderGraph.Internal.ShaderInput;


namespace Temporary
{
    //[ExecuteAlways]
    public class SpreadShader : MonoBehaviour
    {
        [SerializeField] public Shader shader;
        [SerializeField] public float scalable;

        private void Start()
        {
            foreach (var rend in FindObjectsOfType<Renderer>())
            {
                if (rend.material.shader == shader)
                {
                    var spreadOnGO = rend.gameObject.AddComponent<SpreadOnGO>();
                    spreadOnGO.spShader = this;
                }
            }
        }

        private void FixedUpdate()
        {
            scalable += Time.deltaTime;
        }
    }
    
}
