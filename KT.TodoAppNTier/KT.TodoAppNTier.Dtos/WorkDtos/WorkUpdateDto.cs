using KT.TodoAppNTier.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.TodoAppNTier.Dtos.WorkDtos
{
    public class WorkUpdateDto :IDto
    {
        [Range(1,int.MaxValue,ErrorMessage ="İd alanı 0 olamaz")]
        public int Id { get; set; }
        [Required(ErrorMessage ="Açıklama Alanı Girilmesi Zorunludur")]
        public string Definition { get; set; }
        public bool IsCompleted { get; set; }
    }
}
