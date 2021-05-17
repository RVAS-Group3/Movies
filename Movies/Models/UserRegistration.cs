using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{

    public class UserRegistration
    {
    
        [BsonId]
        public Object _id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is requierd")]
        public string firstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is requierd")]
        public string lastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is requierd")]
        public string email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is requierd")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Need min 6 character")]
        public string password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is requierd")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Confirm Password should match with Password")]
        public string confirmPassword { get; set; }
        
    }
}
