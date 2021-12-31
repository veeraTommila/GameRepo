using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Aseta pelinappula kameran kohteeksi.
    public Transform target;

    public Vector3 offset;  //Kuinka kaukaa kamera seuraa pelinappulaa.

    public bool useOffsetValues;

    public float rotateSpeed;

    public float maxKatseluKulma;
    public float minKatseluKulma;

    public Transform pivot;

    public bool invertY;

    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;  //Kuinka kaukaa kamera seuraa pelinappulaa.                     
        }
        //Pivot seuraa kohdetta (pelinappulaa), kun peli on käynnissä.
        pivot.transform.position = target.transform.position;
        //pivot.transform.parent = target.transform;
        pivot.transform.parent = null;

        //Piilotetaan hiiren osoitin, kun peli on käynnissä.
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pivot.transform.position = target.transform.position;   //Pivot-piste liikkuu pelinappulan kanssa.

        //Pelinappula ei liiku, kun kameraa liikutetaan. Silloin target.Rotate(0, horizontal, 0); onkin pivot.Rotate(0, horizontal, 0);


        //Hae hiiren X-sijainti ja käännä kohdetta.
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        pivot.Rotate(0, horizontal, 0);

        //Hae hiiren Y-sijainti ja käännä pivot-pistettä.
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        //pivot.Rotate(-vertical,0 ,0 );
        if (invertY)
        {
            pivot.Rotate(vertical, 0, 0);
        } else
        {
            pivot.Rotate(-vertical, 0, 0);
        }

        /*
         * Kameran ylös- ja alas kääntymisen rajojen määrittäminen.
         * Eulerin kulmat ovat kolme kulmaa, jotka Leonhard Euler esitteli kuvaamaan jäykän rungon suuntaa kiinteään koordinaatistoon nähden.
         * Nyt kamera ei nytkähtele yllättävästi, kun katsomme pelinappulaa suoraan ylhäältä ja maantasosta.
         */
        if (pivot.rotation.eulerAngles.x > maxKatseluKulma && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxKatseluKulma, 0, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minKatseluKulma)
        {
            pivot.rotation = Quaternion.Euler(360f + minKatseluKulma, 0, 0);
        }

        //Move the camera based on the current rotation of the target & the original offset.
        float desiredYAngle = pivot.eulerAngles.y; //Ennen oli target.eulerAngles.y
        float desiredXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        //transform.position = target.position - offset;  //Kuinka kaukaa kamera seuraa pelinappulaa.

        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(target.position.x, target.position.y -.5f, transform.position.z);
        }

        transform.LookAt(target.transform); //Aseta pelinappula kameran kohteeksi.
    }
}
