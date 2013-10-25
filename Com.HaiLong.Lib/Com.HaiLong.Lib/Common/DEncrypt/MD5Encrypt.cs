using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Com.HaiLong.Lib.Common.DEncrypt
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public class MD5Encrypt
    {
        /// <summary>
        /// 获取MD5值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5(string str)
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(str);
            Byte[] hashedBytes = MD5.Create().ComputeHash(clearBytes);

            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }
    }
}
