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
    public partial class Form1 : Form
    {
        private static List<project_class> projects = new List<project_class>();
        private static List<user> users = new List<user>();
        private static List<issue> issues = new List<issue>();

        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {

            wait();
        }
        public void clear()
        {
            issues.Clear();
            users.Clear();
            projects.Clear();
            issues_list.Items.Clear();
            project_list.Items.Clear();
            project_overview.Items.Clear();
        }

        public async void wait()
        {
            await get_projects();
            await get_users();
            await get_issues();
            fillLists();
        }

        private async Task get_projects()
        {

            var httpClient = HttpClientFactory.Create();
            var url = "http://localhost:3000/projects";
            var data = await httpClient.GetStringAsync(url);
            projects = JsonConvert.DeserializeObject<List<project_class>>(data);
        }

        private async Task get_users()
        {

            var httpClient = HttpClientFactory.Create();
            var url = "http://localhost:3000/users";
            var data = await httpClient.GetStringAsync(url);
            users = JsonConvert.DeserializeObject<List<user>>(data);
        }

        private async Task get_issues()
        {

            var httpClient = HttpClientFactory.Create();
            var url = "http://localhost:3000/issues";
            var data = await httpClient.GetStringAsync(url);
            issues = JsonConvert.DeserializeObject<List<issue>>(data);

        }

        private void fillLists()
        {



            foreach (var item in projects)
            {
                project_list.View = View.Details;
                project_list.GridLines = true;
                project_overview.View = View.Details;
                project_overview.GridLines = true;
                ListViewItem lv = new ListViewItem();
                ListViewItem lv2 = new ListViewItem();
                lv.Tag = item.id;
                lv2.Tag = item.id;
                lv.Text = Convert.ToString(item.name);
                lv2.Text = Convert.ToString(item.name);
                foreach (var user in users)
                {
                    if (user.Id == item.Responsible)
                    {
                        lv.SubItems.Add(Convert.ToString(user.Name));
                        lv2.SubItems.Add(Convert.ToString(user.Name));
                    }
                }
                double estimated = 0;
                double completed = 0;
                double actual = 0;
                double task = 0;
                double completed_task = 0;
                foreach (var issue in issues)
                {
                    if (issue.projectId == item.Id)
                    {
                        task++;
                        actual += issue.spent_time;
                        if (issue.isDone)
                        {
                            completed_task++;
                            completed += issue.estimation;
                        }
                        else
                        {
                            estimated += issue.estimation;
                        }
                    }
                }
                lv.SubItems.Add(Convert.ToString(Math.Round(completed * 100 / estimated, 2)) + "%");
                lv2.SubItems.Add(Convert.ToString(Math.Round(completed * 100 / estimated, 2)) + "%");
                lv.SubItems.Add(Convert.ToString(actual));
                lv2.SubItems.Add(Convert.ToString(actual));
                double forcasted = Math.Round(actual * 100 / (completed * 100 / estimated), 2);
                lv2.SubItems.Add(Convert.ToString(forcasted));
                lv2.SubItems.Add(Convert.ToString(task));
                lv2.SubItems.Add(Convert.ToString(completed_task));
                lv.SubItems.Add(Convert.ToString(forcasted));
                lv.SubItems.Add(Convert.ToString(task));
                lv.SubItems.Add(Convert.ToString(completed_task));
                project_overview.Items.Add(lv2);
                project_list.Items.Add(lv);
            }
            foreach (var item in issues)
            {
                issues_list.View = View.Details;
                issues_list.GridLines = true;
                ListViewItem lv = new ListViewItem();
                foreach (var project in projects)
                {
                    if (project.id == item.projectId)
                    {
                        lv.Text = project.name;
                    }
                }
                lv.SubItems.Add(item.name);
                string assigned = "";
                foreach (var user_list in item.assigned_users)
                {
                    foreach (var user in users)
                    {
                        if (user.Id == user_list)
                        {
                            assigned += user.Name + "\n";
                        }
                    }
                }
                lv.SubItems.Add(assigned.Trim());
                lv.Tag = item.id;
                lv.SubItems.Add(Convert.ToString(item.estimation));
                lv.SubItems.Add(Convert.ToString(item.spent_time));
                if (item.isDone)
                {
                    lv.SubItems.Add("Finished");
                }
                else
                {
                    lv.SubItems.Add("Unfinished");
                }
                issues_list.Items.Add(lv);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewIssue newissue = new NewIssue(users, projects, issues);
            newissue.ParentForm = this;
            newissue.Show();
        }

        private void issues_list_DoubleClick(object sender, EventArgs e)
        {
            issue send = new issue();
            foreach (var item in issues)
            {
                if (item.id == Convert.ToInt32(issues_list.FocusedItem.Tag))
                {
                    send = item;
                }
            }
            EditIssue editissue = new EditIssue(users, projects, issues, send);
            editissue.ParentForm = this;
            editissue.Show();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (issues_list.FocusedItem != null)
            {
                issue send = new issue();
                foreach (var item in issues)
                {
                    if (item.id == Convert.ToInt32(issues_list.FocusedItem.Tag))
                    {
                        send = item;
                    }
                }
                issue nissue = new issue(send.id, send.projectId + 1, send.name, send.assigned_users, send.estimation, send.spent_time + (int)time_spent_add.Value, false);
                var httpClient = HttpClientFactory.Create();
                var url = "http://localhost:3000/issues/" + nissue.id;
                var json = JsonConvert.SerializeObject(nissue);
                var buffer = Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var data = await httpClient.PutAsync(url, byteContent);
                clear();
                wait();
            }
            else
            {
                MessageBox.Show("Choose an issue!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            project_class project = new project_class();
            if (project_list.FocusedItem != null)
            {
                foreach (var item in projects)
                {
                    if (item.id == Convert.ToInt32(project_list.FocusedItem.Tag))
                    {
                        project = item;
                    }

                }
            }
            else
            {
                    project.id = 0;
                    project.name = null;
                    project.responsible = 0;
            }
            
            Export export = new Export(project, projects,issues);
            export.ParentForm = this;
            export.Show();
        }
    }
}
