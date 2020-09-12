using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Temporary
{
    public class SpreadOnGO : MonoBehaviour
    {
        [FormerlySerializedAs("_spShader")] public SpreadShader spShader;

        private Material _mat;
        private static readonly int Vector12312A29B = Shader.PropertyToID("SpreadScale");
        private static readonly int TheLocation = Shader.PropertyToID("TheLocation");
        private bool _isMatNotNull;
        
        private void Start()
        {
            _isMatNotNull = _mat != null;
        }

        private void Awake()
        {
            _mat = GetComponent<Renderer>().material;
        }

        private void Update()
        {
            if (!_isMatNotNull) return;
            _mat.SetVector(TheLocation, spShader.transform.position 
                                        + new Vector3(
                                            _mat.GetFloat(Vector12312A29B) / 2,
                                            0f,
                                            _mat.GetFloat(Vector12312A29B) /  2
                                        ));

            _mat.SetFloat(Vector12312A29B, spShader.scalable);
        }
    }
}
