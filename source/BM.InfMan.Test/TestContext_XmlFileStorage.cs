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
    using cc.bren.infman.impl;
    using System;
    using System.IO;

    public class TestContext_XmlFileStorage
    {
        public XmlFileInfManRepository InfManRepository;

        public TestContext_XmlFileStorage()
        {
        }

        public void Setup()
        {
            DirectoryInfo storageRoot = new DirectoryInfo(@"W:\dat\inf_man_" + UnixTime().ToString());

            if (storageRoot.Exists)
            {
                storageRoot.Delete(true);
            }

            storageRoot.Create();
            storageRoot.Refresh();

            InfManRepository = new XmlFileInfManRepository(storageRoot);
        }

        private int UnixTime()
        {
            return (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }
    }
}
