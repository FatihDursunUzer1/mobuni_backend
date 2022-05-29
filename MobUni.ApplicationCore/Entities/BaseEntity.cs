using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities
{
    public abstract class BaseEntity
    {
      //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)] olusturulması diger ogelere baglı olanlarda kullanılabilir.
      //Ornegin lokasyon,adres, metadata, vb
     
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }

        public void CreateObject()
        {
            this.CreatedTime = DateTime.Now.ToLocalTime();
            this.UpdatedTime = DateTime.Now.ToLocalTime();
        }
    }
}
