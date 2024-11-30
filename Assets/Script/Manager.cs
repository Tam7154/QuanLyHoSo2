using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Linq;
using System.Threading.Tasks;
using TMPro;

//load toàn bộ quân nhân
//thêm quân nhân
//xóa quân nhân
//sửa quân nhân
//filter
//đăng nhập
//xuất excel

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    FirebaseFirestore db;

    public List<InfoPerson> infoPerson;

    public TongHopQuanNhanPanel tongHopQuanNhanPanel;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        ReloadAllInfo();
    }

    public async void ReloadAllInfo()
    {
        foreach (Transform item in tongHopQuanNhanPanel.content)
        {
            Destroy(item.gameObject);
        }


        infoPerson = new List<InfoPerson>();

        // Truy cập vào collection và document
        CollectionReference colRef = db.Collection("ho so");

        // Lấy dữ liệu
        await colRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                QuerySnapshot snapshot = task.Result;
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    InfoPerson newInfo = new InfoPerson();
                    newInfo.infoPerson = document.ToDictionary();
                    newInfo.name = document.GetValue<string>("ten");
                    infoPerson.Add(newInfo);

                    GameObject slot = Instantiate(Resources.Load("Slot in TongHopQuanNhan") as GameObject, tongHopQuanNhanPanel.content);
                    slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = document.GetValue<string>("ten");
                    slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = document.GetValue<string>("don vi");
                    slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = document.GetValue<string>("nam sinh");

                    // Lấy field cụ thể từ mỗi tài liệu
                    //if (document.TryGetValue("ten", out string name))
                    //{
                    //    Debug.Log($"ten: {name}");
                    //}
                }
            }
            else
            {
                Debug.LogError("Không thể lấy document hoặc document không tồn tại.");
            }
        });

        //lay du lieu cho filter
        foreach (var item in tongHopQuanNhanPanel.filters)
        {

        }
    }
}
