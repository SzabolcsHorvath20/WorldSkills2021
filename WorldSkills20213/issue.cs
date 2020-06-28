using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldSkills20213
{
    public class issue
    {
        int Id;
        int ProjectId;
        string Name;
        List<int> Assigned_users = new List<int>();
        int Estimation;
        int Spent_time;
        bool IsDone;

        public issue(int id, int projectId, string name, List<int> assigned_users, int estimation, int spent_time, bool isDone)
        {
            Id = id;
            ProjectId = projectId;
            Name = name;
            Assigned_users = assigned_users;
            Estimation = estimation;
            Spent_time = spent_time;
            IsDone = isDone;
        }

        public issue()
        {
            
        }

        public int id { get => Id; set => Id = value; }
        public int projectId { get => ProjectId; set => ProjectId = value; }
        public string name { get => Name; set => Name = value; }
        public List<int> assigned_users { get => Assigned_users; set => Assigned_users = value; }
        public int estimation { get => Estimation; set => Estimation = value; }
        public int spent_time { get => Spent_time; set => Spent_time = value; }
        public bool isDone { get => IsDone; set => IsDone = value; }
    }
}
