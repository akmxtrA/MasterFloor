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
    
    public partial class partnerContact
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public partnerContact()
        {
            this.partners = new HashSet<partners>();
        }
    
        public int id { get; set; }
        public string lastname { get; set; }
        public string name { get; set; }
        public string fathername { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<partners> partners { get; set; }
    }
}
