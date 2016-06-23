/*
    Infrastructure Manager

	Copyright (c) 2016, Brendon Matheson

	http://bren.cc/infman

	This work is copyright.  You may not reproduce or transmit it any any
	form or by any means without permission in writing from the owner of this
	work, Brendon Matheson.  If you infringe our copyright, you render yourself
    liable for prosecution.
*/

namespace cc.bren.infman
{
    using System;
    using System.IO;

    public static class Extensions
    {
        public static FileInfo File(
            this DirectoryInfo dir,
            string name)
        {
            if (dir == null) { throw new ArgumentNullException("dir"); }

            return new FileInfo(Path.Combine(
                dir.FullName,
                name));
        }
    }
}
