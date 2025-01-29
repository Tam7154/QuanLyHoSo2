using NPOI.XWPF.UserModel;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Windows.Forms;
using Application = UnityEngine.Application;
using UnityEngine.UI;

public class InfoQuanNhan : MonoBehaviour
{
    public byte[] imgClipboard;
    public Image img;
    public void Export(SlotInTongHopQuanNhan curSelect)
    {

        string originalFilePath = $"{Application.dataPath}\\StreamingAssets\\temp.docx";
        string copyFilePath = $"{Application.dataPath}\\StreamingAssets\\{curSelect.infoPerson.infoPerson["ho va ten"]}.docx";

        File.Copy(originalFilePath, copyFilePath, true);

        FileStream stream = new FileStream(copyFilePath, FileMode.OpenOrCreate);

        XWPFDocument doc = new XWPFDocument(stream);

        foreach (var paragraph in doc.Paragraphs)
        {
            string text = paragraph.ParagraphText;
            foreach (var item in curSelect.infoPerson.infoPerson)
            {
                if (text.Contains($"*{item.Key}"))
                {
                    string updatedText = text.Replace($"*{item.Key}", item.Value.ToString());

                    //double fontSize = 14;
                    for (int i = 0; i < paragraph.Runs.Count; i++)
                    {
                        paragraph.Runs[i].SetText("", 0);
                        //paragraph.Runs[i].FontSize
                    }
                    //paragraph.Runs[i].SetText("", 0);
                    paragraph.CreateRun().SetText(updatedText);
                    for (int i = 0; i < paragraph.Runs.Count; i++)
                    {
                        paragraph.Runs[i].SetText("", 0);
                        //paragraph.Runs[i].FontSize
                    }
                    //paragraph.CreateRun().SetText(updatedText);
                }
            }
        }


        FileStream writeStream = new FileStream(copyFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        doc.Write(writeStream);
        print("End");

        stream.Dispose();
        writeStream.Dispose();
    }

    public void AddImage()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image files (*.png)|*.png|All files (*.*)|*.*";
        openFileDialog.Title = "Chọn một file";
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            LoadImageToTexture(openFileDialog.FileName);
        }
    }

    private void LoadImageToTexture(string path)
    {
        // Đọc dữ liệu từ file hình ảnh
        byte[] imageBytes = System.IO.File.ReadAllBytes(path);
        string nameImg = Path.GetFileName(path);
        string newPath = Application.streamingAssetsPath + "/" + nameImg;

        imgClipboard = imageBytes;
        Texture2D txt = new Texture2D(3, 2);
        txt.LoadImage(imageBytes);
        //img.sprite = Sprite.Create(txt,new Rect(0, 0, txt.width, txt.height),new Vector2(txt.width/2, txt.height/2));
        img.transform.GetChild(0).gameObject.SetActive(true);
        img.transform.GetChild(1).gameObject.SetActive(true);
        img.transform.GetChild(1).GetComponent<Image>().color = new Color(1,1,1,1);
        img.transform.GetChild(1).GetComponent<Image>().sprite = Sprite.Create(txt,new Rect(0, 0, txt.width, txt.height),new Vector2(txt.width/2, txt.height/2));
        img.transform.GetChild(2).gameObject.SetActive(false);

        //File.WriteAllBytes(newPath, imageBytes);

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
}
