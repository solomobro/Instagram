using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Tests.Serialization
{
    [TestFixture]
    class UsersEndpointTests
    {
        private static readonly  JsonSerializer Serializer = new JsonSerializer();

        [Test]
        public void CanDeserializeGetUser()
        {
            const string response =
                "{\"meta\":{\"code\":200},\"data\":{\"username\":\"solomobro\",\"bio\":\"This is a developer test account for an Instagram .NET SDK\",\"website\":\"https://github.com/solomobro/Instagram\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-xpt1/t51.2885-19/s150x150/11917846_398932440308986_1930181477_a.jpg\",\"full_name\":\"So Lo Mo, Bro!\",\"counts\":{\"media\":2,\"followed_by\":3,\"follows\":1},\"id\":\"12345678\"}}";

            using (var s = new MemoryStream(Encoding.UTF8.GetBytes(response)))
            {
                var resp = Serializer.Deserialize<Response<User>>(s);

                Assert.That(resp, Is.Not.Null);
            }
        }

        [Test]
        public void CanDeserializeRecentMedia()
        {
            const string response = "{\"pagination\":{},\"meta\":{\"code\":200},\"data\":[{\"attribution\":null,\"videos\":{\"low_bandwidth\":{\"url\":\"https://scontent.cdninstagram.com/hphotos-xtf1/t50.2886-16/11912712_809201045844840_586472251_s.mp4\",\"width\":480,\"height\":480},\"standard_resolution\":{\"url\":\"https://scontent.cdninstagram.com/hphotos-xtf1/t50.2886-16/11935453_1142555942424790_887461745_n.mp4\",\"width\":640,\"height\":640},\"low_resolution\":{\"url\":\"https://scontent.cdninstagram.com/hphotos-xtf1/t50.2886-16/11912712_809201045844840_586472251_s.mp4\",\"width\":480,\"height\":480}},\"tags\":[],\"type\":\"video\",\"location\":{\"latitude\":40.7142,\"name\":\"New York, New York\",\"longitude\":-74.0064,\"id\":212988663},\"comments\":{\"count\":3,\"data\":[{\"created_time\":\"1443055128\",\"text\":\"Fula, Fula, Fula\",\"from\":{\"username\":\"christian.villamor\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-prn/t51.2885-19/10853087_676551885775816_116559943_a.jpg\",\"id\":\"1553324802\",\"full_name\":\"Christian\"},\"id\":\"1080747205865296570\"},{\"created_time\":\"1443055140\",\"text\":\"#fula #wetakingover #solomobro\",\"from\":{\"username\":\"christian.villamor\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-prn/t51.2885-19/10853087_676551885775816_116559943_a.jpg\",\"id\":\"1553324802\",\"full_name\":\"Christian\"},\"id\":\"1080747305681343170\"},{\"created_time\":\"1443062157\",\"text\":\"rofl, didn't even know this video had audio of my music\",\"from\":{\"username\":\"solomobro\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-xpt1/t51.2885-19/s150x150/11917846_398932440308986_1930181477_a.jpg\",\"id\":\"2166761587\",\"full_name\":\"So Lo Mo, Bro!\"},\"id\":\"1080806169130881776\"}]},\"filter\":\"Lark\",\"created_time\":\"1443054479\",\"link\":\"https://www.instagram.com/p/7_kOhZmNN6/\",\"likes\":{\"count\":5,\"data\":[{\"username\":\"ekb_afisha\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-xap1/t51.2885-19/11312282_783672458416538_1955468509_a.jpg\",\"id\":\"2126939216\",\"full_name\":\"\u0410\u0444\u0438\u0448\u0430 \u0415\u043A\u0430\u0442\u0435\u0440\u0438\u043D\u0431\u0443\u0440\u0433\u0430\"},{\"username\":\"christian.villamor\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-prn/t51.2885-19/10853087_676551885775816_116559943_a.jpg\",\"id\":\"1553324802\",\"full_name\":\"Christian\"},{\"username\":\"nnov_online\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-xap1/t51.2885-19/11330647_1578450182417512_1990867468_a.jpg\",\"id\":\"1946191898\",\"full_name\":\"\u0410\u0444\u0438\u0448\u0430 \u041D\u0438\u0436\u043D\u0438\u0439 \u041D\u043E\u0432\u0433\u043E\u0440\u043E\u0434\"},{\"username\":\"shmay\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-xpt1/t51.2885-19/11248125_811268515637432_1064807802_a.jpg\",\"id\":\"2325285\",\"full_name\":\"Shereen May \uD83D\uDE45\uD83C\uDFFB\"}]},\"images\":{\"low_resolution\":{\"url\":\"https://scontent.cdninstagram.com/hphotos-xtf1/t51.2885-15/s320x320/e15/11849009_1610051442592070_1153584277_n.jpg\",\"width\":320,\"height\":320},\"thumbnail\":{\"url\":\"https://scontent.cdninstagram.com/hphotos-xtf1/t51.2885-15/s150x150/e15/11849009_1610051442592070_1153584277_n.jpg\",\"width\":150,\"height\":150},\"standard_resolution\":{\"url\":\"https://scontent.cdninstagram.com/hphotos-xtf1/t51.2885-15/e15/11849009_1610051442592070_1153584277_n.jpg\",\"width\":640,\"height\":640}},\"users_in_photo\":[],\"caption\":{\"created_time\":\"1443054479\",\"text\":\"Test Vid\",\"from\":{\"username\":\"solomobro\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-xpt1/t51.2885-19/s150x150/11917846_398932440308986_1930181477_a.jpg\",\"id\":\"2166761587\",\"full_name\":\"So Lo Mo, Bro!\"},\"id\":\"1080741816469868658\"},\"user_has_liked\":false,\"id\":\"1080741763202208634_2166761587\",\"user\":{\"username\":\"solomobro\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-xpt1/t51.2885-19/s150x150/11917846_398932440308986_1930181477_a.jpg\",\"id\":\"2166761587\",\"full_name\":\"So Lo Mo, Bro!\"}},{\"attribution\":null,\"tags\":[\"test\",\"hashtags\"],\"type\":\"image\",\"location\":null,\"comments\":{\"count\":2,\"data\":[{\"created_time\":\"1443050182\",\"text\":\"this is a comment with a bunch of #hashtags #test\",\"from\":{\"username\":\"solomobro\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-xpt1/t51.2885-19/s150x150/11917846_398932440308986_1930181477_a.jpg\",\"id\":\"2166761587\",\"full_name\":\"So Lo Mo, Bro!\"},\"id\":\"1080705719048130770\"},{\"created_time\":\"1443055118\",\"text\":\"amazing bird\",\"from\":{\"username\":\"christian.villamor\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-prn/t51.2885-19/10853087_676551885775816_116559943_a.jpg\",\"id\":\"1553324802\",\"full_name\":\"Christian\"},\"id\":\"1080747120653816501\"}]},\"filter\":\"Clarendon\",\"created_time\":\"1441933594\",\"link\":\"https://www.instagram.com/p/7eKTyImNLJ/\",\"likes\":{\"count\":5,\"data\":[{\"username\":\"christian.villamor\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-prn/t51.2885-19/10853087_676551885775816_116559943_a.jpg\",\"id\":\"1553324802\",\"full_name\":\"Christian\"},{\"username\":\"kenny23_ill_make_you_rich\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-ash/t51.2885-19/10518258_801738696511533_19317499_a.jpg\",\"id\":\"1419217188\",\"full_name\":\"Kenny Watson\"},{\"username\":\"shmay\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-xpt1/t51.2885-19/11248125_811268515637432_1064807802_a.jpg\",\"id\":\"2325285\",\"full_name\":\"Shereen May \uD83D\uDE45\uD83C\uDFFB\"}]},\"images\":{\"low_resolution\":{\"url\":\"https://scontent.cdninstagram.com/hphotos-xft1/t51.2885-15/s320x320/e35/11849418_930064977073430_1521625644_n.jpg\",\"width\":320,\"height\":320},\"thumbnail\":{\"url\":\"https://scontent.cdninstagram.com/hphotos-xft1/t51.2885-15/s150x150/e35/11849418_930064977073430_1521625644_n.jpg\",\"width\":150,\"height\":150},\"standard_resolution\":{\"url\":\"https://scontent.cdninstagram.com/hphotos-xft1/t51.2885-15/s640x640/sh0.08/e35/11849418_930064977073430_1521625644_n.jpg\",\"width\":640,\"height\":640}},\"users_in_photo\":[],\"caption\":{\"created_time\":\"1441933594\",\"text\":\"This is a picture of a bird and a fishing pole\",\"from\":{\"username\":\"solomobro\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-xpt1/t51.2885-19/s150x150/11917846_398932440308986_1930181477_a.jpg\",\"id\":\"2166761587\",\"full_name\":\"So Lo Mo, Bro!\"},\"id\":\"1071339103541842029\"},\"user_has_liked\":true,\"id\":\"1071339101327250121_2166761587\",\"user\":{\"username\":\"solomobro\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-xpt1/t51.2885-19/s150x150/11917846_398932440308986_1930181477_a.jpg\",\"id\":\"2166761587\",\"full_name\":\"So Lo Mo, Bro!\"}}]}";

            using (var s = new MemoryStream(Encoding.UTF8.GetBytes(response)))
            {
                var resp = Serializer.Deserialize<CollectionResponse<Post>>(s);
                Assert.That(resp, Is.Not.Null);
            }
        }
    }
}
