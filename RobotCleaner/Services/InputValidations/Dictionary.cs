﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InputValidations
{
    public static class Dictionary
    {
        public static readonly string CORRECT_FORMAT = @"Make sure the input string has two parts: input file and output file";                

        public static readonly string NOT_FOUND = "File was not found";

        public static readonly string DIFFERENT_FILENAMES = "Files should have different names";        

        public static readonly string TARGET_FILE_EXISTS = "Make sure the target file doesn't exist";       

        public static readonly string NoCreatePermission = "Make sure the user has permission to create files";
    }
}
