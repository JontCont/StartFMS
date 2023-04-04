using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartFMS.Extensions.Data {
    public static class ObjectConvert {
        public static Decimal ToDecimal(this String str) {
            return decimal.TryParse(str, out decimal result) ? result : 0;
        }
        public static DateTime ToDateTime(this String str) {
            return DateTime.TryParse(str, out DateTime result) ? result : new DateTime();
        }

        /// <summary>
        /// 字串轉換日期格式
        /// </summary>
        /// <param name="str"></param>
        /// <param name="format">預設 yyyy-MM-dd HH:mm:ss</param>
        /// <returns></returns>
        public static string ToDateTimeFormat(this String str, string format = "yyyy-MM-dd HH:mm:ss") {
            return DateTime.TryParse(str, out DateTime result) ? result.ToString(format) : "";
        }

        public static Decimal ToDecimal(this Object str) {
            return str != null && decimal.TryParse(str.ToString(), out decimal result) ? result : 0;
        }
        public static Boolean ToBoolean(this Object str) {
            return Boolean.TryParse(str.ToString(), out Boolean result) && result;
        }

        public static int ToInt(this Object str) {
            return int.TryParse(str.ToString(), out int result) ? result : 0;
        }

        public static Int32 ToInt32(this Object str) {
            return Int32.TryParse(str.ToString(), out Int32 result) ? result : 0;
        }

        public static Int64 ToInt64(this Object str) {
            return Int64.TryParse(str.ToString(), out Int64 result) ? result : 0;
        }
        public static Double ToDouble(this Object str) {
            return Double.TryParse(str.ToString(), out Double result) ? result : 0;
        }

        /// <summary>
        /// 轉換小數點
        /// </summary>
        /// <param name="str"></param>
        /// <param name="digits">預設小數點4位</param>
        /// <returns></returns>
        public static string ToDecimalPlaces(this string str, int digits = 4) {
            if (!str.Contains('.')) { str += ".0000"; }
            if (!decimal.TryParse(str, out decimal oDecimal)) { return str; }
            decimal before = decimal.Parse(str);
            return decimal.Round(before, digits).ToString();
        }

        /// <summary>
        /// 千分位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToThousandths(this string str) {
            if (!decimal.TryParse(str, out decimal oDecimal)) { return str; }
            decimal before = decimal.Parse(str);
            return String.Format("{0:N}", before);
        }

        /// <summary>
        /// 轉換數字 (Ex : 0001)
        /// </summary>
        /// <param name="number"></param>
        /// <param name="digits">預設4位數</param>
        /// <returns></returns>
        public static string ToNumber(this string str, int digits = 4) {
            string nowNumber = str;
            string template = "";
            int len = nowNumber.Length;
            if (len >= digits) { return nowNumber; }
            for (int index = 1; index <= (digits - len); index++) {
                template += "0";
            }
            return template + nowNumber;
        }

        /// <summary>
        /// 自動累加數值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="digits">預設4位數</param>
        /// <returns></returns>
        public static string ToAutoNumber(this string str, int digits = 4) {
            string nowNumber = (str.ToInt() + 1).ToString();
            string template = "";
            int len = nowNumber.Length;
            if (len >= digits) { return nowNumber; }
            for (int index = 1; index <= (digits - len); index++) {
                template += "0";
            }
            return template + nowNumber;
        }

        /// <summary>
        /// 第一個英文字首大寫
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToCapitalizeFirstLetter(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return input.Substring(0, 1).ToUpper() + input.Substring(1);
        }
    }
}
