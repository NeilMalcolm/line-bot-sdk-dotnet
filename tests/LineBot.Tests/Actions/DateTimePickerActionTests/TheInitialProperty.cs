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
    public partial class DateTimePickerActionTest
    {
        [TestClass]
        public class TheInitialProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenInitialIsGreaterThanMax()
            {
                var action = new DateTimePickerAction()
                {
                    Min = new DateTime(2018, 10, 8)
                };

                ExceptionAssert.Throws<InvalidOperationException>("The initial must be between the min and the max.", () =>
                {
                    action.Initial = new DateTime(2018, 10, 7);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenInitialIsLessThanMin()
            {
                var action = new DateTimePickerAction()
                {
                    Max = new DateTime(2018, 10, 8)
                };

                ExceptionAssert.Throws<InvalidOperationException>("The initial must be between the min and the max.", () =>
                {
                    action.Initial = new DateTime(2018, 10, 9);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenInitialIsBetweenMinAndMax()
            {
                var min = new DateTime(2018, 10, 7);
                var initial = new DateTime(2018, 10, 8);
                var max = new DateTime(2018, 10, 9);

                var action = new DateTimePickerAction()
                {
                    Mode = DateTimePickerMode.Date,
                    Min = min,
                    Initial = initial,
                    Max = max
                };

                Assert.AreEqual(min, action.Min);
                Assert.AreEqual(initial, action.Initial);
                Assert.AreEqual(max, action.Max);
            }
        }
    }
}
