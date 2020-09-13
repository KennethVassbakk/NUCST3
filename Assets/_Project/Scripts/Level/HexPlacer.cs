// By John Hauge.
using UnityEngine;

namespace Level
{
    public class HexPlacer : MonoBehaviour
    {
        // Temporary Hexagon to instantiate
        //To be replaced.
        [SerializeField] public GameObject hexagon;

        // Circles around the middle
        [SerializeField] public int divisions;
        
        // Hexagons from one side to the other.
        [SerializeField] private float levelRange;

        private void Awake()
        {
            //The lane length of hexagons on a vertical and horizontal line.
            levelRange = divisions * 2 + 1;
            
            //Spawns in the hexagon tiles.
            PlaceHexagon();
        }

        private void PlaceHexagon()
        {
            
            var transform1 = transform;
            var localScale = transform1.localScale;
            
            //var position = transform1.position; <-- implement if it should spawn away from 0,0,0
            
            //Calculating corner neighbor
            var xStart = localScale.x / 2f + localScale.x / 4f;
            var zStart = localScale.z / 2;

            // Variable to adjust per line of hexagons
            var toSpawn = divisions + 1;
            
            var startSpawn = new Vector3(xStart * -1f, 0f, zStart * -1f) * (divisions);
            print( "Start Spawn Pos: " + startSpawn);
            
            // for each lane in the hexagon
            for (var i = 0; i < levelRange; i++)
            {
                var currentSpawnPos = startSpawn;
                // for each hexagon in lane.
                for (var j = 0; j < toSpawn; j++)
                {
                    Instantiate(hexagon,currentSpawnPos, transform1.rotation, gameObject.transform);
                    currentSpawnPos.z += localScale.z;
                }
                // Before middle
                if (i < Mathf.FloorToInt(levelRange / 2))
                {
                    toSpawn++;
                    startSpawn += new Vector3(xStart, 0f, zStart * -1);
                }
                else // after middle
                {
                    toSpawn--;
                    startSpawn += new Vector3(xStart, 0f, zStart);
                }
            }
        }
    }
}
