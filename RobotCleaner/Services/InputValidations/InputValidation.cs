using Common.Exceptions;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InputValidations
{
    public static class InputValidation
    {
        public static void CheckInputParameters(string[] args)
        {
            Check.That(args.Length == 2, $"{Dictionary.CORRECT_FORMAT}");            
            Check.That(File.Exists(args[0]), $"{args[0]}: {Dictionary.NOT_FOUND}");
            Check.That(args[0] != args[1], Dictionary.DIFFERENT_FILENAMES);
            Check.That(!File.Exists(args[1]), $"{args[1]}: {Dictionary.TARGET_FILE_EXISTS}");

            Check.That(new FileInfo(args[2]).CanCreate(), Dictionary.NoCreatePermission);
        }
    }
}
