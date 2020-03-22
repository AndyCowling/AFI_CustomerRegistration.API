using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace AFI_CustomerRegistration.API.Models
{
    public class CustomerDetail
    {
        /*========================================================================================
         *this class is used to hold (and validate) the customer detail information
         *========================================================================================
         */
        [Key]
        public int CustomerID { get; set; }

        /*========================================================================================
         * FirstName
         * validation rules
         *  required
         *  must be between 3 and 50 characters
         *  must be alpha characters
         *========================================================================================
         */
        [Required(ErrorMessage = "Please enter a first name")] // make it required
        [StringLength(50, ErrorMessage = "Please enter a first name with between 3 and 50 characters", MinimumLength = 3)] // make it at least 3 characters long
        [RegularExpression(@"^[A-Za-z]{3,50}",ErrorMessage = "Please only enter letters into the first name")] // ensure it's alpha characters - i.e. no numbers!
        public string FirstName { get; set; }

        /*========================================================================================
        * SURNAME 
        * validation rules
        *   required
        *   must be between 3 and 50 characters
        *   must be alpha characters
        * ========================================================================================
        */
        [Required(ErrorMessage = "Please enter a surname")]
        [StringLength(50, ErrorMessage = "Please enter a surname with between 3 and 50 characters", MinimumLength = 3)] // at least 3 characters, max 50
        [RegularExpression(@"^[A-Za-z]{3,50}", ErrorMessage = "Please only enter letters into the surname")] // ensure it's alpha characters - i.e. no numbers!
        public string Surname { get; set; }

        /*========================================================================================
        * ReferenceNumber
        * validation rules
        *   required
        *   must match pattern of XX-999999 (2 alpha + dash + 6 numbers)
        * ========================================================================================
        */
        [Required(ErrorMessage = "Please enter a policy reference number")] // make it required
        [RegularExpression(@"^[A-Z]{2}-[0-9]{6}$", ErrorMessage = "Please enter a valid policy reference number (XX-123456)")] // make it match the XX-123456 format

        public string ReferenceNumber { get; set; }

        /*========================================================================================
        * Date of Birth
        * validation rules
        *   just a date - will have to do over 18 validation later on
        *   TODO - check the over 18 validation
        * ========================================================================================
        */
        [DataType(DataType.Date)]
        public string DOB { get; set; }

        /*========================================================================================
        * Email
        * validation rules
        *   needs to be an email address
        *   needs to have >3 alphanumeric characters
        *   then an @
        *   then >1 alphanumerics
        *   needs to end in either .co.uk or .com
        *   TODO regular expression for email
        * ========================================================================================
        */
        [EmailAddress]
        public string Email { get; set; }
    }
}
