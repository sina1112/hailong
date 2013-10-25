using System;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data.SqlTypes;

namespace Com.HaiLong.Lib.Common
{
    /// <summary>
    /// 页面数据校验类
    /// Copyright (C) Com.HaiLong 2004-2011
    /// </summary>
    public class PageValidate
    {
        private static Regex RegPhone = new Regex("^[0-9]+[-]?[0-9]+[-]?[0-9]$");
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //等价于^[+-]?\d+[.]?\d+$
        private static Regex RegEmail = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
        private static Regex RegCHZN = new Regex(@"^[\u4e00-\u9fa5]+$");
        private static Regex RegTel = new Regex("^\\(0\\d{2}\\)[- ]?\\d{7,8}$|^0\\d{2}[- ]?\\d{7,8}$|^\\(0\\d{3}\\)[- ]?\\d{7,8}$|^0\\d{3}[- ]?\\d{7,8}$|^\\d{11}$");//匹配11位手机号码及匹配3位或4位区号的电话号码，其中区号可以用小括号括起来,也可以不用，区号与本地号间可以用连字号或空格间隔，也可以没有间隔
        private static Regex RegNumberAndEn = new Regex("^[a-zA-Z0-9]+$");
        private static Regex RegPostCode = new Regex(@"^\d{6}$");
        /// <summary>
        /// 手机号码正则
        /// </summary>
        private static Regex RegHandPhone = new Regex("^1[3|4|5|8]\\d{9}$");

