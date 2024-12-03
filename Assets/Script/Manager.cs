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
//thêm quân nhân //
//xóa quân nhân //
//sửa quân nhân
//filter //
//đăng nhập
//xuất excel

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    FirebaseFirestore db;

    public GameObject loginLayout;

    public List<InfoPerson> infoPerson;

    public TongHopQuanNhanPanel tongHopQuanNhanPanel;

    public UserInfo curUser;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartLogin();
        Initialize();
    }

    public async void CheckForLogin(string us, string pw)
    {
        CollectionReference colRef = db.Collection("users");

        await colRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                QuerySnapshot snapshot = task.Result;
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.GetValue<string>("id") == us &&
                    document.GetValue<string>("pw") == pw)
                    {
                        Dictionary<string, object> dict = document.ToDictionary();
                        curUser = new UserInfo(dict);
                        break;
                    }
                }
            }
            else
            {
                Debug.LogError("Không thể lấy document hoặc document không tồn tại.");
            }
        });

        if (curUser != null)
        {
            StartQuanLy();
        }
    }

    public void StartLogin()
    {
        loginLayout.SetActive(true);
    }
    void Initialize()
    {
        db = FirebaseFirestore.DefaultInstance;
    }
    public void StartQuanLy()
    {
        loginLayout.SetActive(false);
        ReloadAllInfo();
    }

    public async void ReloadAllInfo()
    {
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

                    //GameObject slot = Instantiate(Resources.Load("Slot in TongHopQuanNhan") as GameObject, tongHopQuanNhanPanel.content);
                    //slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = document.GetValue<string>("ten");
                    //slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = document.GetValue<string>("don vi");
                    //slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = document.GetValue<string>("nam sinh");

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
        tongHopQuanNhanPanel.filters.ForEach(n => n.data.Clear());

        foreach (var item in tongHopQuanNhanPanel.filters)
        {
            foreach (var jtem in infoPerson)
            {
                string var = jtem.infoPerson.GetValueOrDefault(item.name).ToString();

                if (!item.data.ContainsKey(item.name) &&
                    !item.data.ContainsKey(var))
                {
                    item.data.Add(var, true);
                }
            }
        }

        ShowInfo();
    }

    public void ShowInfo()
    {
        foreach (Transform item in tongHopQuanNhanPanel.content)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in infoPerson)
        {
            bool b = true;
            foreach (var jtem in tongHopQuanNhanPanel.filters)
            {
                if (!jtem.data[item.infoPerson[jtem.gameObject.name].ToString()])
                {
                    b = false;
                    break;
                }
            }

            if (b)
            {
                GameObject slot = Instantiate(Resources.Load("Slot in TongHopQuanNhan") as GameObject, tongHopQuanNhanPanel.content);
                slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("ten").ToString();
                slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("don vi").ToString();
                slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("nam sinh").ToString();
                slot.name = $"{item.infoPerson.GetValueOrDefault("ten")}:{item.infoPerson.GetValueOrDefault("don vi")}:{item.infoPerson.GetValueOrDefault("nam sinh")}";
                slot.GetComponent<SlotInTongHopQuanNhan>().infoPerson = item;
            }
        }
    }

    public async void Add(InfoPerson infoPerson)
    {
        string newId = $"{infoPerson.infoPerson["ten"]}:{infoPerson.infoPerson["don vi"]}:{infoPerson.infoPerson["nam sinh"]}";
        DocumentReference docRef = db.Collection("ho so").Document(newId);

        await docRef.SetAsync(infoPerson.infoPerson).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log($"Document với ID '{newId}' được thêm thành công!");
                NotificationManager.CreateNoti("Hồ sơ đã được thêm thành công.");
            }
            else
            {
                Debug.LogError("Có lỗi xảy ra khi thêm document.");
                NotificationManager.CreateNoti("Có lỗi xảy ra khi thêm hồ sơ.");
            }
        });

        ReloadAllInfo();
    }


    public async void Delete(string s)
    {
        await db.Collection("ho so").Document(s).DeleteAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Tài liệu đã bị xóa.");
                NotificationManager.CreateNoti("Hồ sơ đã bị xóa.");
            }
            else
            {
                Debug.LogError("Có lỗi xảy ra khi xóa tài liệu: " + task.Exception);
                NotificationManager.CreateNoti("Có lỗi xảy ra khi xóa hồ sơ.");
            }
        });

        ReloadAllInfo();
    }
}
