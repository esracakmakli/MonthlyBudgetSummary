//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Question3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Categorys
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Categorys()
        {
            this.Expense = new HashSet<Expense>();
        }
    
        public int id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expense> Expense { get; set; }
        public virtual Expense Expense1 { get; set; }
    }
}
