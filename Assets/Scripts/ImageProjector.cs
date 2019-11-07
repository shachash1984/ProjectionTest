using UnityEngine;
using System.Collections;

public class ImageProjector : MonoBehaviour
{
    public Texture ProjectionTexture = null;
    WebCamTexture camFeed = null;
    public Material webCamMaterial;
    public GameObject[] ProjectionReceivers = null;
    public float Angle = 0.0f;
    public Projector sceneCam;
    public int cameraIndex;

    Vector4 Vec3ToVec4(Vector3 vec3, float w)
    {
        return new Vector4(vec3.x, vec3.y, vec3.z, w);
    }

    void Start()
    {
        
        sceneCam = GetComponent<Projector>();
        WebCamDevice[] devices = WebCamTexture.devices;
        string camName = devices[cameraIndex].name;
        camFeed = new WebCamTexture(camName);
        camFeed.Play();
        ProjectionTexture = camFeed;
        ProjectionTexture.wrapMode = TextureWrapMode.Clamp;
        
    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 matProj = Matrix4x4.Perspective(sceneCam.fieldOfView, 1, sceneCam.nearClipPlane, sceneCam.farClipPlane);

        Matrix4x4 matView = Matrix4x4.identity;

        Transform trans = sceneCam.transform;

        matView = Matrix4x4.TRS(Vector3.zero, trans.rotation, Vector3.one);

        
        Vector3 pos = trans.position;
        float x = Vector3.Dot(trans.right, -pos);
        float y = Vector3.Dot(trans.up, -pos);
        float z = Vector3.Dot(trans.forward, -pos);

        matView.SetRow(3, new Vector4(x, y, z, 1));

        Matrix4x4 LightViewProjMatrix = matView * matProj;

        foreach (GameObject imageReceiver in ProjectionReceivers)
        {
            Renderer r = imageReceiver.GetComponent<Renderer>();
            r.sharedMaterial.SetTexture("_ShadowMap", ProjectionTexture);
            r.sharedMaterial.SetMatrix("_LightViewProj", LightViewProjMatrix);
            r.sharedMaterial.SetFloat("_Angle", Angle);
        }
    }

    private void OnDestroy()
    {
        Destroy(camFeed);
    }
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    Gizmos.DrawLine(this.GetComponent<Camera>().transform.position, this.GetComponent<Camera>().transform.position + (this.GetComponent<Camera>().transform.forward * 100.0f));
    //}
}
