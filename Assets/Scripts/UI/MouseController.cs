using UnityEngine;
using System.Collections.Generic;


// 类负责根据鼠标输入控制旋转。
public class MouseController : MonoBehaviour
{
    [Header("X Axis")]
	public float sensitivityX = 4f; //鼠标外观敏感度

    [Header("Y Axis")]
    public float sensitivityY = 4f; //鼠标外观敏感度

    [Range(-360, 360)]
    public float maximumY = 60f; // 你可以仰视的最大角度。

    [Range(-360, 360)]
    public float minimumY = -60f; //你可以俯视的最小角度.

    [Space()]
    public int frameCounter = 10; // 要平均的帧数，用于平滑鼠标。
    public GameObject controller; //玩家

    //鼠标旋转输入
    private float rotationX = 0f;
    private float rotationY = 0f;

    //用来计算这个物体的旋转
    private Quaternion xQuaternion;
    private Quaternion yQuaternion;
    private Quaternion originalRotation;

    //旋转平均数组
    private List<float> rotArrayX = new List<float> ();
    private List<float> rotArrayY = new List<float> ();
 
    private void Start ()
    {
  		if (GetComponent<Rigidbody>())
  			GetComponent<Rigidbody>().freezeRotation = true;

       	originalRotation = transform.localRotation;
    }
 
    private void Update ()
	{
        float rotAverageX = 0f; //平滑的平均旋转速度。

        // 收集鼠标输入值并乘以强度，如果玩家瞄准，则强度值除以2。
       // rotationX += Input.GetAxis("Mouse X") * (controller.isAiming ? sensitivityX * 0.5f : sensitivityX);

        rotArrayX.Add(rotationX); // 将当前旋转添加到阵列中，在最后位置。

        // 达到最大步数？从数组中删除最旧的旋转。
        if (rotArrayX.Count >= frameCounter)
        {
            rotArrayX.RemoveAt(0);
        }

        // 把所有这些旋转加在一起。
        for (int i_counterX = 0; i_counterX < rotArrayX.Count; i_counterX++)
        {
            // 循环通过数组。
            rotAverageX += rotArrayX[i_counterX];
        }

        //现在将旋转数除以元素个数以求平均值。
        rotAverageX /= rotArrayX.Count;

        // 平均旋转，与上面相同的过程。
        float rotAverageY = 0;

		//rotationY += Input.GetAxis("Mouse Y") * (controller.isAiming ? sensitivityY * 0.5f : sensitivityY);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);
        rotArrayY.Add(rotationY);

        if (rotArrayY.Count >= frameCounter)
        {
            rotArrayY.RemoveAt(0);
        }

        for (int i_counterY = 0; i_counterY < rotArrayY.Count; i_counterY++)
        {
            rotAverageY += rotArrayY[i_counterY];
        }

        rotAverageY /= rotArrayY.Count;

        //应用并旋转此对象。
        xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
        yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
        transform.localRotation = originalRotation * xQuaternion * yQuaternion;
	}


    // 最小值和最大值之间的夹角。
    // 参数：角度、最小值和最大值。

    private float ClampAngle (float angle, float min, float max)
    {
 		if (angle < -360f)
         	angle += 360f;
 
       	if (angle > 360f)
         	angle -= 360f;

       	return Mathf.Clamp (angle, min, max);
    }
}
