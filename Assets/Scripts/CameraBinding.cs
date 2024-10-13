using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBinding : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        // Для проверки, находимся ли мы внутри границы по оси X
        float deltaX = lookAt.position.x - transform.position.x;

        if(deltaX > boundX || deltaX < -boundX)
        {
            if(transform.position.x < lookAt.position.x) // Если игрок справа, а фокус камеры слева
            {
                delta.x = deltaX - boundX;
            }
            else // Если игрок слева, а фокус камеры справа
            {
                delta.x = deltaX + boundX;
            }
        }

        // Для проверки, находимся ли мы внутри границы по оси Y
        float deltaY = lookAt.position.y - transform.position.y;

        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y) // Если игрок справа, а фокус камеры слева
            {
                delta.y = deltaY - boundY;
            }
            else // Если игрок слева, а фокус камеры справа
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
