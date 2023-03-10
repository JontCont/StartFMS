using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartFMS.Extensions.Data {
    internal static class ObjectConvert {
        public static Decimal ToDecimal(this String str) {
            return decimal.TryParse(str, out decimal result) ? result : 0;
        }
        public static DateTime ToDateTime(this String str) {
            return DateTime.TryParse(str, out DateTime result) ? result : new DateTime();
        }

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
        public static string ToDecimalPlaces(this string str, int digits = 4) {
            if (!str.Contains('.')) { str += ".0000"; }
            if (!decimal.TryParse(str, out decimal oDecimal)) { return str; }
            decimal before = decimal.Parse(str);
            return decimal.Round(before, digits).ToString();
        }

        public static string ToThousandths(this string str) {
            if (!decimal.TryParse(str, out decimal oDecimal)) { return str; }
            decimal before = decimal.Parse(str);
            return String.Format("{0:N}", before);
        }

        public static string ToStringNumber(this int number, int digits = 4) {
            string nowNumber = (number).ToString();
            string template = "";
            int len = nowNumber.Length;
            if (len >= digits) { return nowNumber; }
            for (int index = 1; index <= (digits - len); index++) {
                template += "0";
            }
            return template + nowNumber;
        }
        public static string ToStringAutoNumber(this int number, int digits = 4) {
            string nowNumber = (number + 1).ToString();
            string template = "";
            int len = nowNumber.Length;
            if (len >= digits) { return nowNumber; }
            for (int index = 1; index <= (digits - len); index++) {
                template += "0";
            }
            return template + nowNumber;
        }
    }
}
