using UnityEngine;

namespace Utility.SensorSystem
{
    /// <summary>
    /// Implements a concrete sensor for a complete sphere of fixed radius
    /// </summary>
    ///
    /// Author:     Brian Brookwell (BRB)
    ///
    public class SNSSphere : _SNSSensor
    {
        /// <summary>
        /// Implementation of the CanSee method using a Transform as a target
        /// </summary>
        /// <param name="target">Transform of target GameObject</param>
        /// <returns>True if target is visible and False otherwise</returns>
        /// 
        /// 2017-10-14	BRB		Initial Testing
        /// 
        public override bool CanSee(Transform target)
        {
            Vector3 heading = target.position - gameObject.transform.position;

            if (heading.sqrMagnitude <= r2)
            {
                if (losCheck)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, heading, out hit, radius))
                    {
                        return hit.transform == target;
                    }
                }
                else
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Implementation of the CanSee method using a GameObject as a target
        /// </summary>
        /// <param name="target">GameObject being tested</param>
        /// <returns>True if the GameObject can be seen and False otherwise</returns>
        /// 
        /// 2017-10-14	BRB		Initial Testing
        ///
        public override bool CanSee(GameObject target)
        {
            return CanSee(target.transform);
        }

        /// <summary>
        /// Creates the region mesh for a spherical sensor
        /// </summary>
        /// <returns>Mesh associated with the sphere sensor</returns>
        /// 
        /// 2017-10-14	BRB		Initial Testing
        ///
        public override Mesh CreateMesh()
        {
            GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            Mesh mesh = temp.GetComponent<MeshFilter>().mesh;

            Destroy(temp);

            mesh.name = "Sphere (" + radius + ")";

            return mesh;
        }

        /// <summary>
        /// Initializes the SPhere Sensor by setting the radius and creating a region mesh
        /// </summary>
        /// 
        /// 2017-10-14	BRB		Initial Testing
        ///
        protected override void Init()
        {
            SetRadius(radius);

            SetRegion();
        }
    }
}
