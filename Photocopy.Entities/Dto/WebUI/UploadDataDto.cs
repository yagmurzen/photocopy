using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Dto.WebUI
{
    public class UploadDataDto
    {
        public Guid Id { get; set; }
        public string FileData { get; set; }
        public string FileName { get; set; }
    }
}
