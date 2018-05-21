using Common.Exceptions;
using Common.Extensions;
using Services.FileReaders.InputValidations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileReaders
{
    public class InputValidation
    {
        public void CheckInputParameters(string[] args)
        {
            Check.That(args.Length == 3, $"{Dictionary.CORRECT_FORMAT}");
            Check.That(args[0] == Dictionary.COMPRESS_COMMAND || args[0] == Dictionary.DECOMPRESS_COMMAND, $"{args[0]}: {Dictionary.CORRECT_COMMAND}");
            Check.That(File.Exists(args[1]), $"{args[1]}: {Dictionary.NOT_FOUND}");
            Check.That(args[1] != args[2], Dictionary.DIFFERENT_FILENAMES);
            Check.That(!File.Exists(args[2]), $"{args[2]}: {Dictionary.TARGET_FILE_EXISTS}");

            Check.That(CompressCommandExtensionsCorrect(args), Dictionary.COMPRESS_TO_GZ);

            Check.That(DecompressCommandExtensionsCorrect(args), Dictionary.DECOMPRESS_FROM_GZ);

            Check.That(new FileInfo(args[2]).CanCreate(), Dictionary.NoCreatePermission);
        }

        public static bool CompressCommandExtensionsCorrect(string[] args)
        {
            if (args[0] == Dictionary.COMPRESS_COMMAND)
                return args[2].EndsWith(Dictionary.GZ_EXTENSION);
            else
                return true;
        }

        public static bool DecompressCommandExtensionsCorrect(string[] args)
        {
            if (args[0] == Dictionary.DECOMPRESS_COMMAND)
                return args[1].EndsWith(Dictionary.GZ_EXTENSION);
            else
                return true;
        }
    }
}
