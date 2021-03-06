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
    public partial class ConfirmTemplateTests
    {
        [TestClass]
        public class TheTextProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var template = new ConfirmTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    template.Text = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var template = new ConfirmTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    template.Text = string.Empty;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan240Chars()
            {
                var template = new ConfirmTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 240 characters.", () =>
                {
                    template.Text = new string('x', 241);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs240Chars()
            {
                string value = new string('x', 240);

                var template = new ConfirmTemplate()
                {
                    Text = value
                };

                Assert.AreEqual(value, template.Text);
            }
        }
    }
}
