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
using Firebase;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System;
using Random = UnityEngine.Random;
using System.Windows.Forms;
using Application = UnityEngine.Application;

public class data
{
    public string oldPassword;
    public string newPassword;
}

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    FirebaseFirestore db;

    public GameObject loginLayout;

    public List<InfoPerson> infoPerson;

    public TongHopQuanNhanPanel tongHopQuanNhanPanel;

    public UserInfo curUser;

    public int currentId = 0;

    public SystemData systemData;

    public string URL = "http://localhost:3001/";
    private readonly HttpClient _httpClient = new HttpClient();

    public TMPro.TMP_InputField input;

    public Accounts accounts;
    public InfoAccount currentAccount;

    public InfoMembers infoMembers;

    [Header("path")]
    //string path1 = @"\\192.168.1.10\share file";
    string path1 = @"\\LAPTOP-K8JJVH7S\share file";
    string path2 = @"\\192.168.1.11\share file";
    string path3 = @"\\192.168.1.12\share file";

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //string ip1 = @"\\192.168.1.11";
        //string ip2 = @"\\192.168.1.12";

        //if (!CheckUseNet(ip1))
        //{
        //    UseNet(ip1);
        //}

        GetAccInfo();
        StartLogin();
        Initialize();

        //string path = @"\\192.168.1.10\share file\account.txt";
        //string path = @"\\C2\z\z.txt";

        //if (File.Exists(path))
        //{
        //    // Đọc nội dung của file
        //    string fileContent = File.ReadAllText(path);
        //    Debug.Log("File content: " + fileContent);
        //}
        //else
        //{
        //    Debug.LogError("File không tồn tại ở đường dẫn: " + path);
        //}
    }

    void GetAccInfo()
    {
        string path = $"{path1}\\account.txt";
        //string path = @"\\192.168.1.10\share file\account.txt";
        if (File.Exists(path))
        {
            // Đọc nội dung của file
            //string fileContent = File.ReadAllText(path);
            //Debug.Log("File content: " + fileContent);
            //accounts = JsonUtility.FromJson<Accounts>(fileContent);
            accounts = Data.Load<Accounts>(path);
        }
        else
        {
            Debug.LogError("File không tồn tại ở đường dẫn: " + path);
        }
    }

    bool CheckUseNet(string ip)
    {
        try
        {
            // Tạo một Process để thực thi lệnh CMD
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = $"/C net use | findstr \"{ip}\""; // Lệnh CMD
            Debug.Log(process.StartInfo.Arguments);
            process.StartInfo.RedirectStandardOutput = true; // Chuyển hướng đầu ra
            process.StartInfo.UseShellExecute = false; // Không sử dụng Shell
            process.StartInfo.CreateNoWindow = true; // Ẩn cửa sổ CMD

            // Khởi chạy Process
            process.Start();

            // Đọc kết quả từ đầu ra
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Dispose();

            if (!string.IsNullOrEmpty(output))
            {
                Debug.Log($"Kết nối tới {ip} đã tồn tại:\n{output}");
                return true;
            }
            else
            {
                Debug.Log($"Không tìm thấy kết nối tới {ip}.");
                return false;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Đã xảy ra lỗi khi kiểm tra kết nối mạng: {ex.Message}");
            return false;
        }
    }

    void UseNet(string ip)
    {
        try
        {
            // Tạo một Process để thực thi lệnh CMD
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = $"net use {ip} admin"; // Lệnh CMD
            Debug.Log(process.StartInfo.Arguments);
            process.StartInfo.RedirectStandardOutput = true; // Chuyển hướng đầu ra
            process.StartInfo.UseShellExecute = false; // Không sử dụng Shell
            process.StartInfo.CreateNoWindow = true; // Ẩn cửa sổ CMD

            // Khởi chạy Process
            process.Start();

            // Đọc kết quả từ đầu ra
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Dispose();

            if (!string.IsNullOrEmpty(output))
            {
                Debug.Log($"Đã kết nối tới {ip} :\n{output}");
            }
            else
            {
                Debug.Log($"Không tìm thấy kết nối tới {ip}.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Đã xảy ra lỗi khi kiểm tra kết nối mạng: {ex.Message}");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentAccount.id == "god")
        {
            Add(1, "1");
            //Accounts accounts = new Accounts();
            //accounts.accounts = new List<InfoAccount>();
            //accounts.accounts.Add(new InfoAccount()
            //{
            //    id = "admin",
            //    password = "admin",
            //    role = "2",
            //});

            //accounts.accounts.Add(new InfoAccount()
            //{
            //    id = "le long",
            //    password = "le long",
            //    role = "2",
            //});

            //accounts.accounts.Add(new InfoAccount()
            //{
            //    id = "le tam",
            //    password = "le tam",
            //    role = "3",
            //});

            //accounts.accounts.Add(new InfoAccount()
            //{
            //    id = "god",
            //    password = "god",
            //    role = "1",
            //});

            //string path = $"{path1}\\account.txt";
            //Data.Save(path, accounts);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentAccount.id == "god")
        {
            Add(1, "2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && currentAccount.id == "god")
        {
            Add(1, "3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && currentAccount.id == "god")
        {
            Add(1, "4");
        }
        if (Input.GetKeyDown(KeyCode.T) && currentAccount.id == "god")
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog.Title = "Chọn một file";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadImageToTexture(openFileDialog.FileName);
            }
        }

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    //run(input.text);
        //    Create(1, "1");
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    run(input.text);
        //    //Add2(1, "1");
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    Insert(5, "1");
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    GetInfo();
        //}

        //if (curUser != null && int.Parse(curUser.levelControl.ToString()) == 1)
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha1))
        //    {
        //        Add2(5, "1");
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha2))
        //    {
        //        Add2(5, "2");
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha3))
        //    {
        //        Add2(5, "3");
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha4))
        //    {
        //        Add2(5, "4");
        //    }
        //}
    }

    private void LoadImageToTexture(string path)
    {
        // Đọc dữ liệu từ file hình ảnh
        byte[] imageBytes = System.IO.File.ReadAllBytes(path);
        string nameImg = Path.GetFileName(path);
        string newPath = Application.streamingAssetsPath + "/" + nameImg;
        File.WriteAllBytes(newPath, imageBytes);

        //File.Copy(path, newPath);

        // Tạo Texture2D từ dữ liệu
        //Texture2D texture = new Texture2D(2, 2);
        //texture.LoadImage(imageBytes);

        // Hiển thị Texture lên GameObject (nếu cần)
        //Renderer renderer = GetComponent<Renderer>();
        //if (renderer != null)
        //{
        //    renderer.material.mainTexture = texture;
        //}
    }
    public async void run(string query)
    {
        print("run: " + query);
        string url = URL + "get-function";

        var content = new StringContent(JsonUtility.ToJson(new data()
        {
            oldPassword = query,
            newPassword = "bbb"
        }), Encoding.UTF8, "application/json");

        try
        {
            // Gửi yêu cầu POST đến Node.js server
            var response = await _httpClient.PostAsync(url, content);
            // Đọc nội dung phản hồi từ Node.js server
            var responseContent = await response.Content.ReadAsStringAsync();
            // Trả về phản hồi từ Node.js API tới client
            if (response.IsSuccessStatusCode)
            {
                print("A");
                print(responseContent);
                //a = responseContent;
                //return responseContent;
            }
            else
            {
                print("B");
                Debug.Log("Error " + responseContent);
            }
        }
        catch (HttpRequestException ex)
        {
            print("C");
            Debug.Log("Error " + ex.Message);
        }
        //return "";
    }

    public async void CheckForLogin(string us, string pw)
    {
        GameObject loading = Instantiate(Resources.Load("Loading Panel") as GameObject, GameObject.Find("Canvas").transform);

        //await Task.Delay(1000);


        Destroy(loading);

        #region LAN
        foreach (var item in accounts.accounts)
        {
            if (item.id == us && item.password == pw)
            {
                currentAccount = item;
                StartQuanLy();
            }
        }
        #endregion

        #region use google

        //CollectionReference colRef = db.Collection("users");

        //await colRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        //{
        //    if (task.IsCompleted)
        //    {
        //        QuerySnapshot snapshot = task.Result;
        //        foreach (DocumentSnapshot document in snapshot.Documents)
        //        {
        //            if (document.GetValue<string>("id") == us &&
        //                    document.GetValue<string>("pw") == pw)
        //            {
        //                Dictionary<string, object> dict = document.ToDictionary();
        //                curUser = new UserInfo(dict);
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogError("Không thể lấy document hoặc document không tồn tại.");
        //    }
        //});

        //Destroy(loading);

        //if (curUser != null)
        //{
        //    StartQuanLy();
        //}
        //else
        //{
        //    NotificationManager.CreateNoti("Sai tên tài khoản hoặc mật khẩu!");
        //}
        #endregion
    }

    public void StartLogin()
    {
        loginLayout.SetActive(true);
    }
    void Initialize()
    {
        #region use LAN

        string path = path1 + "\\system.txt";
        string content = File.ReadAllText(path);
        if (string.IsNullOrEmpty(content))
        {
            systemData.currentId = "1";
        }
        else
        {
            systemData = Data.Load<SystemData>(path);
        }
        #endregion

        #region use gg
        //db = FirebaseFirestore.DefaultInstance;

        //foreach (var item in tongHopQuanNhanPanel.dropdownMultiSelects)
        //{
        //    item.GetComponent<Button>().onClick.AddListener(() =>
        //    {
        //        tongHopQuanNhanPanel.ShowListInfo();
        //    });
        //}
        #endregion
    }

    private void OnDestroy()
    {
        SaveSystemData();
    }

    void SaveSystemData()
    {
        #region use LAN
        Data.Save(path1 + "\\system.txt", systemData);
        #endregion
    }
    public void StartQuanLy()
    {
        loginLayout.SetActive(false);
        ReloadAllInfo();
    }

    public async void ReloadAllInfo()
    {
        //GameObject loading = Instantiate(Resources.Load("Loading Panel") as GameObject, GameObject.Find("Canvas").transform);
        //Destroy(loading);

        print("reload");
        #region use LAN
        //string content = File.ReadAllText(path1 + "\\members.txt");
        //infoMembers = JsonUtility.FromJson<InfoMembers>(content);
        string path = path1 + "\\members.txt";
        infoMembers = Data.Load<InfoMembers>(path);

        foreach (var item in tongHopQuanNhanPanel.dropdownMultiSelects)
        {
            for (int i = 0; i < item.items.Count; i++)
            {
                item.RemoveItem(item.items[i].itemName);
            }
        }

        foreach (var item in tongHopQuanNhanPanel.dropdownMultiSelects)
        {
            foreach (var jtem in infoMembers.members)
            {
                Dictionary<string, string> map = new Dictionary<string, string>();
                for (int i = 0; i < jtem.nameInfo.Count; i++)
                {
                    map.Add(jtem.nameInfo[i], jtem.info[i]);
                }

                string var = map.GetValueOrDefault(item.name).ToString();

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

        foreach (var item in tongHopQuanNhanPanel.dropdownMultiSelects)
        {
            foreach (var jtem in item.items)
            {
                jtem.onValueChanged.AddListener((x) =>
                {
                    tongHopQuanNhanPanel.ShowListInfo();
                });
            }
        }

        #endregion

        #region use gg
        //GameObject loading = Instantiate(Resources.Load("Loading Panel") as GameObject, GameObject.Find("Canvas").transform);

        //infoPerson = new List<InfoPerson>();

        // Lấy dữ liệu system
        //CollectionReference colSystem = db.Collection("system");
        //await colSystem.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        //{
        //    if (task.IsCompleted)
        //    {
        //        QuerySnapshot snapshot = task.Result;
        //        foreach (DocumentSnapshot document in snapshot.Documents)
        //        {
        //            var v = document.ToDictionary();
        //            currentId = int.Parse(v.GetValueOrDefault("current id").ToString());
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogError("Không thể lấy document hoặc document không tồn tại.");
        //    }
        //});

        //// Truy cập vào collection và document
        //CollectionReference colRef = db.Collection("ho so");

        //// Lấy dữ liệu của quân nhân
        //await colRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        //{
        //    if (task.IsCompleted)
        //    {
        //        QuerySnapshot snapshot = task.Result;
        //        foreach (DocumentSnapshot document in snapshot.Documents)
        //        {
        //            InfoPerson newInfo = new InfoPerson();
        //            newInfo.infoPerson = document.ToDictionary();
        //            //newInfo.name = document.GetValue<string>("ten");
        //            infoPerson.Add(newInfo);
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogError("Không thể lấy document hoặc document không tồn tại.");
        //    }
        //});

        //Destroy(loading);

        //lay du lieu cho filter
        //tongHopQuanNhanPanel.filters.ForEach(n => n.data.Clear());

        //foreach (var item in tongHopQuanNhanPanel.dropdownMultiSelects)
        //{
        //    for (int i = 0; i < item.items.Count; i++)
        //    {
        //        item.RemoveItem(item.items[i].itemName);
        //    }
        //}

        //foreach (var item in tongHopQuanNhanPanel.dropdownMultiSelects)
        //{
        //    foreach (var jtem in infoPerson)
        //    {
        //        string var = jtem.infoPerson.GetValueOrDefault(item.name).ToString();

        //        bool contain = false;

        //        foreach (var ntem in item.items)
        //        {
        //            if (ntem.itemName == var)
        //            {
        //                contain = true;
        //                break;
        //            }
        //        }

        //        if (!contain)
        //        {
        //            item.CreateNewItem(var, true);

        //        }
        //    }

        //    item.UpdateItemLayout();
        //}

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

        #endregion
        tongHopQuanNhanPanel.ShowListInfo();
    }

    async void Add(int count, string c)
    {
        #region use LAN

        Dictionary<string, string> dict = GetRandomDic();
        dict["don vi"] = c;

        string path = @"\\192.168.1.10\share file\members.txt";
        if (File.Exists(path))
        {
            var content = File.ReadAllText(path);
            InfoMembers infoMembers = new InfoMembers();
            infoMembers.members = new List<InfoMember>();
            if (!string.IsNullOrEmpty(content))
            {
                infoMembers = Data.Load<InfoMembers>(path);
                //infoMembers = JsonUtility.FromJson<InfoMembers>(content);
            }

            InfoMember infoMember = new InfoMember();
            foreach (var item in dict)
            {
                if (item.Key == "ho va ten")
                {
                    print(item.Value);
                }
                infoMember.nameInfo.Add(item.Key.ToString());
                infoMember.info.Add(item.Value.ToString());
            }

            infoMembers.members.Add(infoMember);

            Data.Save(path, infoMembers);
        }
        else
        {
            print("File no exist to add members");
        }

        ReloadAllInfo();
        #endregion

        #region use gg
        //for (int i = 0; i < count; i++)
        //{
        //    Dictionary<string, string> dic = new Dictionary<string, string>();

        //    #region
        //    dic.Add("benh ly", "Không");
        //    dic.Add("benh ly ba ngoai", "Không");
        //    dic.Add("benh ly ba noi", "Không");
        //    dic.Add("benh ly ong ngoai", "Không");
        //    dic.Add("benh ly ong noi", "Không");

        //    dic.Add("benh tat cha", "Không");
        //    dic.Add("benh tat me", "Không");

        //    dic.Add("co quan cong tac ba ngoai", "Bạc Liêu");
        //    dic.Add("co quan cong tac ba noi", "Bạc Liêu");
        //    dic.Add("co quan cong tac ong ngoai", "Bạc Liêu");
        //    dic.Add("co quan cong tac ong noi", "Bạc Liêu");

        //    dic.Add("con song hay da mat cha", "Còn sống");
        //    dic.Add("con song hay da mat me", "Còn sống");

        //    dic.Add("dan toc", "Kinh");

        //    dic.Add("dia chi khi di phep, tranh thu", "Bạc Liêu");

        //    dic.Add("hien nay cha, me dang o dau", "Ở chung");

        //    dic.Add("hinh the", "1m70, 55kg");

        //    dic.Add("ho ten cha", "Nguyễn..");
        //    dic.Add("ho ten me", "Nguyễn..");


        //    dic.Add("ho ten ba ngoai", "Nguyễn..");
        //    dic.Add("ho ten ba noi", "Nguyễn..");
        //    dic.Add("ho ten ong ngoai", "Nguyễn..");
        //    dic.Add("ho ten ong noi", "Nguyễn..");

        //    dic.Add("khi can bao tin cho ai", "cha: Nguyễn Văn A");

        //    dic.Add("khi chat", "Sôi nổi");

        //    dic.Add("nam sinh ba ngoai", "1950");
        //    dic.Add("nam sinh ba noi", "1950");
        //    dic.Add("nam sinh ong ngoai", "1950");
        //    dic.Add("nam sinh ong noi", "1950");

        //    dic.Add("nam sinh cha", "1975");
        //    dic.Add("nam sinh me", "1975");

        //    dic.Add("nganh nghe chuyen mon da duoc dao tao qua truong", "Không");

        //    dic.Add("nganh nghe chuyen mon tu hoc va biet lam", "Không");

        //    dic.Add("ngay sinh", "1");
        //    dic.Add("thang sinh", "1");
        //    dic.Add("nam sinh", "1");

        //    dic.Add("nguyen quan", "Bạc Liêu");

        //    dic.Add("noi dang ky ho khau thuong tru", "Bạc Liêu");

        //    dic.Add("noi o hien nay ba ngoai", "Bạc Liêu");
        //    dic.Add("noi o hien nay ba noi", "Bạc Liêu");
        //    dic.Add("noi o hien nay ong ngoai", "Bạc Liêu");
        //    dic.Add("noi o hien nay ong noi", "Bạc Liêu");

        //    dic.Add("nghe nghiep cha", "Làm ruộng");
        //    dic.Add("nghe nghiep me", "Làm ruộng");

        //    dic.Add("nghe nghiep ba ngoai", "Làm ruộng");
        //    dic.Add("nghe nghiep ba noi", "Làm ruộng");
        //    dic.Add("nghe nghiep ong ngoai", "Làm ruộng");
        //    dic.Add("nghe nghiep ong noi", "Làm ruộng");

        //    dic.Add("so dien thoai cha", "0900xxxx");
        //    dic.Add("so dien thoai me", "0900xxxx");

        //    dic.Add("so dien thoai ba ngoai", "0900xxxx");
        //    dic.Add("so dien thoai ba noi", "0900xxxx");
        //    dic.Add("so dien thoai ong ngoai", "0900xxxx");
        //    dic.Add("so dien thoai ong noi", "0900xxxx");

        //    dic.Add("suc khoe ba ngoai", "Khỏe mạnh");
        //    dic.Add("suc khoe ba noi", "Khỏe mạnh");
        //    dic.Add("suc khoe ong ngoai", "Khỏe mạnh");
        //    dic.Add("suc khoe ong noi", "Khỏe mạnh");

        //    dic.Add("tinh trang hon nhan", "Hạnh phúc");

        //    dic.Add("tinh trang suc khoe cha", "Khỏe mạnh");
        //    dic.Add("tinh trang suc khoe me", "Khỏe mạnh");

        //    dic.Add("toggle con song ba ngoai", "true");
        //    dic.Add("toggle con song ba noi", "true");
        //    dic.Add("toggle con song ong ngoai", "true");
        //    dic.Add("toggle con song ong noi", "true");

        //    dic.Add("toggle da mat ba ngoai", "false");
        //    dic.Add("toggle da mat ba noi", "false");
        //    dic.Add("toggle da mat ong ngoai", "false");
        //    dic.Add("toggle da mat ong noi", "false");

        //    dic.Add("toggle dang vien ba ngoai", "false");
        //    dic.Add("toggle dang vien ba noi", "false");
        //    dic.Add("toggle dang vien ong ngoai", "false");
        //    dic.Add("toggle dang vien ong noi", "false");

        //    dic.Add("toggle day la moi truong gian nan vat va", "false");

        //    dic.Add("toggle day la moi truong tot ren luyen con nguoi", "true");

        //    dic.Add("toggle di hoc sy quan", "false");
        //    dic.Add("toggle di hoc sy quan du bi", "false");
        //    dic.Add("toggle duoc di hoc chuyen mon ky thuat", "false");
        //    dic.Add("toggle duoc di hoc tieu doi truong, khau doi truong", "false");
        //    dic.Add("toggle ket nap vao dang", "false");
        //    dic.Add("toggle ra quan khi het nghia vu", "true");

        //    dic.Add("ton giao", "Không");

        //    dic.Add("trinh do hoc van", "12/12");

        //    dic.Add("don vi", c);

        //    dic.Add("ho va ten", "");
        //    dic.Add("ho va ten dang dung", "");

        //    dic.Add("truoc khi nhap ngu", "Làm thuê ở Tp.Hồ Chí Minh, 1 năm");

        //    dic.Add("ly do di bo doi", "Rèn luyện bản thân");

        //    dic.Add("ho va ten anh chi em 1", "Nguyễn Văn..");
        //    dic.Add("ho va ten anh chi em 2", "Nguyễn Văn..");
        //    dic.Add("ho va ten anh chi em 3", "Nguyễn Văn..");
        //    dic.Add("ho va ten anh chi em 4", "Nguyễn Văn..");
        //    dic.Add("ho va ten anh chi em 5", "Nguyễn Văn..");

        //    dic.Add("nam sinh anh chi em 1", "2000");
        //    dic.Add("nam sinh anh chi em 2", "2000");
        //    dic.Add("nam sinh anh chi em 3", "2000");
        //    dic.Add("nam sinh anh chi em 4", "2000");
        //    dic.Add("nam sinh anh chi em 5", "2000");

        //    dic.Add("nghe nghiep anh chi em 1", "Làm thuê");
        //    dic.Add("nghe nghiep anh chi em 2", "Làm thuê");
        //    dic.Add("nghe nghiep anh chi em 3", "Làm thuê");
        //    dic.Add("nghe nghiep anh chi em 4", "Làm thuê");
        //    dic.Add("nghe nghiep anh chi em 5", "Làm thuê");

        //    dic.Add("so dien thoai anh chi em 1", "0900XXXX");
        //    dic.Add("so dien thoai anh chi em 2", "0900XXXX");
        //    dic.Add("so dien thoai anh chi em 3", "0900XXXX");
        //    dic.Add("so dien thoai anh chi em 4", "0900XXXX");
        //    dic.Add("so dien thoai anh chi em 5", "0900XXXX");

        //    dic.Add("ho va ten ban gai", "Nguyễn XXX");
        //    dic.Add("que quan ban gai", "Bạc Liêu");
        //    dic.Add("nam sinh ban gai", "2000");
        //    dic.Add("nghe nghiep ban gai", "2000");
        //    dic.Add("so dien thoai ban gai", "0900XXX");

        //    dic.Add("ho va ten ban trai", "Nguyễn XXX");
        //    dic.Add("que quan ban trai", "Bạc Liêu");
        //    dic.Add("nam sinh ban trai", "2000");
        //    dic.Add("nghe nghiep ban trai", "2000");
        //    dic.Add("so dien thoai ban trai", "0900XXX");

        //    dic.Add("ho va ten ban gai than nhat", "");
        //    dic.Add("nam sinh ban gai than nhat", "");
        //    dic.Add("dia chi ban gai than nhat", "");
        //    dic.Add("so dien thoai ban gai than nhat", "");

        //    dic.Add("ho va ten ban trai than nhat", "");
        //    dic.Add("nam sinh ban trai than nhat", "");
        //    dic.Add("dia chi ban trai than nhat", "");
        //    dic.Add("so dien thoai ban trai than nhat", "");

        //    dic.Add("ai la nguoi anh huong tich cuc", "");

        //    dic.Add("ho va ten can bo dia phuong 1", "");
        //    dic.Add("chuc vu can bo dia phuong 1", "");
        //    dic.Add("sdt can bo dia phuong 1", "");

        //    dic.Add("ho va ten can bo dia phuong 2", "");
        //    dic.Add("chuc vu can bo dia phuong 2", "");
        //    dic.Add("sdt can bo dia phuong 2", "");

        //    dic.Add("ho va ten can bo dia phuong 3", "");
        //    dic.Add("chuc vu can bo dia phuong 3", "");
        //    dic.Add("sdt can bo dia phuong 3", "");

        //    dic.Add("ho va ten can bo dia phuong 4", "");
        //    dic.Add("chuc vu can bo dia phuong 4", "");
        //    dic.Add("sdt can bo dia phuong 4", "");

        //    dic.Add("ho va ten can bo dia phuong 5", "");
        //    dic.Add("chuc vu can bo dia phuong 5", "");
        //    dic.Add("sdt can bo dia phuong 5", "");

        //    dic.Add("lam gi o dau thoi gian truoc khi nhap ngu", "");
        //    dic.Add("nghe nghiep truoc khi nhap ngu", "");
        //    dic.Add("noi lam viec truoc khi nhap ngu", "");
        //    dic.Add("thoi gian truoc khi nhap ngu", "");

        //    dic.Add("quan he xa hoi truoc khi nhap ngu", "");
        //    dic.Add("quan he xa hoi sau khi nhap ngu", "");

        //    dic.Add("ho va ten vo", "");
        //    dic.Add("nam sinh vo", "");
        //    dic.Add("sdt vo", "");
        //    dic.Add("noi o hien nay vo", "");
        //    dic.Add("suc khoe vo", "");
        //    dic.Add("benh ly vo", "");
        //    dic.Add("co con trai", "");
        //    dic.Add("co con gai", "");
        //    dic.Add("tinh hinh suc khoe cua con", "");

        //    dic.Add("ho va ten cha vo", "");
        //    dic.Add("nam sinh cha vo", "");
        //    dic.Add("sdt cha vo", "");
        //    dic.Add("nghe nghiep cha vo", "");
        //    dic.Add("suc khoe cha vo", "");
        //    dic.Add("noi o hien nay cha vo", "");

        //    dic.Add("ho va ten me vo", "");
        //    dic.Add("nam sinh me vo", "");
        //    dic.Add("sdt me vo", "");
        //    dic.Add("nghe nghiep me vo", "");
        //    dic.Add("suc khoe me vo", "");
        //    dic.Add("noi o hien nay me vo", "");

        //    dic.Add("cha me vo sinh duoc", "");
        //    dic.Add("cha me vo sinh duoc trai", "");
        //    dic.Add("cha me vo sinh duoc gai", "");
        //    dic.Add("vo dong chi la con thu", "");


        //    dic.Add("id", currentId.ToString());
        //    #endregion

        //    InfoPerson newInfo = new InfoPerson();
        //    newInfo.infoPerson = new Dictionary<string, object>();
        //    string namePer = "";
        //    foreach (var item in dic)
        //    {
        //        var v = item.Value;
        //        if (namePer == "")
        //        {
        //            List<string> s1 = new List<string>() { "Trần", "Nguyễn", "Phạm", "Phan", "Hồ", "Danh", "Lý", "Lê", "Đinh", "Võ", "Huỳnh", "Trương", "Bùi", "Đặng", "Đỗ", "Ngô", "Dương", "Trịnh" };
        //            List<string> s2 = new List<string>() { "Văn", "Phúc", "Quang", "Hoài", "Anh", "Tùng", "Bá", "Duy", "Việt", "Thanh", "Minh", "Ngọc", "Hồng", };
        //            List<string> s3 = new List<string>() { "Anh", "Khiêm", "Khá", "Khan", "Hiền", "Hòa", "Tố", "Hữu", "Phú", "Quân", "Quang", "Đào", "Bằng", "Việt", "Long", "An", "Ân", "Bảo", "Nhân", "Minh", "Thái", "Cường", "Chu", "Giang", "Hoàng", "Hy", "Khôi", "Khải", "Lâm", "Linh", "Nhã", "Phúc", "Phương" };
        //            string ss1 = s1[Random.Range(0, s1.Count)];
        //            string ss2 = s2[Random.Range(0, s2.Count)];
        //            string ss3 = s3[Random.Range(0, s3.Count)];
        //            while (ss2 == ss3)
        //            {
        //                ss2 = s2[Random.Range(0, s2.Count)];
        //                ss3 = s3[Random.Range(0, s3.Count)];
        //            }
        //            namePer = $"{ss1} {ss2} {ss3}";
        //        }
        //        if (item.Key == "ho va ten")
        //        {
        //            v = namePer;
        //        }
        //        else if (item.Key == "ho va ten dang dung")
        //        {
        //            v = namePer;
        //        }
        //        if (item.Key == "nam sinh")
        //        {
        //            List<string> s1 = new List<string>() { "1998", "1999", "2000", "2001", "2002", "2003", "2004", "2005" };
        //            v = s1[Random.Range(0, s1.Count)];
        //        }
        //        newInfo.infoPerson.Add(item.Key, v);
        //    }
        //    await Manager.Instance.Add(newInfo, false);
        //}
        #endregion
    }

    async void Create(int count, string c)
    {
        for (int i = 0; i < count; i++)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            #region
            dic.Add("benh ly", "Không");
            dic.Add("benh ly ba ngoai", "Không");
            dic.Add("benh ly ba noi", "Không");
            dic.Add("benh ly ong ngoai", "Không");
            dic.Add("benh ly ong noi", "Không");

            dic.Add("benh tat cha", "Không");
            dic.Add("benh tat me", "Không");

            dic.Add("co quan cong tac ba ngoai", "Bạc Liêu");
            dic.Add("co quan cong tac ba noi", "Bạc Liêu");
            dic.Add("co quan cong tac ong ngoai", "Bạc Liêu");
            dic.Add("co quan cong tac ong noi", "Bạc Liêu");

            dic.Add("con song hay da mat cha", "Còn sống");
            dic.Add("con song hay da mat me", "Còn sống");

            dic.Add("dan toc", "Kinh");

            dic.Add("dia chi khi di phep, tranh thu", "Bạc Liêu");

            dic.Add("hien nay cha, me dang o dau", "Ở chung");

            dic.Add("hinh the", "1m70, 55kg");

            dic.Add("ho ten cha", "Nguyễn..");
            dic.Add("ho ten me", "Nguyễn..");


            dic.Add("ho ten ba ngoai", "Nguyễn..");
            dic.Add("ho ten ba noi", "Nguyễn..");
            dic.Add("ho ten ong ngoai", "Nguyễn..");
            dic.Add("ho ten ong noi", "Nguyễn..");

            dic.Add("khi can bao tin cho ai", "cha: Nguyễn Văn A");

            dic.Add("khi chat", "Sôi nổi");

            dic.Add("nam sinh ba ngoai", "1950");
            dic.Add("nam sinh ba noi", "1950");
            dic.Add("nam sinh ong ngoai", "1950");
            dic.Add("nam sinh ong noi", "1950");

            dic.Add("nam sinh cha", "1975");
            dic.Add("nam sinh me", "1975");

            dic.Add("nganh nghe chuyen mon da duoc dao tao qua truong", "Không");

            dic.Add("nganh nghe chuyen mon tu hoc va biet lam", "Không");

            dic.Add("ngay sinh", "1");
            dic.Add("thang sinh", "1");
            dic.Add("nam sinh", "1");

            dic.Add("nguyen quan", "Bạc Liêu");

            dic.Add("noi dang ky ho khau thuong tru", "Bạc Liêu");

            dic.Add("noi o hien nay ba ngoai", "Bạc Liêu");
            dic.Add("noi o hien nay ba noi", "Bạc Liêu");
            dic.Add("noi o hien nay ong ngoai", "Bạc Liêu");
            dic.Add("noi o hien nay ong noi", "Bạc Liêu");

            dic.Add("nghe nghiep cha", "Làm ruộng");
            dic.Add("nghe nghiep me", "Làm ruộng");

            dic.Add("nghe nghiep ba ngoai", "Làm ruộng");
            dic.Add("nghe nghiep ba noi", "Làm ruộng");
            dic.Add("nghe nghiep ong ngoai", "Làm ruộng");
            dic.Add("nghe nghiep ong noi", "Làm ruộng");

            dic.Add("so dien thoai cha", "0900xxxx");
            dic.Add("so dien thoai me", "0900xxxx");

            dic.Add("so dien thoai ba ngoai", "0900xxxx");
            dic.Add("so dien thoai ba noi", "0900xxxx");
            dic.Add("so dien thoai ong ngoai", "0900xxxx");
            dic.Add("so dien thoai ong noi", "0900xxxx");

            dic.Add("suc khoe ba ngoai", "Khỏe mạnh");
            dic.Add("suc khoe ba noi", "Khỏe mạnh");
            dic.Add("suc khoe ong ngoai", "Khỏe mạnh");
            dic.Add("suc khoe ong noi", "Khỏe mạnh");

            dic.Add("tinh trang hon nhan", "Hạnh phúc");

            dic.Add("tinh trang suc khoe cha", "Khỏe mạnh");
            dic.Add("tinh trang suc khoe me", "Khỏe mạnh");

            dic.Add("toggle con song ba ngoai", "true");
            dic.Add("toggle con song ba noi", "true");
            dic.Add("toggle con song ong ngoai", "true");
            dic.Add("toggle con song ong noi", "true");

            dic.Add("toggle da mat ba ngoai", "false");
            dic.Add("toggle da mat ba noi", "false");
            dic.Add("toggle da mat ong ngoai", "false");
            dic.Add("toggle da mat ong noi", "false");

            dic.Add("toggle dang vien ba ngoai", "false");
            dic.Add("toggle dang vien ba noi", "false");
            dic.Add("toggle dang vien ong ngoai", "false");
            dic.Add("toggle dang vien ong noi", "false");

            dic.Add("toggle day la moi truong gian nan vat va", "false");

            dic.Add("toggle day la moi truong tot ren luyen con nguoi", "true");

            dic.Add("toggle di hoc sy quan", "false");
            dic.Add("toggle di hoc sy quan du bi", "false");
            dic.Add("toggle duoc di hoc chuyen mon ky thuat", "false");
            dic.Add("toggle duoc di hoc tieu doi truong, khau doi truong", "false");
            dic.Add("toggle ket nap vao dang", "false");
            dic.Add("toggle ra quan khi het nghia vu", "true");

            dic.Add("ton giao", "Không");

            dic.Add("trinh do hoc van", "12/12");

            dic.Add("don vi", c);

            dic.Add("ho va ten", "");
            dic.Add("ho va ten dang dung", "");

            dic.Add("truoc khi nhap ngu", "Làm thuê ở Tp.Hồ Chí Minh, 1 năm");

            dic.Add("ly do di bo doi", "Rèn luyện bản thân");

            dic.Add("ho va ten anh chi em 1", "Nguyễn Văn..");
            dic.Add("ho va ten anh chi em 2", "Nguyễn Văn..");
            dic.Add("ho va ten anh chi em 3", "Nguyễn Văn..");
            dic.Add("ho va ten anh chi em 4", "Nguyễn Văn..");
            dic.Add("ho va ten anh chi em 5", "Nguyễn Văn..");

            dic.Add("nam sinh anh chi em 1", "2000");
            dic.Add("nam sinh anh chi em 2", "2000");
            dic.Add("nam sinh anh chi em 3", "2000");
            dic.Add("nam sinh anh chi em 4", "2000");
            dic.Add("nam sinh anh chi em 5", "2000");

            dic.Add("nghe nghiep anh chi em 1", "Làm thuê");
            dic.Add("nghe nghiep anh chi em 2", "Làm thuê");
            dic.Add("nghe nghiep anh chi em 3", "Làm thuê");
            dic.Add("nghe nghiep anh chi em 4", "Làm thuê");
            dic.Add("nghe nghiep anh chi em 5", "Làm thuê");

            dic.Add("so dien thoai anh chi em 1", "0900XXXX");
            dic.Add("so dien thoai anh chi em 2", "0900XXXX");
            dic.Add("so dien thoai anh chi em 3", "0900XXXX");
            dic.Add("so dien thoai anh chi em 4", "0900XXXX");
            dic.Add("so dien thoai anh chi em 5", "0900XXXX");

            dic.Add("ho va ten ban gai", "Nguyễn XXX");
            dic.Add("que quan ban gai", "Bạc Liêu");
            dic.Add("nam sinh ban gai", "2000");
            dic.Add("nghe nghiep ban gai", "2000");
            dic.Add("so dien thoai ban gai", "0900XXX");

            dic.Add("ho va ten ban trai", "Nguyễn XXX");
            dic.Add("que quan ban trai", "Bạc Liêu");
            dic.Add("nam sinh ban trai", "2000");
            dic.Add("nghe nghiep ban trai", "2000");
            dic.Add("so dien thoai ban trai", "0900XXX");

            dic.Add("ho va ten ban gai than nhat", "");
            dic.Add("nam sinh ban gai than nhat", "");
            dic.Add("dia chi ban gai than nhat", "");
            dic.Add("so dien thoai ban gai than nhat", "");

            dic.Add("ho va ten ban trai than nhat", "");
            dic.Add("nam sinh ban trai than nhat", "");
            dic.Add("dia chi ban trai than nhat", "");
            dic.Add("so dien thoai ban trai than nhat", "");

            dic.Add("ai la nguoi anh huong tich cuc", "");

            dic.Add("ho va ten can bo dia phuong 1", "");
            dic.Add("chuc vu can bo dia phuong 1", "");
            dic.Add("sdt can bo dia phuong 1", "");

            dic.Add("ho va ten can bo dia phuong 2", "");
            dic.Add("chuc vu can bo dia phuong 2", "");
            dic.Add("sdt can bo dia phuong 2", "");

            dic.Add("ho va ten can bo dia phuong 3", "");
            dic.Add("chuc vu can bo dia phuong 3", "");
            dic.Add("sdt can bo dia phuong 3", "");

            dic.Add("ho va ten can bo dia phuong 4", "");
            dic.Add("chuc vu can bo dia phuong 4", "");
            dic.Add("sdt can bo dia phuong 4", "");

            dic.Add("ho va ten can bo dia phuong 5", "");
            dic.Add("chuc vu can bo dia phuong 5", "");
            dic.Add("sdt can bo dia phuong 5", "");

            dic.Add("lam gi o dau thoi gian truoc khi nhap ngu", "");
            dic.Add("nghe nghiep truoc khi nhap ngu", "");
            dic.Add("noi lam viec truoc khi nhap ngu", "");
            dic.Add("thoi gian truoc khi nhap ngu", "");

            dic.Add("quan he xa hoi truoc khi nhap ngu", "");
            dic.Add("quan he xa hoi sau khi nhap ngu", "");

            dic.Add("ho va ten vo", "");
            dic.Add("nam sinh vo", "");
            dic.Add("sdt vo", "");
            dic.Add("noi o hien nay vo", "");
            dic.Add("suc khoe vo", "");
            dic.Add("benh ly vo", "");
            dic.Add("co con trai", "");
            dic.Add("co con gai", "");
            dic.Add("tinh hinh suc khoe cua con", "");

            dic.Add("ho va ten cha vo", "");
            dic.Add("nam sinh cha vo", "");
            dic.Add("sdt cha vo", "");
            dic.Add("nghe nghiep cha vo", "");
            dic.Add("suc khoe cha vo", "");
            dic.Add("noi o hien nay cha vo", "");

            dic.Add("ho va ten me vo", "");
            dic.Add("nam sinh me vo", "");
            dic.Add("sdt me vo", "");
            dic.Add("nghe nghiep me vo", "");
            dic.Add("suc khoe me vo", "");
            dic.Add("noi o hien nay me vo", "");

            dic.Add("cha me vo sinh duoc", "");
            dic.Add("cha me vo sinh duoc trai", "");
            dic.Add("cha me vo sinh duoc gai", "");
            dic.Add("vo dong chi la con thu", "");


            dic.Add("id", currentId.ToString());
            #endregion

            InfoPerson newInfo = new InfoPerson();
            newInfo.infoPerson = new Dictionary<string, object>();
            string namePer = "";
            foreach (var item in GetRandomDic())
            //foreach (var item in dic)
            {
                var v = item.Value;
                if (namePer == "")
                {
                    List<string> s1 = new List<string>() { "Trần", "Nguyễn", "Phạm", "Phan", "Hồ", "Danh", "Lý", "Lê", "Đinh", "Võ", "Huỳnh", "Trương", "Bùi", "Đặng", "Đỗ", "Ngô", "Dương", "Trịnh" };
                    List<string> s2 = new List<string>() { "Văn", "Phúc", "Quang", "Hoài", "Anh", "Tùng", "Bá", "Duy", "Việt", "Thanh", "Minh", "Ngọc", "Hồng", };
                    List<string> s3 = new List<string>() { "Anh", "Khiêm", "Khá", "Khan", "Hiền", "Hòa", "Tố", "Hữu", "Phú", "Quân", "Quang", "Đào", "Bằng", "Việt", "Long", "An", "Ân", "Bảo", "Nhân", "Minh", "Thái", "Cường", "Chu", "Giang", "Hoàng", "Hy", "Khôi", "Khải", "Lâm", "Linh", "Nhã", "Phúc", "Phương" };
                    string ss1 = s1[Random.Range(0, s1.Count)];
                    string ss2 = s2[Random.Range(0, s2.Count)];
                    string ss3 = s3[Random.Range(0, s3.Count)];
                    while (ss2 == ss3)
                    {
                        ss2 = s2[Random.Range(0, s2.Count)];
                        ss3 = s3[Random.Range(0, s3.Count)];
                    }
                    namePer = $"{ss1} {ss2} {ss3}";
                }
                if (item.Key == "ho va ten")
                {
                    v = namePer;
                }
                else if (item.Key == "ho va ten dang dung")
                {
                    v = namePer;
                }
                if (item.Key == "nam sinh")
                {
                    List<string> s1 = new List<string>() { "1998", "1999", "2000", "2001", "2002", "2003", "2004", "2005" };
                    v = s1[Random.Range(0, s1.Count)];
                }
                newInfo.infoPerson.Add(item.Key, v);
            }

            //string z = "`DROP TABLE [IF EXISTS] table_name`";
            string z = "CREATE TABLE IF NOT EXISTS members (" +
            "id INTEGER PRIMARY KEY AUTOINCREMENT,";
            //string z = "CREATE TABLE IF NOT EXISTS members (";
            int coo = 0;
            foreach (var item in newInfo.infoPerson)
            {
                if (coo >= 176)
                {
                    //break;
                }
                if (item.Key == "id")
                {
                    continue;
                }
                z += $"{item.Key.Replace(" ", "_")},";
                coo++;
            }
            z = z.Substring(0, z.Count() - 1);
            z = z + ")";
            print(coo);
            run(z);

            await Task.Delay(500);

            //z = $"INSERT INTO members VALUES (";
            //foreach (var item in newInfo.infoPerson)
            //{
            //    if (item.Key == "id")
            //    {
            //        continue;
            //    }
            //    z += $"\"{item.Value}\", ";
            //}
            //z = z.Substring(0, z.Count() - 2);
            //z = z + ",'')";
            //run(z);

            //await Manager.Instance.Add(newInfo, false);
        }
    }

    Dictionary<string, string> GetRandomDic()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();

        #region
        dic.Add("benh ly", "Không");
        dic.Add("benh ly ba ngoai", "Không");
        dic.Add("benh ly ba noi", "Không");
        dic.Add("benh ly ong ngoai", "Không");
        dic.Add("benh ly ong noi", "Không");

        dic.Add("benh tat cha", "Không");
        dic.Add("benh tat me", "Không");

        dic.Add("co quan cong tac ba ngoai", "Bạc Liêu");
        dic.Add("co quan cong tac ba noi", "Bạc Liêu");
        dic.Add("co quan cong tac ong ngoai", "Bạc Liêu");
        dic.Add("co quan cong tac ong noi", "Bạc Liêu");

        dic.Add("con song hay da mat cha", "Còn sống");
        dic.Add("con song hay da mat me", "Còn sống");

        dic.Add("dan toc", "Kinh");

        dic.Add("dia chi khi di phep, tranh thu", "Bạc Liêu");

        dic.Add("hien nay cha, me dang o dau", "Ở chung");

        dic.Add("hinh the", "1m70, 55kg");

        dic.Add("ho ten cha", "Nguyễn..");
        dic.Add("ho ten me", "Nguyễn..");


        dic.Add("ho ten ba ngoai", "Nguyễn..");
        dic.Add("ho ten ba noi", "Nguyễn..");
        dic.Add("ho ten ong ngoai", "Nguyễn..");
        dic.Add("ho ten ong noi", "Nguyễn..");

        dic.Add("khi can bao tin cho ai", "cha: Nguyễn Văn A");

        dic.Add("khi chat", "Sôi nổi");

        dic.Add("nam sinh ba ngoai", "1950");
        dic.Add("nam sinh ba noi", "1950");
        dic.Add("nam sinh ong ngoai", "1950");
        dic.Add("nam sinh ong noi", "1950");

        dic.Add("nam sinh cha", "1975");
        dic.Add("nam sinh me", "1975");

        dic.Add("nganh nghe chuyen mon da duoc dao tao qua truong", "Không");

        dic.Add("nganh nghe chuyen mon tu hoc va biet lam", "Không");

        dic.Add("ngay sinh", "1");
        dic.Add("thang sinh", "1");
        dic.Add("nam sinh", "1");

        dic.Add("nguyen quan", "Bạc Liêu");

        dic.Add("noi dang ky ho khau thuong tru", "Bạc Liêu");

        dic.Add("noi o hien nay ba ngoai", "Bạc Liêu");
        dic.Add("noi o hien nay ba noi", "Bạc Liêu");
        dic.Add("noi o hien nay ong ngoai", "Bạc Liêu");
        dic.Add("noi o hien nay ong noi", "Bạc Liêu");

        dic.Add("nghe nghiep cha", "Làm ruộng");
        dic.Add("nghe nghiep me", "Làm ruộng");

        dic.Add("nghe nghiep ba ngoai", "Làm ruộng");
        dic.Add("nghe nghiep ba noi", "Làm ruộng");
        dic.Add("nghe nghiep ong ngoai", "Làm ruộng");
        dic.Add("nghe nghiep ong noi", "Làm ruộng");

        dic.Add("so dien thoai cha", "0900xxxx");
        dic.Add("so dien thoai me", "0900xxxx");

        dic.Add("so dien thoai ba ngoai", "0900xxxx");
        dic.Add("so dien thoai ba noi", "0900xxxx");
        dic.Add("so dien thoai ong ngoai", "0900xxxx");
        dic.Add("so dien thoai ong noi", "0900xxxx");

        dic.Add("suc khoe ba ngoai", "Khỏe mạnh");
        dic.Add("suc khoe ba noi", "Khỏe mạnh");
        dic.Add("suc khoe ong ngoai", "Khỏe mạnh");
        dic.Add("suc khoe ong noi", "Khỏe mạnh");

        dic.Add("tinh trang hon nhan", "Hạnh phúc");

        dic.Add("tinh trang suc khoe cha", "Khỏe mạnh");
        dic.Add("tinh trang suc khoe me", "Khỏe mạnh");

        dic.Add("toggle con song ba ngoai", "true");
        dic.Add("toggle con song ba noi", "true");
        dic.Add("toggle con song ong ngoai", "true");
        dic.Add("toggle con song ong noi", "true");

        dic.Add("toggle da mat ba ngoai", "false");
        dic.Add("toggle da mat ba noi", "false");
        dic.Add("toggle da mat ong ngoai", "false");
        dic.Add("toggle da mat ong noi", "false");

        dic.Add("toggle dang vien ba ngoai", "false");
        dic.Add("toggle dang vien ba noi", "false");
        dic.Add("toggle dang vien ong ngoai", "false");
        dic.Add("toggle dang vien ong noi", "false");

        dic.Add("toggle day la moi truong gian nan vat va", "false");

        dic.Add("toggle day la moi truong tot ren luyen con nguoi", "true");

        dic.Add("toggle di hoc sy quan", "false");
        dic.Add("toggle di hoc sy quan du bi", "false");
        dic.Add("toggle duoc di hoc chuyen mon ky thuat", "false");
        dic.Add("toggle duoc di hoc tieu doi truong, khau doi truong", "false");
        dic.Add("toggle ket nap vao dang", "false");
        dic.Add("toggle ra quan khi het nghia vu", "true");

        dic.Add("ton giao", "Không");

        dic.Add("trinh do hoc van", "12/12");

        dic.Add("don vi", "1");

        dic.Add("ho va ten", "");
        dic.Add("ho va ten dang dung", "");

        dic.Add("truoc khi nhap ngu", "Làm thuê ở Tp.Hồ Chí Minh, 1 năm");

        dic.Add("ly do di bo doi", "Rèn luyện bản thân");

        dic.Add("ho va ten anh chi em 1", "Nguyễn Văn..");
        dic.Add("ho va ten anh chi em 2", "Nguyễn Văn..");
        dic.Add("ho va ten anh chi em 3", "Nguyễn Văn..");
        dic.Add("ho va ten anh chi em 4", "Nguyễn Văn..");
        dic.Add("ho va ten anh chi em 5", "Nguyễn Văn..");

        dic.Add("nam sinh anh chi em 1", "2000");
        dic.Add("nam sinh anh chi em 2", "2000");
        dic.Add("nam sinh anh chi em 3", "2000");
        dic.Add("nam sinh anh chi em 4", "2000");
        dic.Add("nam sinh anh chi em 5", "2000");

        dic.Add("nghe nghiep anh chi em 1", "Làm thuê");
        dic.Add("nghe nghiep anh chi em 2", "Làm thuê");
        dic.Add("nghe nghiep anh chi em 3", "Làm thuê");
        dic.Add("nghe nghiep anh chi em 4", "Làm thuê");
        dic.Add("nghe nghiep anh chi em 5", "Làm thuê");

        dic.Add("so dien thoai anh chi em 1", "0900XXXX");
        dic.Add("so dien thoai anh chi em 2", "0900XXXX");
        dic.Add("so dien thoai anh chi em 3", "0900XXXX");
        dic.Add("so dien thoai anh chi em 4", "0900XXXX");
        dic.Add("so dien thoai anh chi em 5", "0900XXXX");

        dic.Add("ho va ten ban gai", "Nguyễn XXX");
        dic.Add("que quan ban gai", "Bạc Liêu");
        dic.Add("nam sinh ban gai", "2000");
        dic.Add("nghe nghiep ban gai", "2000");
        dic.Add("so dien thoai ban gai", "0900XXX");

        dic.Add("ho va ten ban trai", "Nguyễn XXX");
        dic.Add("que quan ban trai", "Bạc Liêu");
        dic.Add("nam sinh ban trai", "2000");
        dic.Add("nghe nghiep ban trai", "2000");
        dic.Add("so dien thoai ban trai", "0900XXX");

        dic.Add("ho va ten ban gai than nhat", "");
        dic.Add("nam sinh ban gai than nhat", "");
        dic.Add("dia chi ban gai than nhat", "");
        dic.Add("so dien thoai ban gai than nhat", "");

        dic.Add("ho va ten ban trai than nhat", "");
        dic.Add("nam sinh ban trai than nhat", "");
        dic.Add("dia chi ban trai than nhat", "");
        dic.Add("so dien thoai ban trai than nhat", "");

        dic.Add("ai la nguoi anh huong tich cuc", "");

        dic.Add("ho va ten can bo dia phuong 1", "");
        dic.Add("chuc vu can bo dia phuong 1", "");
        dic.Add("sdt can bo dia phuong 1", "");

        dic.Add("ho va ten can bo dia phuong 2", "");
        dic.Add("chuc vu can bo dia phuong 2", "");
        dic.Add("sdt can bo dia phuong 2", "");

        dic.Add("ho va ten can bo dia phuong 3", "");
        dic.Add("chuc vu can bo dia phuong 3", "");
        dic.Add("sdt can bo dia phuong 3", "");

        dic.Add("ho va ten can bo dia phuong 4", "");
        dic.Add("chuc vu can bo dia phuong 4", "");
        dic.Add("sdt can bo dia phuong 4", "");

        dic.Add("ho va ten can bo dia phuong 5", "");
        dic.Add("chuc vu can bo dia phuong 5", "");
        dic.Add("sdt can bo dia phuong 5", "");

        dic.Add("lam gi o dau thoi gian truoc khi nhap ngu", "");
        dic.Add("nghe nghiep truoc khi nhap ngu", "");
        dic.Add("noi lam viec truoc khi nhap ngu", "");
        dic.Add("thoi gian truoc khi nhap ngu", "");

        dic.Add("quan he xa hoi truoc khi nhap ngu", "");
        dic.Add("quan he xa hoi sau khi nhap ngu", "");

        dic.Add("ho va ten vo", "");
        dic.Add("nam sinh vo", "");
        dic.Add("sdt vo", "");
        dic.Add("noi o hien nay vo", "");
        dic.Add("suc khoe vo", "");
        dic.Add("benh ly vo", "");
        dic.Add("co con trai", "");
        dic.Add("co con gai", "");
        dic.Add("tinh hinh suc khoe cua con", "");

        dic.Add("ho va ten cha vo", "");
        dic.Add("nam sinh cha vo", "");
        dic.Add("sdt cha vo", "");
        dic.Add("nghe nghiep cha vo", "");
        dic.Add("suc khoe cha vo", "");
        dic.Add("noi o hien nay cha vo", "");

        dic.Add("ho va ten me vo", "");
        dic.Add("nam sinh me vo", "");
        dic.Add("sdt me vo", "");
        dic.Add("nghe nghiep me vo", "");
        dic.Add("suc khoe me vo", "");
        dic.Add("noi o hien nay me vo", "");

        dic.Add("cha me vo sinh duoc", "");
        dic.Add("cha me vo sinh duoc trai", "");
        dic.Add("cha me vo sinh duoc gai", "");
        dic.Add("vo dong chi la con thu", "");

        dic.Add("id", systemData.currentId);
        int parseCurrentId = int.Parse(systemData.currentId);
        parseCurrentId++;
        systemData.currentId = parseCurrentId.ToString();

        string namePer = "";
        List<string> s1 = new List<string>() { "Trần", "Nguyễn", "Phạm", "Phan", "Hồ", "Danh", "Lý", "Lê", "Đinh", "Võ", "Huỳnh", "Trương", "Bùi", "Đặng", "Đỗ", "Ngô", "Dương", "Trịnh" };
        List<string> s2 = new List<string>() { "Văn", "Phúc", "Quang", "Hoài", "Anh", "Tùng", "Bá", "Duy", "Việt", "Thanh", "Minh", "Ngọc", "Hồng", };
        List<string> s3 = new List<string>() { "Anh", "Khiêm", "Khá", "Khan", "Hiền", "Hòa", "Tố", "Hữu", "Phú", "Quân", "Quang", "Đào", "Bằng", "Việt", "Long", "An", "Ân", "Bảo", "Nhân", "Minh", "Thái", "Cường", "Chu", "Giang", "Hoàng", "Hy", "Khôi", "Khải", "Lâm", "Linh", "Nhã", "Phúc", "Phương" };
        string ss1 = s1[Random.Range(0, s1.Count)];
        string ss2 = s2[Random.Range(0, s2.Count)];
        string ss3 = s3[Random.Range(0, s3.Count)];
        while (ss2 == ss3)
        {
            ss2 = s2[Random.Range(0, s2.Count)];
            ss3 = s3[Random.Range(0, s3.Count)];
        }
        namePer = $"{ss1} {ss2} {ss3}";

        dic["ho va ten"] = namePer;
        dic["ho va ten dang dung"] = namePer;
        List<string> date = new List<string>() { "1998", "1999", "2000", "2001", "2002", "2003", "2004", "2005" };
        dic["nam sinh"] = date[Random.Range(0, date.Count)];

        //dic.Add("id", currentId.ToString());
        #endregion

        return dic;
    }

    async void Insert(int count, string c)
    {
        for (int i = 0; i < count; i++)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            #region
            dic.Add("benh ly", "Không");
            dic.Add("benh ly ba ngoai", "Không");
            dic.Add("benh ly ba noi", "Không");
            dic.Add("benh ly ong ngoai", "Không");
            dic.Add("benh ly ong noi", "Không");

            dic.Add("benh tat cha", "Không");
            dic.Add("benh tat me", "Không");

            dic.Add("co quan cong tac ba ngoai", "Bạc Liêu");
            dic.Add("co quan cong tac ba noi", "Bạc Liêu");
            dic.Add("co quan cong tac ong ngoai", "Bạc Liêu");
            dic.Add("co quan cong tac ong noi", "Bạc Liêu");

            dic.Add("con song hay da mat cha", "Còn sống");
            dic.Add("con song hay da mat me", "Còn sống");

            dic.Add("dan toc", "Kinh");

            dic.Add("dia chi khi di phep, tranh thu", "Bạc Liêu");

            dic.Add("hien nay cha, me dang o dau", "Ở chung");

            dic.Add("hinh the", "1m70, 55kg");

            dic.Add("ho ten cha", "Nguyễn..");
            dic.Add("ho ten me", "Nguyễn..");


            dic.Add("ho ten ba ngoai", "Nguyễn..");
            dic.Add("ho ten ba noi", "Nguyễn..");
            dic.Add("ho ten ong ngoai", "Nguyễn..");
            dic.Add("ho ten ong noi", "Nguyễn..");

            dic.Add("khi can bao tin cho ai", "cha: Nguyễn Văn A");

            dic.Add("khi chat", "Sôi nổi");

            dic.Add("nam sinh ba ngoai", "1950");
            dic.Add("nam sinh ba noi", "1950");
            dic.Add("nam sinh ong ngoai", "1950");
            dic.Add("nam sinh ong noi", "1950");

            dic.Add("nam sinh cha", "1975");
            dic.Add("nam sinh me", "1975");

            dic.Add("nganh nghe chuyen mon da duoc dao tao qua truong", "Không");

            dic.Add("nganh nghe chuyen mon tu hoc va biet lam", "Không");

            dic.Add("ngay sinh", "1");
            dic.Add("thang sinh", "1");
            dic.Add("nam sinh", "1");

            dic.Add("nguyen quan", "Bạc Liêu");

            dic.Add("noi dang ky ho khau thuong tru", "Bạc Liêu");

            dic.Add("noi o hien nay ba ngoai", "Bạc Liêu");
            dic.Add("noi o hien nay ba noi", "Bạc Liêu");
            dic.Add("noi o hien nay ong ngoai", "Bạc Liêu");
            dic.Add("noi o hien nay ong noi", "Bạc Liêu");

            dic.Add("nghe nghiep cha", "Làm ruộng");
            dic.Add("nghe nghiep me", "Làm ruộng");

            dic.Add("nghe nghiep ba ngoai", "Làm ruộng");
            dic.Add("nghe nghiep ba noi", "Làm ruộng");
            dic.Add("nghe nghiep ong ngoai", "Làm ruộng");
            dic.Add("nghe nghiep ong noi", "Làm ruộng");

            dic.Add("so dien thoai cha", "0900xxxx");
            dic.Add("so dien thoai me", "0900xxxx");

            dic.Add("so dien thoai ba ngoai", "0900xxxx");
            dic.Add("so dien thoai ba noi", "0900xxxx");
            dic.Add("so dien thoai ong ngoai", "0900xxxx");
            dic.Add("so dien thoai ong noi", "0900xxxx");

            dic.Add("suc khoe ba ngoai", "Khỏe mạnh");
            dic.Add("suc khoe ba noi", "Khỏe mạnh");
            dic.Add("suc khoe ong ngoai", "Khỏe mạnh");
            dic.Add("suc khoe ong noi", "Khỏe mạnh");

            dic.Add("tinh trang hon nhan", "Hạnh phúc");

            dic.Add("tinh trang suc khoe cha", "Khỏe mạnh");
            dic.Add("tinh trang suc khoe me", "Khỏe mạnh");

            dic.Add("toggle con song ba ngoai", "true");
            dic.Add("toggle con song ba noi", "true");
            dic.Add("toggle con song ong ngoai", "true");
            dic.Add("toggle con song ong noi", "true");

            dic.Add("toggle da mat ba ngoai", "false");
            dic.Add("toggle da mat ba noi", "false");
            dic.Add("toggle da mat ong ngoai", "false");
            dic.Add("toggle da mat ong noi", "false");

            dic.Add("toggle dang vien ba ngoai", "false");
            dic.Add("toggle dang vien ba noi", "false");
            dic.Add("toggle dang vien ong ngoai", "false");
            dic.Add("toggle dang vien ong noi", "false");

            dic.Add("toggle day la moi truong gian nan vat va", "false");

            dic.Add("toggle day la moi truong tot ren luyen con nguoi", "true");

            dic.Add("toggle di hoc sy quan", "false");
            dic.Add("toggle di hoc sy quan du bi", "false");
            dic.Add("toggle duoc di hoc chuyen mon ky thuat", "false");
            dic.Add("toggle duoc di hoc tieu doi truong, khau doi truong", "false");
            dic.Add("toggle ket nap vao dang", "false");
            dic.Add("toggle ra quan khi het nghia vu", "true");

            dic.Add("ton giao", "Không");

            dic.Add("trinh do hoc van", "12/12");

            dic.Add("don vi", c);

            dic.Add("ho va ten", "");
            dic.Add("ho va ten dang dung", "");

            dic.Add("truoc khi nhap ngu", "Làm thuê ở Tp.Hồ Chí Minh, 1 năm");

            dic.Add("ly do di bo doi", "Rèn luyện bản thân");

            dic.Add("ho va ten anh chi em 1", "Nguyễn Văn..");
            dic.Add("ho va ten anh chi em 2", "Nguyễn Văn..");
            dic.Add("ho va ten anh chi em 3", "Nguyễn Văn..");
            dic.Add("ho va ten anh chi em 4", "Nguyễn Văn..");
            dic.Add("ho va ten anh chi em 5", "Nguyễn Văn..");

            dic.Add("nam sinh anh chi em 1", "2000");
            dic.Add("nam sinh anh chi em 2", "2000");
            dic.Add("nam sinh anh chi em 3", "2000");
            dic.Add("nam sinh anh chi em 4", "2000");
            dic.Add("nam sinh anh chi em 5", "2000");

            dic.Add("nghe nghiep anh chi em 1", "Làm thuê");
            dic.Add("nghe nghiep anh chi em 2", "Làm thuê");
            dic.Add("nghe nghiep anh chi em 3", "Làm thuê");
            dic.Add("nghe nghiep anh chi em 4", "Làm thuê");
            dic.Add("nghe nghiep anh chi em 5", "Làm thuê");

            dic.Add("so dien thoai anh chi em 1", "0900XXXX");
            dic.Add("so dien thoai anh chi em 2", "0900XXXX");
            dic.Add("so dien thoai anh chi em 3", "0900XXXX");
            dic.Add("so dien thoai anh chi em 4", "0900XXXX");
            dic.Add("so dien thoai anh chi em 5", "0900XXXX");

            dic.Add("ho va ten ban gai", "Nguyễn XXX");
            dic.Add("que quan ban gai", "Bạc Liêu");
            dic.Add("nam sinh ban gai", "2000");
            dic.Add("nghe nghiep ban gai", "2000");
            dic.Add("so dien thoai ban gai", "0900XXX");

            dic.Add("ho va ten ban trai", "Nguyễn XXX");
            dic.Add("que quan ban trai", "Bạc Liêu");
            dic.Add("nam sinh ban trai", "2000");
            dic.Add("nghe nghiep ban trai", "2000");
            dic.Add("so dien thoai ban trai", "0900XXX");

            dic.Add("ho va ten ban gai than nhat", "");
            dic.Add("nam sinh ban gai than nhat", "");
            dic.Add("dia chi ban gai than nhat", "");
            dic.Add("so dien thoai ban gai than nhat", "");

            dic.Add("ho va ten ban trai than nhat", "");
            dic.Add("nam sinh ban trai than nhat", "");
            dic.Add("dia chi ban trai than nhat", "");
            dic.Add("so dien thoai ban trai than nhat", "");

            dic.Add("ai la nguoi anh huong tich cuc", "");

            dic.Add("ho va ten can bo dia phuong 1", "");
            dic.Add("chuc vu can bo dia phuong 1", "");
            dic.Add("sdt can bo dia phuong 1", "");

            dic.Add("ho va ten can bo dia phuong 2", "");
            dic.Add("chuc vu can bo dia phuong 2", "");
            dic.Add("sdt can bo dia phuong 2", "");

            dic.Add("ho va ten can bo dia phuong 3", "");
            dic.Add("chuc vu can bo dia phuong 3", "");
            dic.Add("sdt can bo dia phuong 3", "");

            dic.Add("ho va ten can bo dia phuong 4", "");
            dic.Add("chuc vu can bo dia phuong 4", "");
            dic.Add("sdt can bo dia phuong 4", "");

            dic.Add("ho va ten can bo dia phuong 5", "");
            dic.Add("chuc vu can bo dia phuong 5", "");
            dic.Add("sdt can bo dia phuong 5", "");

            dic.Add("lam gi o dau thoi gian truoc khi nhap ngu", "");
            dic.Add("nghe nghiep truoc khi nhap ngu", "");
            dic.Add("noi lam viec truoc khi nhap ngu", "");
            dic.Add("thoi gian truoc khi nhap ngu", "");

            dic.Add("quan he xa hoi truoc khi nhap ngu", "");
            dic.Add("quan he xa hoi sau khi nhap ngu", "");

            dic.Add("ho va ten vo", "");
            dic.Add("nam sinh vo", "");
            dic.Add("sdt vo", "");
            dic.Add("noi o hien nay vo", "");
            dic.Add("suc khoe vo", "");
            dic.Add("benh ly vo", "");
            dic.Add("co con trai", "");
            dic.Add("co con gai", "");
            dic.Add("tinh hinh suc khoe cua con", "");

            dic.Add("ho va ten cha vo", "");
            dic.Add("nam sinh cha vo", "");
            dic.Add("sdt cha vo", "");
            dic.Add("nghe nghiep cha vo", "");
            dic.Add("suc khoe cha vo", "");
            dic.Add("noi o hien nay cha vo", "");

            dic.Add("ho va ten me vo", "");
            dic.Add("nam sinh me vo", "");
            dic.Add("sdt me vo", "");
            dic.Add("nghe nghiep me vo", "");
            dic.Add("suc khoe me vo", "");
            dic.Add("noi o hien nay me vo", "");

            dic.Add("cha me vo sinh duoc", "");
            dic.Add("cha me vo sinh duoc trai", "");
            dic.Add("cha me vo sinh duoc gai", "");
            dic.Add("vo dong chi la con thu", "");


            dic.Add("id", currentId.ToString());
            #endregion

            InfoPerson newInfo = new InfoPerson();
            newInfo.infoPerson = new Dictionary<string, object>();
            string namePer = "";
            foreach (var item in GetRandomDic())
            //foreach (var item in dic)
            {
                var v = item.Value;
                if (namePer == "")
                {
                    List<string> s1 = new List<string>() { "Trần", "Nguyễn", "Phạm", "Phan", "Hồ", "Danh", "Lý", "Lê", "Đinh", "Võ", "Huỳnh", "Trương", "Bùi", "Đặng", "Đỗ", "Ngô", "Dương", "Trịnh" };
                    List<string> s2 = new List<string>() { "Văn", "Phúc", "Quang", "Hoài", "Anh", "Tùng", "Bá", "Duy", "Việt", "Thanh", "Minh", "Ngọc", "Hồng", };
                    List<string> s3 = new List<string>() { "Anh", "Khiêm", "Khá", "Khan", "Hiền", "Hòa", "Tố", "Hữu", "Phú", "Quân", "Quang", "Đào", "Bằng", "Việt", "Long", "An", "Ân", "Bảo", "Nhân", "Minh", "Thái", "Cường", "Chu", "Giang", "Hoàng", "Hy", "Khôi", "Khải", "Lâm", "Linh", "Nhã", "Phúc", "Phương" };
                    string ss1 = s1[Random.Range(0, s1.Count)];
                    string ss2 = s2[Random.Range(0, s2.Count)];
                    string ss3 = s3[Random.Range(0, s3.Count)];
                    while (ss2 == ss3)
                    {
                        ss2 = s2[Random.Range(0, s2.Count)];
                        ss3 = s3[Random.Range(0, s3.Count)];
                    }
                    namePer = $"{ss1} {ss2} {ss3}";
                }
                if (item.Key == "ho va ten")
                {
                    v = namePer;
                }
                else if (item.Key == "ho va ten dang dung")
                {
                    v = namePer;
                }
                if (item.Key == "nam sinh")
                {
                    List<string> s1 = new List<string>() { "1998", "1999", "2000", "2001", "2002", "2003", "2004", "2005" };
                    v = s1[Random.Range(0, s1.Count)];
                }
                newInfo.infoPerson.Add(item.Key, v);
            }

            //string z = "`DROP TABLE [IF EXISTS] table_name`";
            //string z = "CREATE TABLE IF NOT EXISTS members (" +
            //    "id INTEGER PRIMARY KEY AUTOINCREMENT";
            //string z = "CREATE TABLE IF NOT EXISTS members (";
            //foreach (var item in newInfo.infoPerson)
            //{
            //    if (item.Key == "id")
            //    {
            //        continue;
            //    }
            //    z += $"{item.Key.Replace(" ", "_")},";
            //}
            //z = z.Substring(0, z.Count() - 1);
            //z = z + ")";
            //run(z);

            //await Task.Delay(500);

            string z = $"INSERT INTO members VALUES (NULL, ";
            int coo = 0;
            foreach (var item in newInfo.infoPerson)
            {
                if (coo >= 176)
                {
                    //break;
                }
                if (item.Key == "id")
                {
                    continue;
                }
                z += $"\'{item.Value.ToString().Replace(" ", "_")}',";
                coo++;
            }
            //z += "\'\',";
            //z += "\'\',";
            //z += "\'\',";
            z = z.Substring(0, z.Count() - 1);
            z = z + ")";
            run(z);

            //await Manager.Instance.Add(newInfo, false);
        }
    }

    async void GetInfo()
    {
        //run("SELECT * FROM 'users'");
        run("SELECT * FROM 'members'");
        //print(s.Result);
    }

    public void Add(InfoMember infoMember)
    {
        infoMembers.members.Add(infoMember);
        Data.Save(path1 + "\\members.txt", infoMembers);
        ReloadAllInfo();
    }

    public async Task Add(InfoPerson infoPerson, bool reload = true)
    {
        //GameObject loading = Instantiate(Resources.Load("Loading Panel") as GameObject, GameObject.Find("Canvas").transform);

        //string newId = $"{infoPerson.infoPerson["ten"]}:{infoPerson.infoPerson["don vi"]}:{infoPerson.infoPerson["nam sinh"]}";
        //DocumentReference docRef = db.Collection("ho so").Document(newId);

        #region use LAN

        #endregion
        string json = JsonUtility.ToJson(infoPerson);
        print(json);
        #region use gg

        //if (!infoPerson.infoPerson.ContainsKey("id"))
        //{
        //    infoPerson.infoPerson.Add("id", currentId.ToString());
        //}

        //DocumentReference docRef = db.Collection("ho so").Document(infoPerson.infoPerson.GetValueOrDefault("id").ToString());
        //await docRef.SetAsync(infoPerson.infoPerson).ContinueWithOnMainThread(task =>
        //{
        //    if (task.IsCompleted)
        //    {
        //        Debug.Log($"Document {infoPerson.infoPerson.GetValueOrDefault("id").ToString()} được thêm thành công!");
        //        NotificationManager.CreateNoti($"Hồ sơ {infoPerson.infoPerson.GetValueOrDefault("id").ToString()} đã được thêm thành công.");
        //    }
        //    else
        //    {
        //        Debug.LogError("Có lỗi xảy ra khi thêm document.");
        //        NotificationManager.CreateNoti("Có lỗi xảy ra khi thêm hồ sơ.");
        //    }
        //});

        //Destroy(loading);

        //currentId++;
        //DocumentReference colSystem = db.Collection("system").Document("data");
        //await colSystem.UpdateAsync("current id", currentId.ToString()).ContinueWithOnMainThread(task =>
        //{
        //    if (task.IsCompleted)
        //    {
        //        Debug.Log("Đã update current id");
        //    }
        //    else
        //    {
        //        Debug.LogError("Không thể update current id");
        //    }
        //});
        #endregion


        if (reload)
        {
            ReloadAllInfo();
        }
    }

    public async void Delete(InfoMember infoMember)
    {
        #region use LAN
        Manager.Instance.infoMembers.members.Remove(infoMember);
        Data.Save(path1 + "\\members.txt", infoMembers);
        #endregion
        ReloadAllInfo();
    }

    public async void Delete(string s)
    {
        #region use gg
        //GameObject loading = Instantiate(Resources.Load("Loading Panel") as GameObject, GameObject.Find("Canvas").transform);

        //await db.Collection("ho so").Document(s).DeleteAsync().ContinueWithOnMainThread(task =>
        //{
        //    if (task.IsCompleted)
        //    {
        //        Debug.Log($"Tài liệu {s} đã bị xóa.");
        //        NotificationManager.CreateNoti($"Hồ sơ {s} đã bị xóa.");
        //    }
        //    else
        //    {
        //        Debug.LogError("Có lỗi xảy ra khi xóa tài liệu: " + task.Exception);
        //        NotificationManager.CreateNoti("Có lỗi xảy ra khi xóa hồ sơ.");
        //    }
        //});

        //Destroy(loading);
        #endregion

        ReloadAllInfo();
    }
}
