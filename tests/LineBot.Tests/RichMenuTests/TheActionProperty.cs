﻿// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class RichMenuTests
    {
        [TestClass]
        public class TheActionProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenActionIsNull()
            {
                var richMenuArea = new RichMenuArea();

                ExceptionAssert.Throws<InvalidOperationException>("The action cannot be null.", () =>
                {
                    richMenuArea.Action = null;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenActionIsNotNull()
            {
                var action = new UriAction { Label = "testLabel2", Url = new Uri("http://www.bing.com") };

                var richMenuArea = new RichMenuArea
                {
                    Action = action
                };

                Assert.AreEqual(action, richMenuArea.Action);
            }
        }
    }
}