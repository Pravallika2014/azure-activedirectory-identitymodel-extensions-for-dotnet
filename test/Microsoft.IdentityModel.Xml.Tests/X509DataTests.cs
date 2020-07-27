﻿//------------------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using System;
using Microsoft.IdentityModel.TestUtils;
using Xunit;

namespace Microsoft.IdentityModel.Xml.Tests
{
#pragma warning disable CS3016 // Arrays as attribute arguments is not CLS-compliant
    public class X509DataTests
    {
        [Theory, MemberData(nameof(X509DataComparisonData))]
        public void X509Data_HashCodeTests(X509DataComparisonTheoryData theoryData)
        {
            var context = TestUtilities.WriteHeader($"{this}.{nameof(X509Data_HashCodeTests)}", theoryData);
            try
            {
                var firstHashCode = theoryData.FirstX509Data.GetHashCode();
                var secondHashCode = theoryData.SecondX509Data.GetHashCode();

                Assert.Equal(theoryData.HashShouldMatch, firstHashCode.Equals(secondHashCode));
            }
            catch (Exception ex)
            {
                theoryData.ExpectedException.ProcessException(ex, context);
            }

            TestUtilities.AssertFailIfErrors(context);
        }


        [Theory, MemberData(nameof(X509DataComparisonData))]
        public void X509Data_EqualsTests(X509DataComparisonTheoryData theoryData)
        {
            var context = TestUtilities.WriteHeader($"{this}.{nameof(X509Data_EqualsTests)}", theoryData);
            try
            {
                Assert.Equal(theoryData.ShouldBeConsideredEqual, theoryData.FirstX509Data.Equals(theoryData.SecondX509Data));
            }
            catch (Exception ex)
            {
                theoryData.ExpectedException.ProcessException(ex, context);
            }

            TestUtilities.AssertFailIfErrors(context);
        }

        public static TheoryData<X509DataComparisonTheoryData> X509DataComparisonData
        {
            get
            {
                return new TheoryData<X509DataComparisonTheoryData>
                {
                    new X509DataComparisonTheoryData
                    {
                        TestId = "Matching_empty",
                        FirstX509Data = new X509Data(),
                        SecondX509Data = new X509Data(),
                        ShouldBeConsideredEqual = true,
                        // Hash will always differ
                        HashShouldMatch = false,
                    },
                    new X509DataComparisonTheoryData
                    {
                        TestId = "Matching_Certificates",
                        FirstX509Data = new X509Data(ReferenceMetadata.X509Certificate1),
                        SecondX509Data = new X509Data(ReferenceMetadata.X509Certificate1),
                        ShouldBeConsideredEqual = true,
                    },
                    new X509DataComparisonTheoryData
                    {
                        TestId = "Nonmatching_Certificates",
                        FirstX509Data = new X509Data(ReferenceMetadata.X509Certificate1),
                        SecondX509Data = new X509Data(ReferenceMetadata.X509Certificate2),
                    },
                    new X509DataComparisonTheoryData
                    {
                        TestId = "Matching_MultipleCertificates",
                        FirstX509Data = new X509Data(new [] { ReferenceMetadata.X509Certificate1, ReferenceMetadata.X509Certificate2 }),
                        SecondX509Data = new X509Data(new [] { ReferenceMetadata.X509Certificate1, ReferenceMetadata.X509Certificate2 }),
                        ShouldBeConsideredEqual = true,
                    },
                    new X509DataComparisonTheoryData
                    {
                        TestId = "Nonmatching_MultipleCertificates",
                        FirstX509Data = new X509Data(new [] { ReferenceMetadata.X509Certificate1, ReferenceMetadata.X509Certificate2 }),
                        SecondX509Data = new X509Data(new [] { ReferenceMetadata.X509Certificate1, ReferenceMetadata.X509Certificate3 }),
                    },
                    new X509DataComparisonTheoryData
                    {
                        TestId = "Matching_SKI",
                        FirstX509Data = new X509Data()
                        {
                            SKI = "SKISampleString"
                        },
                        SecondX509Data = new X509Data()
                        {
                            SKI = "SKISampleString"
                        },
                        ShouldBeConsideredEqual = true,
                    },
                    new X509DataComparisonTheoryData
                    {
                        TestId = "Nonmatching_SKI",
                        FirstX509Data = new X509Data()
                        {
                            SKI = "SKISampleString"
                        },
                        SecondX509Data = new X509Data()
                        {
                            SKI = "AnotherSKISampleString"
                        },
                    },
                    new X509DataComparisonTheoryData
                    {
                        TestId = "Matching_CRL",
                        FirstX509Data = new X509Data()
                        {
                            CRL = "CRLSampleString"
                        },
                        SecondX509Data = new X509Data()
                        {
                            CRL = "CRLSampleString"
                        },
                        ShouldBeConsideredEqual = true,
                    },
                    new X509DataComparisonTheoryData
                    {
                        TestId = "Nonmatching_CRL",
                        FirstX509Data = new X509Data()
                        {
                            CRL = "CRLSampleString"
                        },
                        SecondX509Data = new X509Data()
                        {
                            CRL = "AnotherCRLSampleString"
                        },
                    },
                    new X509DataComparisonTheoryData
                    {
                        TestId = "Matching_IssuerSerial",
                        FirstX509Data = new X509Data()
                        {
                            IssuerSerial = new IssuerSerial("IssuerName", "SerialNumber"),
                        },
                        SecondX509Data = new X509Data()
                        {
                            IssuerSerial = new IssuerSerial("IssuerName", "SerialNumber"),
                        },
                        ShouldBeConsideredEqual = true,
                    },
                    new X509DataComparisonTheoryData
                    {
                        TestId = "Nonmatching_IssuerSerial",
                        FirstX509Data = new X509Data()
                        {
                            IssuerSerial = new IssuerSerial("IssuerName", "SerialNumber"),
                        },
                        SecondX509Data = new X509Data()
                        {
                            IssuerSerial = new IssuerSerial("AnotherIssuerName", "AnotherSerialNumber"),
                        },
                    }
                };
            }
        }

        public class X509DataComparisonTheoryData : TheoryDataBase
        {
            public X509Data FirstX509Data { get; set; }

            public X509Data SecondX509Data { get; set; }

            public bool ShouldBeConsideredEqual { get; set; }

            public bool HashShouldMatch { get; set; }
        }
    }
#pragma warning restore CS3016 // Arrays as attribute arguments is not CLS-compliant
}
