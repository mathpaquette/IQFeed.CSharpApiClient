﻿using System;
using System.Linq;
using IQFeed.CSharpApiClient.Lookup.Chains;
using IQFeed.CSharpApiClient.Lookup.Chains.Equities;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Chains.Messages
{
    public class EquityOptionMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_EquityOptionMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "LC,EBAY1803H25,EBAY1803H27.5,EBAY1803H30,EBAY1803H31,EBAY1803H32,EBAY1803H33,EBAY1803H34,EBAY1803H35,EBAY1803H36,EBAY1803H37,EBAY1803H38,EBAY1803H39,EBAY1803H40,EBAY1803H41,EBAY1803H42,EBAY1803H43,EBAY1803H44,EBAY1803H45,EBAY1803H46,EBAY1803H47.5,EBAY1803H50,EBAY1810H25,EBAY1810H27.5,EBAY1810H30,EBAY1810H31,EBAY1810H32,EBAY1810H33,EBAY1810H34,EBAY1810H35,EBAY1810H36,EBAY1810H37,EBAY1810H38,EBAY1810H39,EBAY1810H40,EBAY1810H41,EBAY1810H42,EBAY1810H42.5,EBAY1810H43,EBAY1810H44,EBAY1810H45,EBAY1810H47.5,EBAY1810H50,EBAY1813G25,EBAY1813G27.5,EBAY1813G30,EBAY1813G30.5,EBAY1813G31,EBAY1813G31.5,EBAY1813G32,EBAY1813G32.5,EBAY1813G33,EBAY1813G33.5,EBAY1813G34,EBAY1813G34.5,EBAY1813G35,EBAY1813G35.5,EBAY1813G36,EBAY1813G36.5,EBAY1813G37,EBAY1813G37.5,EBAY1813G38,EBAY1813G38.5,EBAY1813G39,EBAY1813G39.5,EBAY1813G40,EBAY1813G41,EBAY1813G42,EBAY1813G43,EBAY1813G44,EBAY1813G45,EBAY1813G46,EBAY1813G47,EBAY1813G50,EBAY1817H28,EBAY1817H29,EBAY1817H30,EBAY1817H31,EBAY1817H32,EBAY1817H33,EBAY1817H34,EBAY1817H35,EBAY1817H36,EBAY1817H37,EBAY1817H38,EBAY1817H39,EBAY1817H40,EBAY1817H41,EBAY1817H42,EBAY1817H43,EBAY1817H44,EBAY1817H45,EBAY1817H46,EBAY1817H47,EBAY1817H50,EBAY1819J25,EBAY1819J26,EBAY1819J27,EBAY1819J28,EBAY1819J29,EBAY1819J30,EBAY1819J31,EBAY1819J32,EBAY1819J33,EBAY1819J34,EBAY1819J35,EBAY1819J36,EBAY1819J37,EBAY1819J38,EBAY1819J39,EBAY1819J40,EBAY1819J41,EBAY1819J42,EBAY1819J43,EBAY1819J44,EBAY1819J45,EBAY1819J46,EBAY1819J47,EBAY1819J48,EBAY1819J49,EBAY1819J50,EBAY1819J52.5,EBAY1819J55,EBAY1820G23,EBAY1820G24,EBAY1820G25,EBAY1820G26,EBAY1820G27,EBAY1820G28,EBAY1820G29,EBAY1820G30,EBAY1820G31,EBAY1820G32,EBAY1820G33,EBAY1820G33.5,EBAY1820G34,EBAY1820G34.5,EBAY1820G35,EBAY1820G35.5,EBAY1820G36,EBAY1820G36.5,EBAY1820G37,EBAY1820G37.5,EBAY1820G38,EBAY1820G38.5,EBAY1820G39,EBAY1820G39.5,EBAY1820G40,EBAY1820G40.5,EBAY1820G41,EBAY1820G41.5,EBAY1820G42,EBAY1820G42.5,EBAY1820G43,EBAY1820G43.5,EBAY1820G44,EBAY1820G44.5,EBAY1820G45,EBAY1820G46,EBAY1820G47,EBAY1820G48,EBAY1820G49,EBAY1820G50,EBAY1820G52.5,EBAY1820G55,EBAY1821I25,EBAY1821I26,EBAY1821I27,EBAY1821I28,EBAY1821I29,EBAY1821I30,EBAY1821I31,EBAY1821I32,EBAY1821I33,EBAY1821I34,EBAY1821I35,EBAY1821I36,EBAY1821I37,EBAY1821I38,EBAY1821I39,EBAY1821I40,EBAY1821I41,EBAY1821I42,EBAY1821I43,EBAY1821I44,EBAY1821I45,EBAY1821I46,EBAY1821I47,EBAY1821I48,EBAY1821I49,EBAY1821I50,EBAY1824H25,EBAY1824H27.5,EBAY1824H30,EBAY1824H31,EBAY1824H32,EBAY1824H33,EBAY1824H34,EBAY1824H35,EBAY1824H36,EBAY1824H37,EBAY1824H38,EBAY1824H39,EBAY1824H40,EBAY1824H41,EBAY1824H42,EBAY1824H43,EBAY1824H44,EBAY1824H45,EBAY1824H47.5,EBAY1824H50,EBAY1827G25,EBAY1827G27.5,EBAY1827G30,EBAY1827G31,EBAY1827G32,EBAY1827G33,EBAY1827G34,EBAY1827G35,EBAY1827G36,EBAY1827G37,EBAY1827G38,EBAY1827G39,EBAY1827G40,EBAY1827G41,EBAY1827G42,EBAY1827G43,EBAY1827G44,EBAY1827G45,EBAY1827G46,EBAY1827G47,EBAY1827G50,:,EBAY1803T25,EBAY1803T27.5,EBAY1803T30,EBAY1803T31,EBAY1803T32,EBAY1803T33,EBAY1803T34,EBAY1803T35,EBAY1803T36,EBAY1803T37,EBAY1803T38,EBAY1803T39,EBAY1803T40,EBAY1803T41,EBAY1803T42,EBAY1803T43,EBAY1803T44,EBAY1803T45,EBAY1803T46,EBAY1803T47.5,EBAY1803T50,EBAY1810T25,EBAY1810T27.5,EBAY1810T30,EBAY1810T31,EBAY1810T32,EBAY1810T33,EBAY1810T34,EBAY1810T35,EBAY1810T36,EBAY1810T37,EBAY1810T38,EBAY1810T39,EBAY1810T40,EBAY1810T41,EBAY1810T42,EBAY1810T42.5,EBAY1810T43,EBAY1810T44,EBAY1810T45,EBAY1810T47.5,EBAY1810T50,EBAY1813S25,EBAY1813S27.5,EBAY1813S30,EBAY1813S30.5,EBAY1813S31,EBAY1813S31.5,EBAY1813S32,EBAY1813S32.5,EBAY1813S33,EBAY1813S33.5,EBAY1813S34,EBAY1813S34.5,EBAY1813S35,EBAY1813S35.5,EBAY1813S36,EBAY1813S36.5,EBAY1813S37,EBAY1813S37.5,EBAY1813S38,EBAY1813S38.5,EBAY1813S39,EBAY1813S39.5,EBAY1813S40,EBAY1813S41,EBAY1813S42,EBAY1813S43,EBAY1813S44,EBAY1813S45,EBAY1813S46,EBAY1813S47,EBAY1813S50,EBAY1817T28,EBAY1817T29,EBAY1817T30,EBAY1817T31,EBAY1817T32,EBAY1817T33,EBAY1817T34,EBAY1817T35,EBAY1817T36,EBAY1817T37,EBAY1817T38,EBAY1817T39,EBAY1817T40,EBAY1817T41,EBAY1817T42,EBAY1817T43,EBAY1817T44,EBAY1817T45,EBAY1817T46,EBAY1817T47,EBAY1817T50,EBAY1819V25,EBAY1819V26,EBAY1819V27,EBAY1819V28,EBAY1819V29,EBAY1819V30,EBAY1819V31,EBAY1819V32,EBAY1819V33,EBAY1819V34,EBAY1819V35,EBAY1819V36,EBAY1819V37,EBAY1819V38,EBAY1819V39,EBAY1819V40,EBAY1819V41,EBAY1819V42,EBAY1819V43,EBAY1819V44,EBAY1819V45,EBAY1819V46,EBAY1819V47,EBAY1819V48,EBAY1819V49,EBAY1819V50,EBAY1819V52.5,EBAY1819V55,EBAY1820S23,EBAY1820S24,EBAY1820S25,EBAY1820S26,EBAY1820S27,EBAY1820S28,EBAY1820S29,EBAY1820S30,EBAY1820S31,EBAY1820S32,EBAY1820S33,EBAY1820S33.5,EBAY1820S34,EBAY1820S34.5,EBAY1820S35,EBAY1820S35.5,EBAY1820S36,EBAY1820S36.5,EBAY1820S37,EBAY1820S37.5,EBAY1820S38,EBAY1820S38.5,EBAY1820S39,EBAY1820S39.5,EBAY1820S40,EBAY1820S40.5,EBAY1820S41,EBAY1820S41.5,EBAY1820S42,EBAY1820S42.5,EBAY1820S43,EBAY1820S43.5,EBAY1820S44,EBAY1820S44.5,EBAY1820S45,EBAY1820S46,EBAY1820S47,EBAY1820S48,EBAY1820S49,EBAY1820S50,EBAY1820S52.5,EBAY1820S55,EBAY1821U25,EBAY1821U26,EBAY1821U27,EBAY1821U28,EBAY1821U29,EBAY1821U30,EBAY1821U31,EBAY1821U32,EBAY1821U33,EBAY1821U34,EBAY1821U35,EBAY1821U36,EBAY1821U37,EBAY1821U38,EBAY1821U39,EBAY1821U40,EBAY1821U41,EBAY1821U42,EBAY1821U43,EBAY1821U44,EBAY1821U45,EBAY1821U46,EBAY1821U47,EBAY1821U48,EBAY1821U49,EBAY1821U50,EBAY1824T25,EBAY1824T27.5,EBAY1824T30,EBAY1824T31,EBAY1824T32,EBAY1824T33,EBAY1824T34,EBAY1824T35,EBAY1824T36,EBAY1824T37,EBAY1824T38,EBAY1824T39,EBAY1824T40,EBAY1824T41,EBAY1824T42,EBAY1824T43,EBAY1824T44,EBAY1824T45,EBAY1824T47.5,EBAY1824T50,EBAY1827S25,EBAY1827S27.5,EBAY1827S30,EBAY1827S31,EBAY1827S32,EBAY1827S33,EBAY1827S34,EBAY1827S35,EBAY1827S36,EBAY1827S37,EBAY1827S38,EBAY1827S39,EBAY1827S40,EBAY1827S41,EBAY1827S42,EBAY1827S43,EBAY1827S44,EBAY1827S45,EBAY1827S46,EBAY1827S47,EBAY1827S50, ";
            var messageWithRequestId = "TESTREQUEST,LC,EBAY1803H25,EBAY1803H27.5,EBAY1803H30,EBAY1803H31,EBAY1803H32,EBAY1803H33,EBAY1803H34,EBAY1803H35,EBAY1803H36,EBAY1803H37,EBAY1803H38,EBAY1803H39,EBAY1803H40,EBAY1803H41,EBAY1803H42,EBAY1803H43,EBAY1803H44,EBAY1803H45,EBAY1803H46,EBAY1803H47.5,EBAY1803H50,EBAY1810H25,EBAY1810H27.5,EBAY1810H30,EBAY1810H31,EBAY1810H32,EBAY1810H33,EBAY1810H34,EBAY1810H35,EBAY1810H36,EBAY1810H37,EBAY1810H38,EBAY1810H39,EBAY1810H40,EBAY1810H41,EBAY1810H42,EBAY1810H42.5,EBAY1810H43,EBAY1810H44,EBAY1810H45,EBAY1810H47.5,EBAY1810H50,EBAY1813G25,EBAY1813G27.5,EBAY1813G30,EBAY1813G30.5,EBAY1813G31,EBAY1813G31.5,EBAY1813G32,EBAY1813G32.5,EBAY1813G33,EBAY1813G33.5,EBAY1813G34,EBAY1813G34.5,EBAY1813G35,EBAY1813G35.5,EBAY1813G36,EBAY1813G36.5,EBAY1813G37,EBAY1813G37.5,EBAY1813G38,EBAY1813G38.5,EBAY1813G39,EBAY1813G39.5,EBAY1813G40,EBAY1813G41,EBAY1813G42,EBAY1813G43,EBAY1813G44,EBAY1813G45,EBAY1813G46,EBAY1813G47,EBAY1813G50,EBAY1817H28,EBAY1817H29,EBAY1817H30,EBAY1817H31,EBAY1817H32,EBAY1817H33,EBAY1817H34,EBAY1817H35,EBAY1817H36,EBAY1817H37,EBAY1817H38,EBAY1817H39,EBAY1817H40,EBAY1817H41,EBAY1817H42,EBAY1817H43,EBAY1817H44,EBAY1817H45,EBAY1817H46,EBAY1817H47,EBAY1817H50,EBAY1819J25,EBAY1819J26,EBAY1819J27,EBAY1819J28,EBAY1819J29,EBAY1819J30,EBAY1819J31,EBAY1819J32,EBAY1819J33,EBAY1819J34,EBAY1819J35,EBAY1819J36,EBAY1819J37,EBAY1819J38,EBAY1819J39,EBAY1819J40,EBAY1819J41,EBAY1819J42,EBAY1819J43,EBAY1819J44,EBAY1819J45,EBAY1819J46,EBAY1819J47,EBAY1819J48,EBAY1819J49,EBAY1819J50,EBAY1819J52.5,EBAY1819J55,EBAY1820G23,EBAY1820G24,EBAY1820G25,EBAY1820G26,EBAY1820G27,EBAY1820G28,EBAY1820G29,EBAY1820G30,EBAY1820G31,EBAY1820G32,EBAY1820G33,EBAY1820G33.5,EBAY1820G34,EBAY1820G34.5,EBAY1820G35,EBAY1820G35.5,EBAY1820G36,EBAY1820G36.5,EBAY1820G37,EBAY1820G37.5,EBAY1820G38,EBAY1820G38.5,EBAY1820G39,EBAY1820G39.5,EBAY1820G40,EBAY1820G40.5,EBAY1820G41,EBAY1820G41.5,EBAY1820G42,EBAY1820G42.5,EBAY1820G43,EBAY1820G43.5,EBAY1820G44,EBAY1820G44.5,EBAY1820G45,EBAY1820G46,EBAY1820G47,EBAY1820G48,EBAY1820G49,EBAY1820G50,EBAY1820G52.5,EBAY1820G55,EBAY1821I25,EBAY1821I26,EBAY1821I27,EBAY1821I28,EBAY1821I29,EBAY1821I30,EBAY1821I31,EBAY1821I32,EBAY1821I33,EBAY1821I34,EBAY1821I35,EBAY1821I36,EBAY1821I37,EBAY1821I38,EBAY1821I39,EBAY1821I40,EBAY1821I41,EBAY1821I42,EBAY1821I43,EBAY1821I44,EBAY1821I45,EBAY1821I46,EBAY1821I47,EBAY1821I48,EBAY1821I49,EBAY1821I50,EBAY1824H25,EBAY1824H27.5,EBAY1824H30,EBAY1824H31,EBAY1824H32,EBAY1824H33,EBAY1824H34,EBAY1824H35,EBAY1824H36,EBAY1824H37,EBAY1824H38,EBAY1824H39,EBAY1824H40,EBAY1824H41,EBAY1824H42,EBAY1824H43,EBAY1824H44,EBAY1824H45,EBAY1824H47.5,EBAY1824H50,EBAY1827G25,EBAY1827G27.5,EBAY1827G30,EBAY1827G31,EBAY1827G32,EBAY1827G33,EBAY1827G34,EBAY1827G35,EBAY1827G36,EBAY1827G37,EBAY1827G38,EBAY1827G39,EBAY1827G40,EBAY1827G41,EBAY1827G42,EBAY1827G43,EBAY1827G44,EBAY1827G45,EBAY1827G46,EBAY1827G47,EBAY1827G50,:,EBAY1803T25,EBAY1803T27.5,EBAY1803T30,EBAY1803T31,EBAY1803T32,EBAY1803T33,EBAY1803T34,EBAY1803T35,EBAY1803T36,EBAY1803T37,EBAY1803T38,EBAY1803T39,EBAY1803T40,EBAY1803T41,EBAY1803T42,EBAY1803T43,EBAY1803T44,EBAY1803T45,EBAY1803T46,EBAY1803T47.5,EBAY1803T50,EBAY1810T25,EBAY1810T27.5,EBAY1810T30,EBAY1810T31,EBAY1810T32,EBAY1810T33,EBAY1810T34,EBAY1810T35,EBAY1810T36,EBAY1810T37,EBAY1810T38,EBAY1810T39,EBAY1810T40,EBAY1810T41,EBAY1810T42,EBAY1810T42.5,EBAY1810T43,EBAY1810T44,EBAY1810T45,EBAY1810T47.5,EBAY1810T50,EBAY1813S25,EBAY1813S27.5,EBAY1813S30,EBAY1813S30.5,EBAY1813S31,EBAY1813S31.5,EBAY1813S32,EBAY1813S32.5,EBAY1813S33,EBAY1813S33.5,EBAY1813S34,EBAY1813S34.5,EBAY1813S35,EBAY1813S35.5,EBAY1813S36,EBAY1813S36.5,EBAY1813S37,EBAY1813S37.5,EBAY1813S38,EBAY1813S38.5,EBAY1813S39,EBAY1813S39.5,EBAY1813S40,EBAY1813S41,EBAY1813S42,EBAY1813S43,EBAY1813S44,EBAY1813S45,EBAY1813S46,EBAY1813S47,EBAY1813S50,EBAY1817T28,EBAY1817T29,EBAY1817T30,EBAY1817T31,EBAY1817T32,EBAY1817T33,EBAY1817T34,EBAY1817T35,EBAY1817T36,EBAY1817T37,EBAY1817T38,EBAY1817T39,EBAY1817T40,EBAY1817T41,EBAY1817T42,EBAY1817T43,EBAY1817T44,EBAY1817T45,EBAY1817T46,EBAY1817T47,EBAY1817T50,EBAY1819V25,EBAY1819V26,EBAY1819V27,EBAY1819V28,EBAY1819V29,EBAY1819V30,EBAY1819V31,EBAY1819V32,EBAY1819V33,EBAY1819V34,EBAY1819V35,EBAY1819V36,EBAY1819V37,EBAY1819V38,EBAY1819V39,EBAY1819V40,EBAY1819V41,EBAY1819V42,EBAY1819V43,EBAY1819V44,EBAY1819V45,EBAY1819V46,EBAY1819V47,EBAY1819V48,EBAY1819V49,EBAY1819V50,EBAY1819V52.5,EBAY1819V55,EBAY1820S23,EBAY1820S24,EBAY1820S25,EBAY1820S26,EBAY1820S27,EBAY1820S28,EBAY1820S29,EBAY1820S30,EBAY1820S31,EBAY1820S32,EBAY1820S33,EBAY1820S33.5,EBAY1820S34,EBAY1820S34.5,EBAY1820S35,EBAY1820S35.5,EBAY1820S36,EBAY1820S36.5,EBAY1820S37,EBAY1820S37.5,EBAY1820S38,EBAY1820S38.5,EBAY1820S39,EBAY1820S39.5,EBAY1820S40,EBAY1820S40.5,EBAY1820S41,EBAY1820S41.5,EBAY1820S42,EBAY1820S42.5,EBAY1820S43,EBAY1820S43.5,EBAY1820S44,EBAY1820S44.5,EBAY1820S45,EBAY1820S46,EBAY1820S47,EBAY1820S48,EBAY1820S49,EBAY1820S50,EBAY1820S52.5,EBAY1820S55,EBAY1821U25,EBAY1821U26,EBAY1821U27,EBAY1821U28,EBAY1821U29,EBAY1821U30,EBAY1821U31,EBAY1821U32,EBAY1821U33,EBAY1821U34,EBAY1821U35,EBAY1821U36,EBAY1821U37,EBAY1821U38,EBAY1821U39,EBAY1821U40,EBAY1821U41,EBAY1821U42,EBAY1821U43,EBAY1821U44,EBAY1821U45,EBAY1821U46,EBAY1821U47,EBAY1821U48,EBAY1821U49,EBAY1821U50,EBAY1824T25,EBAY1824T27.5,EBAY1824T30,EBAY1824T31,EBAY1824T32,EBAY1824T33,EBAY1824T34,EBAY1824T35,EBAY1824T36,EBAY1824T37,EBAY1824T38,EBAY1824T39,EBAY1824T40,EBAY1824T41,EBAY1824T42,EBAY1824T43,EBAY1824T44,EBAY1824T45,EBAY1824T47.5,EBAY1824T50,EBAY1827S25,EBAY1827S27.5,EBAY1827S30,EBAY1827S31,EBAY1827S32,EBAY1827S33,EBAY1827S34,EBAY1827S35,EBAY1827S36,EBAY1827S37,EBAY1827S38,EBAY1827S39,EBAY1827S40,EBAY1827S41,EBAY1827S42,EBAY1827S43,EBAY1827S44,EBAY1827S45,EBAY1827S46,EBAY1827S47,EBAY1827S50, ";

            // Act
            var equityOptionMessageParsed = EquityOptionMessage.Parse(message);
            var equityOptionMessageWithRequestIdParsed = EquityOptionMessage.ParseWithRequestId(messageWithRequestId);
            var equityOption = new EquityOption("EBAY1803H25", "EBAY", 25.0f, new DateTime(2018, 08, 03), OptionSide.Call);

            // Assert
            Assert.AreEqual(equityOptionMessageParsed.Chains.First(), equityOption);
            Assert.AreEqual("TESTREQUEST", equityOptionMessageWithRequestIdParsed.RequestId);
            Assert.AreEqual(equityOptionMessageWithRequestIdParsed.Chains.First(), equityOption);
        }
    }
}