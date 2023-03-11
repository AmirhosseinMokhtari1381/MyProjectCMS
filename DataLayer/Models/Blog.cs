using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Display(Name = "گروهبندی وبلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]


        public int GroupId { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }
        [Display(Name = "متن اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        public string BlogTexe { get; set; }
        [Display(Name = "بازدید")]
        public int Visit { get; set; }
        [Display(Name = "نام تصویر")]
        public string ImageName { get; set; }


        [Display(Name = "تاریخ ایجاد")]
        //فرمت تاریخ رو درست میکند
        [DisplayFormat(DataFormatString ="{0:yyyy/mm/dd}")]
        public DateTime CreateDate { get; set; }

       
        public virtual BlogGroup BlogGroup { get; set; }

        //یک بلاگ میتواند چندین کامنت داشته باشد
        public virtual ICollection<BlogComment> BlogComments { get; set; }

    }
}
