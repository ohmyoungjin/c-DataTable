using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;
namespace TestTable
{
    class MySQL
    {
        string ServerName;
        string DataBase;
        string userid;
        string userpw;
        ArrayList result = new ArrayList();
        MySqlConnection connection;
        public MySQL(string ServerName, string DataBase, string userid, string userpw)//생성자로 인수를 받아서 값을 지정해준다.
        {
            this.ServerName = ServerName;
            this.DataBase = DataBase;
            this.userid = userid;
            this.userpw = userpw;
        }//MySQLmethod end

        public Boolean Sql(string SQL)//insert,update,delete에 쓰이는 함수.
        { 
            MySqlConnection connection = new MySqlConnection("Server=" + ServerName + ";Database=" + DataBase + ";Uid=" + userid + ";Pwd=" + userpw);
            connection.Open(); //서버를 열어 연결한다.
            try
            {     
                MySqlCommand cmd = new MySqlCommand(SQL, connection);//실행할 SQL문을 담아둔다. 
                cmd.ExecuteNonQuery(); //SQL문을 실행한다.
                connection.Close(); //SQL문을 실행했으면 연결을 닫는다.
                return true; //성공했으면 true 값 반환
            }
            catch (Exception e)//예외처리중 문제이상이 생기는 부분을 처리하는 함수
            {

                MessageBox.Show(Convert.ToString(e));//어떤 문제인지 보여주는 부분
                return false;//실패했으면 false값 반환
            }
        }
       
    }//class end
}//namespace end