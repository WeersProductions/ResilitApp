using System;
using Xamarin.Forms;

namespace ResilITApp
{
    public class Talk
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Color Color { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
    }
}
