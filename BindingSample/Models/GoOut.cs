using System;
namespace BindingSample.Models
{
    public class GoOut
    {
        public DateTime EventTime { get; set; }
        public string Content { get; set; }
        public string Comment { get; set; }
        public EStatus Status { get; set; }

        public enum EStatus
        {
            Plan,
            Checked,
            OnHold,
        }

        public GoOut()
        {
        }
    }
}
