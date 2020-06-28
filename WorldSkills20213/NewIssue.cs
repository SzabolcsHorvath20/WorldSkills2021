using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WorldSkills20213
{
    public partial class NewIssue : Form
    {
        private static List<project_class> projects;
        private static List<user> users;
        private static List<int> selected_users = new List<int>();
        private static List<issue> issues;

        public NewIssue(List<user> user_list,List<project_class> project_list, List<issue> issue_list)
        {
            InitializeComponent();
            projects = project_list;
            users = user_list;
            issues = issue_list;
            foreach (var item in users)
            {
                all_users.Items.Add(item);
            }
            foreach (var item in projects)
            {
                cb_project.Items.Add(item);
            }
        }

        public Form1 ParentForm { get; set; }

        private void NewIssue_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lb_users.Items.Add(all_users.SelectedItem);
            string[] split = all_users.SelectedItem.ToString().Split('-');
            all_users.Items.Remove(all_users.SelectedItem);
            foreach (var item in users)
            {
                if (item.Id == Convert.ToInt32(split[0]))
                {
                    selected_users.Add(item.Id);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            all_users.Items.Add(lb_users.SelectedItem);
            lb_users.Items.Remove(lb_users.SelectedItem);
            string[] split = lb_users.SelectedItem.ToString().Split('-');
            foreach (var item in selected_users)
            {
                if (item == Convert.ToInt32(split[0]))
                {
                    selected_users.Remove(item);
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (cb_project.SelectedItem != null && tb_name.Text != "" && up_estimated.Value > 0)
            {
                await newissue();
                if (ParentForm != null)
                {
                    ParentForm.clear();
                    ParentForm.wait();
                }
                Close();
            }
            else
            {
                MessageBox.Show("You need to fill out every field!", "Empty values", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task newissue()
        {
            issue nissue = new issue(issues.Count+1, cb_project.SelectedIndex+1, tb_name.Text, selected_users, (int)up_estimated.Value, 0, false);
            var httpClient = HttpClientFactory.Create();
            var url = "http://localhost:3000/issues";
            var json = JsonConvert.SerializeObject(nissue);
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var data = await httpClient.PostAsync(url, byteContent);
        }
    }
}
