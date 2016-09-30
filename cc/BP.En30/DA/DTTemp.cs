using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using System.IO;
using BP.Sys;
namespace BP.DA
{
    public class DTTemp
    {
        /// <summary>
        /// ʹ��C#�ѷ����ʱ���Ϊ������,����ǰ,��Сʱǰ,������ǰ,����ǰ
        ///  2008��03��15�� ������ 02:35
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string DateStringFromNow(string dt)
        {
            return DateStringFromNow(DataType.ParseSysDateTime2DateTime(dt));
        }
        /// <summary>
        /// ʹ��C#�ѷ����ʱ���Ϊ������,����ǰ,��Сʱǰ,������ǰ,����ǰ
        ///  2008��03��15�� ������ 02:35
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string DateStringFromNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60)
            {
                return dt.ToShortDateString();
            }
            else
            {
                if (span.TotalDays > 30)
                {
                    return
                    "1����ǰ";
                }
                else
                {
                    if (span.TotalDays > 14)
                    {
                        return
                        "2��ǰ";
                    }
                    else
                    {
                        if (span.TotalDays > 7)
                        {
                            return
                            "1��ǰ";
                        }
                        else
                        {
                            if (span.TotalDays > 1)
                            {
                                return
                                string.Format("{0}��ǰ", (int)Math.Floor(span.TotalDays));
                            }
                            else
                            {
                                if (span.TotalHours > 1)
                                {
                                    return
                                    string.Format("{0}Сʱǰ", (int)Math.Floor(span.TotalHours));
                                }
                                else
                                {
                                    if (span.TotalMinutes > 1)
                                    {
                                        return
                                        string.Format("{0}����ǰ", (int)Math.Floor(span.TotalMinutes));
                                    }
                                    else
                                    {
                                        if (span.TotalSeconds >= 1)
                                        {
                                            return
                                            string.Format("{0}��ǰ", (int)Math.Floor(span.TotalSeconds));
                                        }
                                        else
                                        {
                                            return
                                            "1��ǰ";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //C#��ʹ��TimeSpan��������ʱ��Ĳ�ֵ
        //���Է�����������֮���κ�һ��ʱ�䵥λ��
        private string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days.ToString() + "��" + ts.Hours.ToString() + "Сʱ" + ts.Minutes.ToString() + "����" + ts.Seconds.ToString() + "��";
            return dateDiff;
        }

        //˵����
        /**/
        /*1.DateTimeֵ���ʹ�����һ���ӹ�Ԫ0001��1��1��0��0��0�뵽��Ԫ9999��12��31��23��59��59��֮��ľ�������ʱ�̡���ˣ��������DateTimeֵ�����������κ�������Χ֮�ڵ�ʱ�䡣һ��DateTimeֵ������һ�������ʱ��
        2.TimeSpanֵ��������������뷽�������ڷ��ʻ���һ��TimeSpanֵ
        ������б��������е�һ���֣�
        Add������һ��TimeSpanֵ��ӡ� 
        Days:���������������TimeSpanֵ�� 
        Duration:��ȡTimeSpan�ľ���ֵ�� 
        Hours:������Сʱ�����TimeSpanֵ 
        Milliseconds:�����ú�������TimeSpanֵ�� 
        Minutes:�����÷��Ӽ����TimeSpanֵ�� 
        Negate:���ص�ǰʵ�����෴���� 
        Seconds:������������TimeSpanֵ�� 
        Subtract:���м�ȥ��һ��TimeSpanֵ�� 
        Ticks:����TimeSpanֵ��tick���� 
        TotalDays:����TimeSpanֵ��ʾ�������� 
        TotalHours:����TimeSpanֵ��ʾ��Сʱ���� 
        TotalMilliseconds:����TimeSpanֵ��ʾ�ĺ������� 
        TotalMinutes:����TimeSpanֵ��ʾ�ķ������� 
        TotalSeconds:����TimeSpanֵ��ʾ��������
        */

        /**/
        /// <summary>
        /// ���ڱȽ�
        /// </summary>
        /// <param name="today">��ǰ����</param>
        /// <param name="writeDate">��������</param>
        /// <param name="n">�Ƚ�����</param>
        /// <returns>������������true��С�ڷ���false</returns>
        private bool CompareDate(string today, string writeDate, int n)
        {
            DateTime Today = Convert.ToDateTime(today);
            DateTime WriteDate = Convert.ToDateTime(writeDate);
            WriteDate = WriteDate.AddDays(n);
            if (Today >= WriteDate)
                return false;
            else
                return true;
        }
    }

}
