using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldSkills20213
{
    public partial class Export : Form
    {
        private static List<project_class> projects;
        private static List<issue> issues;
        public project_class exp_issue;

        public Export(project_class issue, List<project_class> project_list, List<issue> issue_list)
        {
            InitializeComponent();
            projects = project_list;
            issues = issue_list;
            exp_issue = issue;
            if (issue.name == null)
            {
                cb_issue.Enabled = false;
            }
        }

        public Form1 ParentForm { get; set; }

        private void Export_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cb_issue.Checked || cb_project.Checked)
            {
                if (cb_project.Checked)
                {
                    StreamWriter writeproject = new StreamWriter("projects.csv");
                    writeproject.WriteLine("id,name,responsible");
                    foreach (var item in projects)
                    {
                        writeproject.WriteLine(item.id+","+item.name+","+item.responsible);
                    }
                    writeproject.Close();
                }
                if (cb_issue.Checked)
                {
                    StreamWriter writeissue = new StreamWriter("issues.csv");
                    writeissue.WriteLine("id,projectId,name,assigned_users,estimation,spent_time,isDone");
                    foreach (var item in issues)
                    {
                        if (item.projectId == exp_issue.id)
                        {
                            writeissue.Write(item.id + "," + item.projectId +","+item.name + ",");
                            for (int i = 0; i < item.assigned_users.Count; i++)
                            {
                                if (i == item.assigned_users.Count - 1)
                                {
                                    writeissue.Write(item.assigned_users[i] + ";");
                                }
                                else
                                {
                                    writeissue.Write(item.assigned_users[i] + ",");
                                }
                            }
                            writeissue.Write(item.estimation + "," + item.spent_time + "," + item.isDone+"\n");

                        }
                    }
                    writeissue.Close();
                }
            }
            else
            {
                MessageBox.Show("Choose what do you want to export!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
