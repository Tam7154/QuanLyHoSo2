using NPOI.XWPF.UserModel;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InfoQuanNhan : MonoBehaviour
{
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
}