        #region 数字字符串检查
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsPhone(string inputData)
        {
            Match m = RegPhone.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 检查Request查询字符串的键值，是否是数字，最大长度限制
        /// </summary>
        /// <param name="req">Request</param>
        /// <param name="inputKey">Request的键值</param>
        /// <param name="maxLen">最大长度</param>
        /// <returns>返回Request查询字符串</returns>
        public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
        {
            string retVal = string.Empty;
            if (inputKey != null && inputKey != string.Empty)
            {
                retVal = req.QueryString[inputKey];
                if (null == retVal)
                    retVal = req.Form[inputKey];
                if (null != retVal)
                {
                    retVal = SqlText(retVal, maxLen);
                    if (!IsNumber(retVal))
                        retVal = string.Empty;
                }
            }
            if (retVal == null)
                retVal = string.Empty;
            return retVal;
        }
        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            Match m = RegNumber.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否数字字符串 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            Match m = RegDecimal.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 是否只有数字和字母组合
        /// <summary>
        /// 检测是否只有数字和字母组合
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsOnlyNumberAndEn(string inputData)
        {
            Match m = RegNumberAndEn.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 中文检测
        /// <summary>
        /// 检测是否中文字符
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsCHZN(string inputData)
        {
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 邮件地址
        /// <summary>
        /// 是否是邮件地址
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string inputData)
        {
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 电话号码验证
        /// <summary>
        /// 电话号码验证
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsTel(string inputData)
        {
            Match m = RegTel.Match(inputData);
            return m.Success;

        }
        #endregion

        #region 手机号码验证
        /// <summary>
        /// 手机号码验证
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsHandPhone(string inputData)
        {
            Match match = RegHandPhone.Match(inputData);
            return match.Success;
        }
        #endregion

        #region 日期格式判断
        /// <summary>
        /// 日期格式字符串判断
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(string str)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    DateTime.Parse(str);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 格式化时间，转换为SqlDateTime 允许的时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime FormatDateTime(DateTime dt)
        {
            if (dt <= SqlDateTime.MinValue.Value)
                return SqlDateTime.MinValue.Value;
            else if (dt >= SqlDateTime.MaxValue.Value)
                return SqlDateTime.MaxValue.Value;
            else return dt;
        }
        #endregion

        #region  邮编验证
        /// <summary>
        /// 邮编验证
        /// </summary>
        /// <param name="str_postalcode"></param>
        /// <returns></returns>
        public static bool IsPostalcode(string postalcode)
        {

            Match m = RegPostCode.Match(postalcode);
           return m.Success;
        }
        #endregion

        #region 其他
        /// <summary>
        /// 检查字符串最大长度，返回指定长度的串
        /// </summary>
        /// <param name="sqlInput">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>			
        public static string SqlText(string sqlInput, int maxLength)
        {
            if (sqlInput != null && sqlInput != string.Empty)
            {
                sqlInput = sqlInput.Trim();
                if (sqlInput.Length > maxLength)//按最大长度截取字符串
                    sqlInput = sqlInput.Substring(0, maxLength);
            }
            return sqlInput;
        }
        /// <summary>
        /// 字符串编码
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string HtmlEncode(string inputData)
        {
            return HttpUtility.HtmlEncode(inputData);
        }
        /// <summary>
        /// 设置Label显示Encode的字符串
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="txtInput"></param>
        public static void SetLabel(Label lbl, string txtInput)
        {
            lbl.Text = HtmlEncode(txtInput);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="inputObj"></param>
        public static void SetLabel(Label lbl, object inputObj)
        {
            SetLabel(lbl, inputObj.ToString());
        }
        /// <summary>
        /// 字符串清理
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string InputText(string inputString, int maxLength)
        {
            StringBuilder retVal = new StringBuilder();

            // 检查是否为空
            if ((inputString != null) && (inputString != String.Empty))
            {
                inputString = inputString.Trim();

                //检查长度
                if (inputString.Length > maxLength)
                    inputString = inputString.Substring(0, maxLength);

                //替换危险字符
                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }
                retVal.Replace("'", " ");// 替换单引号
            }
            return retVal.ToString();

        }
        /// <summary>
        /// 转换成 HTML code
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string Encode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }
        /// <summary>
        /// 解析html成 普通文本
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string Decode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }
        /// <summary>
        /// 去除特殊符号
        /// </summary>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public static string SqlTextClear(string sqlText)
        {
            if (sqlText == null)
            {
                return null;
            }
            if (sqlText == "")
            {
                return "";
            }
            sqlText = sqlText.Replace("<", "");//去除<
            sqlText = sqlText.Replace(">", "");//去除>
            sqlText = sqlText.Replace("--", "");//去除--
            sqlText = sqlText.Replace("'", "");//去除'
            sqlText = sqlText.Replace("\"", "");//去除"
            sqlText = sqlText.Replace("=", "");//去除=
            sqlText = sqlText.Replace("%", "");//去除%
            sqlText = sqlText.Replace(" ", "");//去除空格
            return sqlText;
        }
        #endregion

        #region 是否由特定字符组成
        /// <summary>
        /// 是否由特定字符组成
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static bool isContainSameChar(string strInput)
        {
            string charInput = string.Empty;
            if (!string.IsNullOrWhiteSpace(strInput))
            {
                charInput = strInput.Substring(0, 1);
            }
            return isContainSameChar(strInput, charInput, strInput.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="charInput"></param>
        /// <param name="lenInput"></param>
        /// <returns></returns>
        public static bool isContainSameChar(string strInput, string charInput, int lenInput)
        {
            if (string.IsNullOrWhiteSpace(charInput))
            {
                return false;
            }
            else
            {
                Regex RegNumber = new Regex(string.Format("^([{0}])+$", charInput));
                Match m = RegNumber.Match(strInput);
                return m.Success;
            }
        }
        #endregion

        #region 检查输入的参数是不是某些定义好的特殊字符：这个方法目前用于密码输入的安全检查
        /// <summary>
        /// 检查输入的参数是不是某些定义好的特殊字符：这个方法目前用于密码输入的安全检查
        /// </summary>
        public static bool isContainSpecChar(string strInput)
        {
            string[] list = new string[] { "123456", "654321" };
            bool result = new bool();
            for (int i = 0; i < list.Length; i++)
            {
                if (strInput == list[i])
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        #endregion

        #region 验证是不是正常字符 字母，数字，下划线的组合
        /// <summary>
        /// 验证是不是正常字符 字母，数字，下划线的组合
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNormalChar(string source)
        {
            return Regex.IsMatch(source, @"[\w\d_]+", RegexOptions.IgnoreCase);
        }
        #endregion

        #region 验证身份证是否有效
        /// <summary>
        /// 验证身份证是否有效
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool IsIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                bool check = IsIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = IsIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 18位身份证验证
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool IsIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }
        /// <summary>
        /// 15位身份证验证
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool IsIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }
        #endregion

        #region 是否是详细地址
        /// <summary>
        /// 是否是详细地址
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static bool IsAddress(string Address)
        {
            bool Flag = false;
            if (Address.Contains("北京市") || Address.Contains("上海市") || Address.Contains("天津市") || Address.Contains("重庆市"))
            {
                if (Address.Contains("市") && (Address.Contains("县") || Address.Contains("区")))
                    Flag = true;
            }
            else if (Address.Contains("自治区") || Address.Contains("自治州") || Address.Contains("特别行政区"))
            {
                Flag = true;
            }
            else
            {
                if (Address.Contains("省") && Address.Contains("市") && (Address.Contains("县") || Address.Contains("市") || Address.Contains("地区") || Address.Contains("旗") || Address.Contains("区")))
                    Flag = true;
            }
            return Flag;
        }
        #endregion
    }
}