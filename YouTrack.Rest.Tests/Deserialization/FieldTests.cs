﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using YouTrack.Rest.Deserialization;
using YouTrack.Rest.Exceptions;

namespace YouTrack.Rest.Tests.Deserialization
{
    class FieldTests : TestFor<Field>
    {
        private Value_ dateTimeValue;
        private const ulong expectedMilliseconds = 1000;
        private readonly DateTime expectedDateTime = new DateTime(1970, 1, 1).AddMilliseconds(expectedMilliseconds).ToLocalTime();
        private Value_ stringValue;
        private Value_ integerValue;
        private static string expectedString = "foobar";
        private const int expectedInteger = 1000;

        protected override void SetupDependencies()
        {
            Sut.Values = new List<Value_>();

            dateTimeValue = CreateDateTimeValue();
            stringValue = CreateStringValue();
            integerValue = CreateIntegerValue();
        }

        private static Value_ CreateIntegerValue()
        {
            Value_ value = new Value_();
            value.Value = expectedInteger.ToString();

            return value;
        }

        private static Value_ CreateStringValue()
        {
            Value_ value = new Value_();
            value.Value = expectedString;

            return value;
        }

        private static Value_ CreateDateTimeValue()
        {
            Value_ timeValue = new Value_();
            timeValue.Value = expectedMilliseconds.ToString();

            return timeValue;
        }

        [Test]
        public void DatetimeIsParsed()
        {
            Sut.Values.Add(dateTimeValue);

            Assert.That(Sut.GetDateTime(), Is.EqualTo(expectedDateTime));
        }

        [Test]
        public void InvalidDateTimeIsNotParsed()
        {
            Sut.Values.Add(stringValue);

            Assert.Throws<FormatException>(() => Sut.GetDateTime());
        }

        [Test]
        public void Int32IsParsed()
        {
            Sut.Values.Add(integerValue);

            Assert.That(Sut.GetInt32(), Is.EqualTo(expectedInteger));
        }

        [Test]
        public void InvalidIntegerIsNotParsed()
        {
            Sut.Values.Add(stringValue);

            Assert.Throws<FormatException>(() => Sut.GetInt32());
        }

        [Test]
        public void StringIsParsed()
        {
            Sut.Values.Add(stringValue);

            Assert.That(Sut.GetValue(), Is.EqualTo(expectedString));
        }

        [Test]
        public void FieldIsConvertedToString()
        {
            Sut.Name = "Foo";
            Sut.Values.Add(integerValue);

            Assert.That(Sut.ToString(), Is.EqualTo(String.Format("{0}: {1}", Sut.Name, expectedInteger)));
        }

        [Test]
        public void ExceptionThrownIfNoValues()
        {
            Assert.Throws<IssueDeserializationException>(() => Sut.GetValue());
        }

        [Test]
        public void ExceptionThrownIfMoreThanOneValue()
        {
            Sut.Values.Add(stringValue);
            Sut.Values.Add(stringValue);

            Assert.Throws<IssueDeserializationException>(() => Sut.GetValue());
        }
    }
}
