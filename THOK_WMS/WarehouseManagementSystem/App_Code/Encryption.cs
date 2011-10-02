/****************************************************** 
FileName:Encryption
Copyright (c) 2004-2007 天海欧康科技信息（厦门）有限公司技术开发部
Writer:施建新

create Date:2007/10/24
Rewriter:施建新

Rewrite Date:2007/10/24
Impact:
Main Content（Function Name、parameters、returns）

******************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;

/// <summary>
/// 加密解密类

/// </summary>
public class Encryption
{
    private byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };//默认密钥向量
    //密钥
    //获取或设置对称算法的机密密钥。机密密钥既用于加密，也用于解密。为了保证对称算法的安全，必须只有发送方和接收方知道该机密密钥。有效密钥大小是由特定对称算法实现指定的，密钥大小在 LegalKeySizes 中列出。

    private static byte[] DESKey = new byte[] { 11, 23, 93, 102, 72, 41, 18, 12 };
    //获取或设置对称算法的初始化向量

    private static byte[] DESIV = new byte[] { 75, 158, 46, 97, 78, 57, 17, 36 };

    string _strEncryptString;
    string _strDecryptString;
    string _strEncryptDecryptKey;

	public Encryption()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 待加密字符

    /// </summary>
    public string EncryptString
    {
        get
        {
            return _strEncryptString;
        }
        set
        {
            if (value is string)
            {
                _strEncryptString = value;
            }
        }
    }
    /// <summary>
    /// 待解密字符

    /// </summary>
    public string DecryptString
    {
        get
        {
            return _strDecryptString;
        }
        set
        {
            if (value is string)
            {
                _strDecryptString = value;
            }
        }
    }
    /// <summary>
    /// 加密解密Key(要求为8位)
    /// </summary>
    public string EncryptDecryptKey
    {
        get
        {
            return _strEncryptDecryptKey;
        }
        set
        {
            if (value is string)
            {
                _strEncryptDecryptKey = value;
            }
        }
    }
    /// <summary>
    /// DES加密字符串

    /// </summary>
    /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
    public string EncryptDES()
    {
        try
        {
            byte[] btRgbKey = Encoding.UTF8.GetBytes(EncryptDecryptKey.Substring(0, 8));
            byte[] btRgbIV = Keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(EncryptString);
            DESCryptoServiceProvider dCSPEncryption = new DESCryptoServiceProvider();
            MemoryStream mStreamEncryption = new MemoryStream();
            CryptoStream cStreamEncryption = new CryptoStream(mStreamEncryption, dCSPEncryption.CreateEncryptor(btRgbKey, btRgbIV), CryptoStreamMode.Write);
            cStreamEncryption.Write(inputByteArray, 0, inputByteArray.Length);
            cStreamEncryption.FlushFinalBlock();
            return Convert.ToBase64String(mStreamEncryption.ToArray());
        }
        catch
        {
            return EncryptString;
        }
    }
    /// <summary>
    /// DES解密字符串

    /// </summary>
    /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
    public string DecryptDES()
    {
        try
        {
            byte[] btRgbKey = Encoding.UTF8.GetBytes(EncryptDecryptKey);
            byte[] btRgbIV = Keys;
            byte[] outputByteArray = Convert.FromBase64String(DecryptString);
            DESCryptoServiceProvider dCSPDecryption = new DESCryptoServiceProvider();
            MemoryStream mStreamDecryption = new MemoryStream();
            CryptoStream cStreamDecryption = new CryptoStream(mStreamDecryption, dCSPDecryption.CreateDecryptor(btRgbKey, btRgbIV), CryptoStreamMode.Write);
            cStreamDecryption.Write(outputByteArray, 0, outputByteArray.Length);
            cStreamDecryption.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStreamDecryption.ToArray());
        }
        catch
        {
            return DecryptString;
        }
    }
    /// <summary>
    /// MD5加密
    /// </summary>
    /// <returns>加密后的字符</returns>
    public string EncryptMD5()
    {
        string strEncryptMD5 = "";
        MD5 md5Object = MD5.Create();
        // 加密后是一个字节类型的数组 
        byte[] btEncryptMD5 = md5Object.ComputeHash(Encoding.Unicode.GetBytes(EncryptString));
        // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得 
        for (int i = 0; i < btEncryptMD5.Length; i++)
        {
            // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
            strEncryptMD5 = strEncryptMD5 + btEncryptMD5[i].ToString("x");
        }
        return strEncryptMD5;
    }
}
