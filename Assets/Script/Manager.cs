using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

//load toàn bộ quân nhân
//thêm quân nhân //
//xóa quân nhân //
//sửa quân nhân
//filter //
//đăng nhập
//xuất excel

//Aoyama%20Aina
//SUKE-080
//MILK-067
//sayama love

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

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        Add(14, "c1");
    //        Add(14, "c2");
    //        Add(14, "c3");
    //        Add(14, "c4");
    //    }
    //}

    public async void CheckForLogin(string us, string pw)
    {
        GameObject loading = Instantiate(Resources.Load("Loading Panel") as GameObject, GameObject.Find("Canvas").transform);

        await Task.Delay(1000);

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

        Destroy(loading);

        if (curUser != null)
        {
            StartQuanLy();
        }
        else
        {
            NotificationManager.CreateNoti("Sai tên tài khoản hoặc mật khẩu!");
        }
    }

    public void StartLogin()
    {
        loginLayout.SetActive(true);
    }
    void Initialize()
    {
        db = FirebaseFirestore.DefaultInstance;

        foreach (var item in tongHopQuanNhanPanel.dropdownMultiSelects)
        {
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                tongHopQuanNhanPanel.ShowListInfo();
            });
        }
    }
    public void StartQuanLy()
    {
        loginLayout.SetActive(false);
        ReloadAllInfo();
    }

    public async void ReloadAllInfo()
    {
        GameObject loading = Instantiate(Resources.Load("Loading Panel") as GameObject, GameObject.Find("Canvas").transform);

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
                }
            }
            else
            {
                Debug.LogError("Không thể lấy document hoặc document không tồn tại.");
            }
        });

        Destroy(loading);

        //lay du lieu cho filter
        //tongHopQuanNhanPanel.filters.ForEach(n => n.data.Clear());

        foreach (var item in tongHopQuanNhanPanel.dropdownMultiSelects)
        {
            for (int i = 0; i < item.items.Count; i++)
            {
                item.RemoveItem(item.items[i].itemName);
            }
        }

        foreach (var item in tongHopQuanNhanPanel.dropdownMultiSelects)
        {
            foreach (var jtem in infoPerson)
            {
                string var = jtem.infoPerson.GetValueOrDefault(item.name).ToString();

                bool contain = false;

                foreach (var ntem in item.items)
                {
                    if (ntem.itemName == var)
                    {
                        contain = true;
                        break;
                    }
                }

                if (!contain)
                {
                    item.CreateNewItem(var, true);

                }
            }

            item.UpdateItemLayout();
        }

        //foreach (var item in tongHopQuanNhanPanel.filters)
        //{
        //    foreach (var jtem in infoPerson)
        //    {
        //        string var = jtem.infoPerson.GetValueOrDefault(item.name).ToString();

        //        if (!item.data.ContainsKey(item.name) &&
        //            !item.data.ContainsKey(var))
        //        {
        //            item.data.Add(var, true);
        //        }
        //    }
        //}

        tongHopQuanNhanPanel.ShowListInfo();
    }

    void Add(int count, string c)
    {
        for (int i = 0; i < count; i++)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ben ngoai", "ông ngoại: ... \nbà ngoại: ...");
            dic.Add("ben noi", "ông nội: ... \nbà nội: ...");
            dic.Add("benh ly", "Không");
            dic.Add("cap bac", "Binh nhất");
            dic.Add("cho di nuoc ngoai", "Không");
            dic.Add("cho o hien nay", "Bạc Liêu");
            dic.Add("chuc vu", "Chiến sĩ");
            dic.Add("co ai la can bo", "Không");
            dic.Add("da tung di nuoc ngoai", "Không");
            dic.Add("don vi", c);
            dic.Add("hinh xam", "Không");
            dic.Add("ho ten cha", "...");
            dic.Add("ho ten me", "...");
            dic.Add("nam sinh cha", "1975");
            dic.Add("nam sinh me", "1975");
            dic.Add("nghe nghiep cha", "Làm nông");
            dic.Add("nghe nghiep me", "Làm nông");
            dic.Add("khieu nai", "Không");
            dic.Add("kinh te gia dinh", "Đủ ăn");
            dic.Add("ma tuy", "Không");
            dic.Add("mo coi", "Không");
            dic.Add("nam sinh", "");
            dic.Add("ngay nhap ngu", "27/2/2024");
            dic.Add("ngay vao doan", "");
            dic.Add("ngay vao dang", "");
            dic.Add("nghe nghiep", "Làm nông");
            dic.Add("nguoi than di nuoc ngoai", "Không");
            dic.Add("quan diem ve quan he dong gioi", "Không");
            dic.Add("quan diem ve the che chinh tri", "Không");
            dic.Add("que quan", "Bạc Liêu");
            dic.Add("ten", "");
            dic.Add("tham gia bieu tinh", "Không");
            dic.Add("tham gia dang", "Không");
            dic.Add("thong tin anh chi em", "...");
            dic.Add("toa an", "Không");
            dic.Add("trinh do van hoa", "9/12");
            dic.Add("vo", "Không");
            dic.Add("xu ly hanh chinh", "Không");

            InfoPerson newInfo = new InfoPerson();
            newInfo.infoPerson = new Dictionary<string, object>();
            foreach (var item in dic)
            {
                var v = item.Value;
                if (item.Key == "ten")
                {
                    List<string> s1 = new List<string>() { "Trần", "Nguyễn", "Phạm", "Phan", "Hồ", "Danh", "Lý", "Lê", "Đinh", "Võ", "Huỳnh", "Trương", "Bùi", "Đặng", "Đỗ", "Ngô", "Dương", "Trịnh" };
                    List<string> s2 = new List<string>() { "Văn", "Phúc", "Quang", "Hoài", "Anh", "Tùng", "Bá", "Duy", "Việt", "Thanh", "Minh", "Ngọc", "Hồng", };
                    List<string> s3 = new List<string>() { "Anh", "Tố", "Hữu", "Phú", "Quân", "Quang", "Đào", "Bằng", "Việt", "Long", "An", "Ân", "Bảo", "Nhân", "Minh", "Thái", "Cường", "Chu", "Giang", "Hoàng", "Hy", "Khôi", "Khải", "Lâm", "Linh", "Nhã", "Phúc", "Phương" };
                    string ss1 = s1[Random.Range(0, s1.Count)];
                    string ss2 = s2[Random.Range(0, s2.Count)];
                    string ss3 = s3[Random.Range(0, s3.Count)];
                    while (ss2 == ss3)
                    {
                        ss2 = s2[Random.Range(0, s2.Count)];
                        ss3 = s3[Random.Range(0, s3.Count)];
                    }
                    v = $"{ss1} {ss2} {ss3}";
                }
                if (item.Key == "nam sinh")
                {
                    List<string> s1 = new List<string>() { "1997", "1998", "1999", "2000", "2001", "2002", "2003", "2004", "2005" };
                    v = s1[Random.Range(0, s1.Count)];
                }
                newInfo.infoPerson.Add(item.Key, v);
            }
            Manager.Instance.Add(newInfo, false);
        }
    }
    public async void Add(InfoPerson infoPerson, bool reload = true)
    {
        GameObject loading = Instantiate(Resources.Load("Loading Panel") as GameObject, GameObject.Find("Canvas").transform);

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

        Destroy(loading);

        if (reload)
        {
            ReloadAllInfo();
        }
    }
    //By The Way
    //ながえSTYLE

    public async void Delete(string s)
    {
        GameObject loading = Instantiate(Resources.Load("Loading Panel") as GameObject, GameObject.Find("Canvas").transform);

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

        Destroy(loading);

        ReloadAllInfo();
    }
}
