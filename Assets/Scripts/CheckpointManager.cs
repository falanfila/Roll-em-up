using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    // Statik değişkenler sahne değişse bile hafızada kalır!
    public static Vector3 SonKayitNoktasi;
    private static bool ilkBaslangic = true;

    void Awake()
    {
        // Eğer oyun ilk defa açılıyorsa, topun başlangıç yerini kaydet
        if (ilkBaslangic)
        {
            SonKayitNoktasi = new Vector3(0, 1, 0); // Burayı topun başlangıç koordinatıyla değiştir!
            ilkBaslangic = false;
        }
    }
}