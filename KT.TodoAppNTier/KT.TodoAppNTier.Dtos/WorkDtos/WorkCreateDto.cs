using KT.TodoAppNTier.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.TodoAppNTier.Dtos.WorkDtos
{
    public class WorkCreateDto :IDto
    {
        //[Required(ErrorMessage = "Açıklama Gereklidir.")]
        public string Definition { get; set; }
        public bool IsCompleted { get; set; }
    }
}
