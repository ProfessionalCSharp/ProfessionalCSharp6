#if DNX46
using System.Security.Permissions;
#endif

namespace PInvokeSampleLib
{

    public static class FileUtility
    {
#if DNX46
        [FileIOPermission(SecurityAction.LinkDemand, Unrestricted = true)]
#endif
        public static void CreateHardLink(string oldFileName,
                                          string newFileName)
        {
            NativeMethods.CreateHardLink(oldFileName, newFileName);
        }
    }

}
