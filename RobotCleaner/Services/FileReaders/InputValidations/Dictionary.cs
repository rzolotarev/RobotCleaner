﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileReaders.InputValidations
{
    public static class Dictionary
    {
        public static readonly string CORRECT_FORMAT = @"Make sure the input string is in the format: compress source_file target_file.gz OR decompress source_file.gz target_file";

        public static readonly string CORRECT_COMMAND = "Make sure the input command is 'compress' or 'decompress'";

        public static readonly string COMPRESS_COMMAND = "compress";

        public static readonly string DECOMPRESS_COMMAND = "decompress";

        public static readonly string NOT_FOUND = "File was not found";

        public static readonly string DIFFERENT_FILENAMES = "Files should have different names";

        public static readonly string GZ_EXTENSION = ".gz";

        public static readonly string TARGET_FILE_EXISTS = "Make sure the target file doesn't exist";

        public static readonly string COMPRESS_TO_GZ = "Make sure the target file has gz extension at compress";

        public static readonly string DECOMPRESS_FROM_GZ = "Make sure the source file has gz extension at decompress";

        public static readonly string NoCreatePermission = "Make sure the user has permission to create files";
    }
}
