
using UnityEngine;


namespace Temporary
{
    //[ExecuteAlways]
    public class SpreadShader : MonoBehaviour
    {
        [SerializeField] public Material material;
        private static readonly int TheLocation = Shader.PropertyToID("TheLocation");
        private static readonly int Vector12312A29B = Shader.PropertyToID("Vector1_2312A29B");

        private void Start()
        {
            print(material);
        }

        private void FixedUpdate()
        {
            material.SetVector(TheLocation, transform.position 
                                            + new Vector3(
                                                material.GetFloat(Vector12312A29B) / 2,
                                                0f,
                                                material.GetFloat(Vector12312A29B) /  2
                                                ));
            var scalable = material.GetFloat(Vector12312A29B);

            scalable += Time.deltaTime * 20f;
            
            material.SetFloat(Vector12312A29B, scalable);
        }
    }
    
}
