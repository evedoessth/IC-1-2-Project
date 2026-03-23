using System;
using UnityEngine;

namespace VillagerUtlities
{
    public class Job {

        public Job(String title, GameObject workplace)
        {
            this.JobTitle = title;
            this.Workplace = workplace;
        }
        private String JobTitle { get; set;}

        private GameObject Workplace { get; set;}
    }
}