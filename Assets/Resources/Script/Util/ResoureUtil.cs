using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUtil : MonoBehaviour
{
    /// <summary>
    /// �Ҽ��� ���ڸ��������� ����ϴ� �޼���(Vector3 ��)
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    public static Vector3 RoundVecFloat(Vector3 vec)
    {
        string vecXstring = string.Format("{0:N2}", vec.x);
        string vecYstring = string.Format("{0:N2}", vec.y);
        string vecZstring = string.Format("{0:N2}", vec.z);

        float vecX = float.Parse(vecXstring);
        float vecY = float.Parse(vecYstring);
        float vecZ = float.Parse(vecZstring);

        Vector3 vector3 = new Vector3(vecX, vecY, vecZ);
        return vector3;
    }

    /// <summary>
    /// ������ ���ڸ��������� ����ϴ� �޼���(Quaternion ��)
    /// </summary>
    /// <param name="qut"></param>
    /// <returns></returns>
    public static Quaternion RoundRotFloat(Quaternion qut)
    {
        string qutXstring = string.Format("{0:N2}", qut.x);
        string qutYstring = string.Format("{0:N2}", qut.y);
        string qutZstring = string.Format("{0:N2}", qut.z);

        float qutX = float.Parse(qutXstring);
        float qutY = float.Parse(qutYstring);
        float qutZ = float.Parse(qutZstring);

        Quaternion quaternion = new Quaternion(qutX, qutY, qutZ,0);
        return quaternion;
    }
}
