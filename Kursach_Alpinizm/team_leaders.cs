//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kursach_Alpinizm
{
    using System;
    using System.Collections.Generic;
    
    public partial class team_leaders
    {
        public int ID_entry { get; set; }
        public Nullable<int> ID_groups { get; set; }
        public Nullable<int> ID_team_member { get; set; }
    
        public virtual groups groups { get; set; }
        public virtual team team { get; set; }
    }
}
