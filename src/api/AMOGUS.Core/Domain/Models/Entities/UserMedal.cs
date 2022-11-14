using AMOGUS.Core.Common.Interfaces.User;
using AMOGUS.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOGUS.Core.Domain.Models.Entities {
    public class UserMedal {
        
        [Key]
        public UserMedalType MedalId { get; set; }

        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public IApplicationUser User { get; set; }


        [Required]
        public DateTime AquisitionTime { get; set; }

        [NotMapped]
        public string MedalName { get {
                throw new NotImplementedException();
            }
        }
        [NotMapped]
        public string Description { get {
                throw new NotImplementedException();
        } }
    }
}
