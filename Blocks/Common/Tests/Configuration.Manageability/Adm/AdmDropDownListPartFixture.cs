﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Core
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Collections.Generic;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Tests.Adm
{
    [TestClass]
    public class AdmDropDownListPartFixture
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AdmDropDownListPartWriteWithNullWriterThrows()
        {
            AdmDropDownListPart part = new AdmDropDownListPart("name", "key", "value", new List<AdmDropDownListItem>(), null);

            part.Write(null);
        }

        [TestMethod]
        public void InstanceGeneratesAppropriateAdmTextWithoutDefault()
        {
            List<AdmDropDownListItem> items = new List<AdmDropDownListItem>();
            items.Add(new AdmDropDownListItem("key1", "value1"));
            items.Add(new AdmDropDownListItem("key2", "value2"));

            AdmDropDownListPart part = new AdmDropDownListPart("name", "key", "value", items, null);

            StringWriter writer = new StringWriter();
            part.Write(writer);

            StringReader contentsReader = new StringReader(writer.ToString());

            Assert.AreEqual(string.Format(AdmDropDownListPart.PartStartTemplate, "name"), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.DropDownListTemplate), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.ValueNameTemplate, "value"), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.KeyNameTemplate, "key"), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.ItemListStartTemplate), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.ListItemTemplate, "key1", "value1"), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.ListItemTemplate, "key2", "value2"), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.ItemListEndTemplate), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.PartEndTemplate), contentsReader.ReadLine());
            Assert.IsNull(contentsReader.ReadLine());
        }

        [TestMethod]
        public void InstanceGeneratesAppropriateAdmTextWithDefault()
        {
            List<AdmDropDownListItem> items = new List<AdmDropDownListItem>();
            items.Add(new AdmDropDownListItem("key1", "value1"));
            items.Add(new AdmDropDownListItem("key2", "value2"));

            AdmDropDownListPart part = new AdmDropDownListPart("name", "key", "value", items, "key1");

            StringWriter writer = new StringWriter();
            part.Write(writer);

            StringReader contentsReader = new StringReader(writer.ToString());

            Assert.AreEqual(string.Format(AdmDropDownListPart.PartStartTemplate, "name"), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.DropDownListTemplate), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.ValueNameTemplate, "value"), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.KeyNameTemplate, "key"), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.ItemListStartTemplate), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.DefaultListItemTemplate, "key1", "value1"), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.ListItemTemplate, "key2", "value2"), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.ItemListEndTemplate), contentsReader.ReadLine());
            Assert.AreEqual(string.Format(AdmDropDownListPart.PartEndTemplate), contentsReader.ReadLine());
            Assert.IsNull(contentsReader.ReadLine());
        }
    }
}
