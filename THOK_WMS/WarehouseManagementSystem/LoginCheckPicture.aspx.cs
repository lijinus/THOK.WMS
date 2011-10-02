/****************************************************** 
FileName:DataClass
Copyright (c) 2004-xxxx *********公司技术开发部
Writer:
create Date:2007/08/15
Rewriter:
Rewrite Date:
Impact:
Main Content（Function Name、parameters、returns）



******************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

///<summary>
///Module ID：<模块编号，可以引用系统设计中的模块编号>
///Depiction：生成验证码模块
///Author：施建新
///Create Date：2007-08-15
///</summary> 
public partial class LoginCheckPicture : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CreateCheckCodeImage(GenerateCheckCode());
    }

    #region 随机产生数字或字母



    private string GenerateCheckCode()
    {
        int intCheckCodeNumber;
        int iCode;
        string strCheckCode = String.Empty;

        System.Random random = new Random();
        
        for (int i = 0; i < 4; i++)
        {
            intCheckCodeNumber = random.Next();

            //if (intCheckCodeNumber % 2 == 0)
            //    chrCode = (char)('0' + (char)(intCheckCodeNumber % 10));
            //else
            //    chrCode = (char)('A' + (char)(intCheckCodeNumber % 26));
            iCode = intCheckCodeNumber % 10;
            strCheckCode += iCode.ToString();
        }

        //Response.Cookies.Add(new HttpCookie("CheckCode", strCheckCode));
        Session["CheckCode"] = strCheckCode;
        return strCheckCode;
    }
    #endregion

    #region 对背景设置



    private void CreateCheckCodeImage(string strCheckCode)
    {
        if (strCheckCode == null || strCheckCode.Trim() == String.Empty)
            return;

        System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((strCheckCode.Length * 12.5)), 22);
        Graphics g = Graphics.FromImage(image);

        try
        {
            //生成随机生成器



            Random random = new Random();

            //清空图片背景色



            g.Clear(Color.Azure);

            ////画图片的背景噪音线



            //for (int i = 0; i < 25; i++)
            //{
            //    int x1 = random.Next(image.Width);
            //    int x2 = random.Next(image.Width);
            //    int y1 = random.Next(image.Height);
            //    int y2 = random.Next(image.Height);

            //    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            //}

            Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
            g.DrawString(strCheckCode, font, brush, 2, 2);

            ////画图片的前景噪音点



            //for (int i = 0; i < 100; i++)
            //{
            //    int x = random.Next(image.Width);
            //    int y = random.Next(image.Height);

            //    image.SetPixel(x, y, Color.FromArgb(random.Next()));
            //}

            //画图片的边框线



            g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms.ToArray());
        }
        finally
        {
            g.Dispose();
            image.Dispose();
         }
    }
    #endregion
}
