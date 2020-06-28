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
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WorldSkills20213
{
    public partial class EditIssue : Form
    {
        private static List<project_class> projects;
        private static List<user> users;
        private static List<int> selected_users = new List<int>();
        private static List<issue> issues;
        private static issue edit;

        public EditIssue(List<user> user_list, List<project_class> project_list, List<issue> issue_list, issue issue)
        {
            InitializeComponent();
            projects = project_list;
            users = user_list;
            issues = issue_list;
            edit = issue;
            tb_name.Text = edit.name;
            up_estimated.Value = edit.estimation;
            up_timespent.Value = edit.spent_time;
            if (edit.isDone)
            {
                cb_finished.SelectedIndex = 1;
            }
            else
            {
                cb_finished.SelectedIndex = 0;
            }
            foreach (var item in users)
            {
                all_users.Items.Add(item);
                foreach (var user in edit.assigned_users)
                {
                    if (item.Id == user)
                    {
                        lb_users.Items.Add(item);
                        selected_users.Add(item.id);
                        all_users.Items.Remove(item);
                    }
                }
            }
            foreach (var item in projects)
            {
                if (item.id == edit.projectId)
                {
                    cb_project.Items.Add(item);
                    cb_project.SelectedItem = item;
                }
                else
                {
                    cb_project.Items.Add(item);
                }
            }
        }

        public Form1 ParentForm { get; set; }

        private void NewIssue_Load(object sender, EventArgs e)
        {

        }

       

        private async Task putissue()
        {
            bool finished = false;
            if ((string)cb_finished.SelectedItem == "Finished")
            {
                finished = true;
            }
            issue nissue = new issue(edit.id, cb_project.SelectedIndex + 1, tb_name.Text, selected_users, (int)up_estimated.Value, (int)up_timespent.Value, finished);
            var httpClient = HttpClientFactory.Create();
            var url = "http://localhost:3000/issues/"+nissue.id;
            var json = JsonConvert.SerializeObject(nissue);
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var data = await httpClient.PutAsync(url, byteContent);
        }

        private void button1_Click_1(object sender, EventArgs e)
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

        private void button2_Click_1(object sender, EventArgs e)
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

        private async void button3_Click_1(object sender, EventArgs e)
        {
            await putissue();
            if (ParentForm != null)
            {
                ParentForm.clear();
                ParentForm.wait();
            }
            Close();
        }
    }
}
