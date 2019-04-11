﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinkedIn.Models;

namespace ClinkedIn.Validators
{
    public class CreateUserRequestValidator
    {
        public bool Validate(CreateUserRequest requestToValidate)
        {
            return !(string.IsNullOrEmpty(requestToValidate.Username)
                || string.IsNullOrEmpty(requestToValidate.Password));
        }
    }
}
