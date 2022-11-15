using AMOGUS.Core.Domain.Enums;
using AMOGUS.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMOGUS.Core.Domain.Models.Entities {
    public class UserMedal {
        
        [Key]
        public UserMedalType MedalId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


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
