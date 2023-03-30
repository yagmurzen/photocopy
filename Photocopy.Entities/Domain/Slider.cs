using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photocopy.Entities.Domain;

namespace Photocopy.Entities.Domain
{
    public class Slider : EntityBase
    {
        public  string Name { get; set; }
        public  string ButtonText { get; set; }
        public  string ImagePath { get; set; }
        public  string MobileImagePath { get; set; }
        public  bool IsActive { get; set; }
        public  int Position { get; set; }
        public  string Description { get; set; }
        public  string Subtitle { get; set; }
    }
}
