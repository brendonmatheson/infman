/*
    Infrastructure Manager

	Copyright (c) 2016, Brendon Matheson

	http://bren.cc/infman

	This work is copyright.  You may not reproduce or transmit it any any
	form or by any means without permission in writing from the owner of this
	work, Brendon Matheson.  If you infringe our copyright, you render yourself
    liable for prosecution.
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cc.bren.infman
{
    [TestClass]
    public class XmlFileInfManRepository_HostSpec_UnitTests : BaseInfManRepository_HostSpec_UnitTests, InfManRepository_HostSpec_UnitTests
    {
        private static TestContext_XmlFileStorage TC = new TestContext_XmlFileStorage();

        [ClassInitialize]
        public static void BeforeClass(TestContext tc)
        {
            TC.Setup();
        }

        //
        // HostSpecSingle
        //

        [TestMethod]
        public void HostSpecSingle_ValidRequest_Succeeds()
        {

        }

        [TestMethod]
        public void HostSpecSingle_NullFilter_Throws()
        {

        }

        //
        // HostSpecInsert
        //

        [TestMethod]
        public void HostSpecInsert_ValidRequest_Succeeds()
        {
            base.HostSpecInsert_ValidRequest_Succeeds(TC.InfManRepository);
        }
    }
}
