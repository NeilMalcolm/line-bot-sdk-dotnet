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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Line
{
    /// <summary>
    /// Encapsulates the datetimepicker mode.
    /// </summary>
    public enum DateTimePickerMode
    {
        /// <summary>
        /// Pick date.
        /// </summary>
        Date,

        /// <summary>
        /// Pick time.
        /// </summary>
        Time,

        /// <summary>
        /// Pick date and time.
        /// </summary>
        DateTime
    }

    /// <summary>
    /// Encapsulates a datetimepicker action.
    /// </summary>
    public sealed class DateTimePickerAction : IAction
    {
#pragma warning disable 0414 // Suppress value is never used.
        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<ActionType>))]
        private readonly ActionType _type = ActionType.DateTimePicker;
#pragma warning restore 0414

        private string _label;
        private string _data;
        private DateTimePickerMode _mode;
        private DateTime? _initial;
        private DateTime? _min;
        private DateTime? _max;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimePickerAction"/> class.
        /// </summary>
        /// <param name="mode">The mode of the Datetime picker action.</param>
        public DateTimePickerAction(DateTimePickerMode mode)
        {
            Mode = mode;
        }

        /// <summary>
        /// Gets or sets the label.
        /// <para>Max: 20 characters.</para>
        /// </summary>
        [JsonProperty("label")]
        public string Label
        {
            get
            {
                return _label;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The label cannot be null or whitespace.");

                if (value.Length > 20)
                    throw new InvalidOperationException("The label cannot be longer than 20 characters.");

                _label = value;
            }
        }

        /// <summary>
        /// Gets or sets the string returned via webhook in the postback.data property of the <see cref="IPostback"/> event.
        /// <para>Max: 300 characters.</para>
        /// </summary>
        [JsonProperty("data")]
        public string Data
        {
            get
            {
                return _data;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The data cannot be null or whitespace.");

                if (value.Length > 300)
                    throw new InvalidOperationException("The data cannot be longer than 300 characters.");

                _data = value;
            }
        }

        /// <summary>
        /// Gets the action mode for the datetimepicker.
        /// </summary>
        [JsonProperty("mode")]
        [JsonConverter(typeof(EnumConverter<DateTimePickerMode>))]
        public DateTimePickerMode Mode
        {
            get
            {
                return _mode;
            }

            private set
            {
                _mode = value;
            }
        }

        /// <summary>
        /// Gets or sets the initial datetime for the datetimepicker.
        /// </summary>
        [JsonProperty("initial")]
        public DateTime? Initial
        {
            get
            {
                return _initial;
            }

            set
            {
                var adjustedValue = AdjustedDateTimeByMode(value);
                if ((Min.HasValue && adjustedValue < Min.Value) || (Max.HasValue && adjustedValue > Max.Value))
                    throw new InvalidOperationException("The initial must be between the min and the max.");

                _initial = adjustedValue;
            }
        }

        /// <summary>
        /// Gets or sets the min datetime for the datetimepicker.
        /// </summary>
        [JsonProperty("min")]
        public DateTime? Min
        {
            get
            {
                return _min;
            }

            set
            {
                var adjustedValue = AdjustedDateTimeByMode(value);
                if (Max.HasValue && adjustedValue >= Max.Value)
                    throw new InvalidOperationException("The min must be less than the max.");

                _min = adjustedValue;
            }
        }

        /// <summary>
        /// Gets or sets the max datetime for the datetimepicker.
        /// </summary>
        [JsonProperty("max")]
        public DateTime? Max
        {
            get
            {
                return _max;
            }

            set
            {
                var adjustedValue = AdjustedDateTimeByMode(value);
                if (Min.HasValue && adjustedValue <= Min.Value)
                    throw new InvalidOperationException("The max must be greater than the min.");

                _max = adjustedValue;
            }
        }

        void IAction.Validate()
        {
            if (_label == null)
                throw new InvalidOperationException("The label cannot be null.");

            if (_data == null)
                throw new InvalidOperationException("The data cannot be null.");
        }

        private DateTime? AdjustedDateTimeByMode(DateTime? value)
        {
            if (value == null)
                return null;

            DateTime adjustedDateTime = value.Value;

            if (_mode == DateTimePickerMode.Date)
                adjustedDateTime = new DateTime(adjustedDateTime.Year, adjustedDateTime.Month, adjustedDateTime.Day, 0, 0, 0);
            else if (_mode == DateTimePickerMode.Time)
                adjustedDateTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, adjustedDateTime.Hour, adjustedDateTime.Minute, adjustedDateTime.Second);

            return adjustedDateTime;
        }
    }
}