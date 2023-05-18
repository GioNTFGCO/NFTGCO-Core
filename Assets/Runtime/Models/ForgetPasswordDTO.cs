using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NFTGCO.Models.DTO
{
    public class ForgetPasswordDTO
    {
        public string email;
        public ForgetPasswordDTO(string userEmail)
        {
            email = userEmail;
        }
    }
}