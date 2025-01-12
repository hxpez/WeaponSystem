using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastExplanation : MonoBehaviour
{
    /* if they want, quick introduction into Raycasts (what is it, how can you use it)
     * showing BoxCast, SphereCast
     * 
     */

    float m_MaxDistance;
    float m_Speed;
    bool m_HitDetect;
    bool m_HitDetect0;

    float radius = 1;

    Collider m_Collider;
    RaycastHit m_Hit;
    RaycastHit m_Hit0;

    void Start()
    {
        //Choose the distance the Box can reach to
        m_MaxDistance = 300.0f;
        m_Speed = 20.0f;
        m_Collider = GetComponent<Collider>();
    }

    void Update()
    {
        //Simple movement in x and z axes
        //float xAxis = Input.GetAxis("Horizontal") * m_Speed;
        //float zAxis = Input.GetAxis("Vertical") * m_Speed;
        //transform.Translate(new Vector3(xAxis, 0, zAxis));
    }

    void FixedUpdate()
    {
        //Test to see if there is a hit using a BoxCast
        //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
        //Also fetch the hit data
        m_HitDetect = Physics.BoxCast(transform.position, transform.localScale * 0.5f, transform.forward, out m_Hit, transform.rotation, m_MaxDistance);
        if (m_HitDetect)
        {
            //Output the name of the Collider your Box hit
            Debug.Log("Hit : " + m_Hit.collider.name);
        }

        m_HitDetect0 = Physics.SphereCast(transform.position, radius, transform.forward, out m_Hit0, m_MaxDistance); // could also define a layerMask, so that only objects on a specific layer are detected
        if (m_HitDetect0)
        {
            //Output the name of the Collider your Box hit
            Debug.Log("Hit : " + m_Hit0.collider.name);
        }

        //Physics.CapsuleCast -> same as the others but as a capsule

        // m_HitDetect0 = Physics.SphereCastAll() / Physics.BoxCastAll() -> returns all the objects it hits in an array
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * m_MaxDistance, transform.localScale);
        }

        Gizmos.color = Color.green;
        if (m_HitDetect0)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * m_Hit0.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireSphere(transform.position + transform.forward * m_Hit.distance, radius);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireSphere(transform.position + transform.forward * m_MaxDistance, radius);
        }
    }

}
