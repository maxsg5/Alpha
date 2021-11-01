using UnityEngine;

namespace Utility.SensorSystem
{
    /// <summary>
    /// Base class that all sensors are derived from
    /// </summary>
    ///
    /// Author:     Brian Brookwell (BRB)
    /// 
    /// Constant			Description
    /// EPSILON				Floating point comparison fuzz value
    ///
    /// Editor Field        Description
    /// layerMask           LayerMask for excluded object layers
    /// radius              Radius of the cone sensor
    /// losCheck            Final check visibility using RayCasting
    /// showSensorRegion    Generate and display the sensor region
    /// 
    /// Field				Description
    /// meshFilter          MeshFilter associated with the "eye" object
    /// r2                  Radius of filter squared
    /// 
    [System.Serializable]
    public abstract class _SNSSensor : MonoBehaviour
    {
        [System.NonSerialized] protected MeshFilter meshFilter;
        [System.NonSerialized] protected float r2;

        protected const float EPSILON = 1e-4f;
        
        public LayerMask layerMask;
        public float radius;
        public bool losCheck = false;
        public bool showSensorRegion = true;

        /// <summary>
        /// Awake initializes this Sensor when the sensor is created in the level
        /// </summary>
        /// 
        /// 2017-10-12	BRB		Initial Testing
        /// 
        void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();

            r2 = radius * radius + radius * EPSILON;

            HandleSensorRegion();

            Init();
        }

        /// <summary>
        /// Handles all changes to the Sensor Region size due to changes in the
        /// radius
        /// </summary>
        /// 
        /// 2017-10-12	BRB		Initial Testing
        ///
        protected void HandleSensorRegion()
        {
            transform.localScale = Scale();     // Apply radius as scaling
        }

        /// <summary>
        /// Changes the 3D scale of the sensor's visibility object.  The default is uniform scaling
        /// by a fixed amount in X, Y and Z
        /// </summary>
        /// 
        /// Date		Author	Description
        /// 2017-10-12	BRB		Initial Testing

        virtual protected Vector3 Scale()
        {
            return new Vector3(radius, radius, radius);
        }

        /// <summary>
        /// Changes the visibility radius and computes the radius squared
        /// </summary>
        ///
        /// <param name="r">Visibility radius of the sensor</param>
        /// 
        /// 2017-10-08	BRB		Initial Testing
        ///
        public void SetRadius(float r)
        {
            radius = r;
            r2 = r * r + r * EPSILON;
        }

        /// <summary>
        /// Generates the sensor region object dynamically by creaating the mesh
        /// </summary>
        /// 
        /// 2017-10-08	BRB		Initial Testing
        ///
        public void SetRegion()
        {
            meshFilter.sharedMesh = CreateMesh();
            meshFilter.GetComponent<MeshRenderer>().enabled = showSensorRegion;
        }

        /// <summary>
        /// Uses a raycast to check whether the target is in LOS.  Replace this to change the
        /// method used when doing the final RayCast check if losCheck is true
        /// </summary>
        /// <returns><c>true</c> if target was in LOS, <c>false</c> otherwise.</returns>
        /// <param name="target">Target being checked for LOS</param>
        /// <param name="heading">Heading of ray to use to check LOS</param>
        /// <param name="source">Source position of the ray</param>
        /// 
        /// 2017-10-08	BRB		Initial Testing
        ///
        protected bool RayCheck(Transform target, Vector3 heading, Vector3 source)
        {
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(source, heading.normalized, out hit, radius, ~layerMask))
                return hit.collider.transform == target;
            return false;
        }

        // Abstract Methods implemented by a sub-class sensor

        public abstract bool CanSee(Transform target);
        public abstract bool CanSee(GameObject target);

        public abstract Mesh CreateMesh();

        protected abstract void Init();
    }
}