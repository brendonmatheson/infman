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
    using cc.bren.infman.spec;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;

    public abstract class BaseInfManRepository_HostSpec_UnitTests
    {

        //
        // HostSpecSingle
        //

        protected void HostSpecSingle_ValidRequest_Succeeds()
        {

        }

        protected void HostSpecSingle_NullFilter_Throws()
        {

        }

        //
        // HostSpecInsert
        //

        protected void HostSpecInsert_ValidRequest_Succeeds(
            InfManRepository infManRepository)
        {
            if (infManRepository == null) { throw new ArgumentNullException("infManRepository"); }

            // Setup
            HostSpecInsert request = HostSpecFactory.Insert("foo");

            // Execute
            HostSpecEntity result = infManRepository.HostSpecInsert(request);

            // Verify
            Assert.IsNotNull(result, "result");
        }

    }
}
