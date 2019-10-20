using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using environmentshow20191010;

namespace environmentshow20191010
{
    public partial class enter : Form
    {
        public enter()
        {
            InitializeComponent();
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string connStr = "Server=DESKTOP-IJAJNJP\\SQLEXPRESS;database=tempdata;Integrated Security=True";
            SqlConnection sqlConn = new SqlConnection(connStr);
            try
            {
                // 连接数据库
                sqlConn.Open();

                // 构造命令发送给数据库
                string sqlStr = "select * from gongzuo where id=@id and password=@password";
                SqlCommand cmd = new SqlCommand(sqlStr, sqlConn);

                // 注意是用用户ID登录，而不是用户名，用户名可能会重复
                cmd.Parameters.Add(new SqlParameter("@id", this.textBox1.Text.Trim()));
                cmd.Parameters.Add(new SqlParameter("@password", this.textBox2.Text.Trim()));

                SqlDataReader dr = cmd.ExecuteReader();

                // 如果从数据库中查询到记录，则表示可以登录
                if (dr.HasRows)
                {
                    dr.Read();
                    UserInfo.userId = int.Parse(dr["id"].ToString());
                    UserInfo.userPwd = dr["password"].ToString();

                    MessageBox.Show(UserInfo.userName + "登录成功");
                    // 显示读卡界面
                    Form1 formUser = new Form1();
                    formUser.Show();

                    // 隐藏登录界面
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("用户名或密码错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("访问数据库错误：" + exp.Message);
            }
            finally
            {
                sqlConn.Close();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
