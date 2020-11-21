using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace TestTable
{
    public partial class Form1 : Form
    {   
        string ServerName = "localhost";
        string DataBase = "member1";
        string userid = "root";
        string userpw = "1234"; //사용할 DataBase 서버 , Database, 아이디 ,비밀번호 를 기입한다.
        ArrayList list = new ArrayList();
        MySQL mysql = new MySQL("localhost", "member1", "root", "1234");//생성자로 쓰이는 
        bool result = false;
        string Grade = null;
        string Cla = null; //class는 c#의 예약어라 사용이 불가능하므로 이름을 cla로 줬다.
        int  No = 0;
        string Score = null; //사용되는 컬럼명을 전역변수로 설정
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) //처음 Form 창이 로딩될때 알아서 실행되는 함수.
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("grade", typeof(string));
            dataTable.Columns.Add("class", typeof(string));
            dataTable.Columns.Add("no", typeof(int));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("score", typeof(string)); //테이블의 컬럼명을 기술한다.

            dataGridView1.DataSource = dataTable;
            
        }
        private void getsetter()
        {

        }
        private void button1_Click(object sender, EventArgs e) //버튼 1번을 클릭하면 실행하는 함수. 조회버튼으로 쓰이고 있다.    
        {
            MySqlConnection connection = new MySqlConnection("Server=" + ServerName + ";Database=" + DataBase + ";Uid=" + userid + ";Pwd=" + userpw);
            string SQL = "select * from student"; //조회할 쿼리문
            DataTable dataTable = new DataTable(); //데이터 테이블 생성


            MySqlDataAdapter adapt = new MySqlDataAdapter(SQL, connection);//SQL문과 database 연결하는 인수를 받아 객체안의 메모리 상태로 저장해둔다.
            adapt.Fill(dataTable); // DataTable에 바인딩한다.
            dataGridView1.DataSource = dataTable; 
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int isi = this.dataGridView1.CurrentCell.RowIndex;
            Grade = this.dataGridView1.Rows[isi].Cells[0].Value.ToString(); //지금 선택돼있는 부분을 가르킨다.
            Cla =  this.dataGridView1.Rows[isi].Cells[1].Value.ToString();
            No = Convert.ToInt32(this.dataGridView1.Rows[isi].Cells[2].Value.ToString());
            Name = this.dataGridView1.Rows[isi].Cells[3].Value.ToString();
            Score = this.dataGridView1.Rows[isi].Cells[4].Value.ToString();
            result = mysql.Sql("INSERT INTO student (grade,class,no,name,score) VALUES('" + Grade + "','" + Cla + "'," + No + ",'" + Name + "','" + Score + "')");
           /* MessageBox.Show(name); 값이 제대로 넘어왔는지 확인하는 부분.
            MessageBox.Show(age);
             if (result == true)
             {
                 MessageBox.Show("데이터가 삽입됐습니다");

             }
             else
             {
                 MessageBox.Show("데이터 삽입 실패"); 성공실패 여부를 눈으로 확인하기 위한 부분.
             }*/


        }

        private void button3_Click(object sender, EventArgs e)//수정 버튼
        {
            int isi = this.dataGridView1.CurrentCell.RowIndex;
            Grade = this.dataGridView1.Rows[isi].Cells[0].Value.ToString();
            Cla = this.dataGridView1.Rows[isi].Cells[1].Value.ToString();
            No = Convert.ToInt32(this.dataGridView1.Rows[isi].Cells[2].Value.ToString());
            Name = this.dataGridView1.Rows[isi].Cells[3].Value.ToString();
            Score = this.dataGridView1.Rows[isi].Cells[4].Value.ToString();
            result = mysql.Sql("update student set grade = '"+Grade+"' ,class = '"+Cla+"'," +
                                "no = "+No+" , score = '"+Score+"'  WHERE Name = '" + Name + "'");

            /*MessageBox.Show(name);
            MessageBox.Show(age); 값이 제대로 넘어왔는지 확인하는 부분 
            if (result == true)
            {
                MessageBox.Show("데이터가 성공적으로 수정됐습니다");

            }
            else
            {
                MessageBox.Show("데이터 수정실패"); 성공실패 여부를 눈으로 확인하기 위한 부분.
            }*/
            
        }
        private void button4_Click(object sender, EventArgs e)//삭제 버튼
        {
            DataTable dt = new DataTable();
            int isi = this.dataGridView1.CurrentCell.RowIndex;
            Grade = this.dataGridView1.Rows[isi].Cells[0].Value.ToString();
            Cla = this.dataGridView1.Rows[isi].Cells[1].Value.ToString();
            No = Convert.ToInt32(this.dataGridView1.Rows[isi].Cells[2].Value.ToString());
            Name = this.dataGridView1.Rows[isi].Cells[3].Value.ToString();
            Score = this.dataGridView1.Rows[isi].Cells[4].Value.ToString();
            result = mysql.Sql("DELETE FROM STUDENT WHERE Name = '" + Name + "'");
            dt.AcceptChanges();//테이블데이터에 대한 커밋부분.
            Boolean ab = dataGridView1.AllowUserToDeleteRows;//행을 삭제할 수 있는지 확인 설정해주는 부분
            if (ab == true)
            {
                dataGridView1.Rows.RemoveAt(isi);
                if (result == true)
                {
                    MessageBox.Show("데이터가 성공적으로 삭제됐습니다");

                }
                else
                {
                    MessageBox.Show("데이터 삭제실패");
                }
            }
            else
            {
                MessageBox.Show("아직커밋이 되지않았습니다.");
            }
            
        }
    }
}