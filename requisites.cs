//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MasterFloor
{
    using System;
    using System.Collections.Generic;
    
    public partial class requisites
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public requisites()
        {
            this.employes = new HashSet<employes>();
        }
    
        public int id { get; set; }
        public string nameRecipient { get; set; }
        public string lastnameRecipient { get; set; }
        public string fathernameRecipient { get; set; }
        public string inn { get; set; }
        public string number { get; set; }
        public string nameBank { get; set; }
        public string k_s { get; set; }
        public string bik { get; set; }
        public string kbk { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<employes> employes { get; set; }
    }
}
