using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Photocopy.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Photocopy.Helper
{
    public interface IHttpClientExtensions
    {
        ApiResponse GetTokenAsync();
        ApiResponse CreateOrder(MngOrder orderModel);
        ApiResponse GetCities();
        ApiResponse GetDistrict(string cityCode);
    }

    public class TokenModel
    {
        public string customerNumber { get; set; }
        public string password { get; set; }
        public int identityType { get; set; }
    }


    public class HttpClientExtensions : IHttpClientExtensions
    {
        private readonly IHttpClientFactory factory;
        private readonly Apimodel _configuration;

        public HttpClientExtensions(IHttpClientFactory factory, IOptions<Apimodel> configuration)
        {
            this.factory = factory;
            this._configuration = configuration.Value;
        }


        public ApiResponse GetTokenAsync()
        {
            try
            {
                using (var client = factory.CreateClient())
                {
                    client.BaseAddress = new Uri("https://testapi.mngkargo.com.tr/");
                    client.DefaultRequestHeaders.Clear();

                    //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-IBM-Client-Id", _configuration.XIBMClientId/* "92c21416-8b6c-49ff-b248-4c63395515d3"*/);
                    client.DefaultRequestHeaders.Add("X-IBM-Client-Secret", _configuration.XIBMClientSecret/* "eA1fN7tR2tB2jQ2cV6bE5yG1hU4vB5sC5wP7tX6sF1iU5fQ6iU"*/);
                    client.DefaultRequestHeaders.Add("x-api-version", "");

                    TokenModel tokenModel = new TokenModel();
                    tokenModel.customerNumber = _configuration.Username /*"35615719"*/;
                    tokenModel.password = _configuration.Password /* "4440606Mng."*/;
                    tokenModel.identityType = 1;

                    var myContent = JsonHelper.Serialize(tokenModel);
                    StringContent content = new StringContent(myContent, Encoding.UTF8, "application/json");
                    var response = client.PostAsync("mngapi/api/token", content);
                    response.Wait();
                    var result = response.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        return new ApiResponse(new ApiResponseModel { Repsonse = readTask.Result, StatusCode = 200 });
                    }
                    else
                        return new ApiResponse("Token Alınamadı.");
                }
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message);
            }

        }

        public ApiResponse CreateOrder(MngOrder orderModel)
        {
            ApiResponse responseToken = GetTokenAsync();
            if (!responseToken.Success)
                return new ApiResponse("Token Alınamadı.");
            try
            {
                MngTokenModel token = JsonHelper.Deserialize<MngTokenModel>(responseToken.response.Repsonse);

                using (var client = factory.CreateClient())
                {
                    client.DefaultRequestHeaders.Clear();

                    client.BaseAddress = new Uri("https://testapi.mngkargo.com.tr/");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-IBM-Client-Id", _configuration.XIBMClientId/* "92c21416-8b6c-49ff-b248-4c63395515d3"*/);
                    client.DefaultRequestHeaders.Add("X-IBM-Client-Secret", _configuration.XIBMClientSecret/* "eA1fN7tR2tB2jQ2cV6bE5yG1hU4vB5sC5wP7tX6sF1iU5fQ6iU"*/);
                    client.DefaultRequestHeaders.Add("x-api-version", "");
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.jwt);


                    MngOrder tokenModel = new MngOrder();
                    tokenModel.order = new Order
                    {
                        referenceId = "SIPARIS34567",
                        barcode = "SIPARIS34567",
                        billOfLandingId = "İrsaliye 1",
                        isCOD = 0,
                        codAmount = 0,
                        shipmentServiceType = 1,
                        packagingType = 1,
                        content = "İçerik 1",
                        smsPreference1 = 1,
                        smsPreference2 = 0,
                        smsPreference3 = 0,
                        paymentType = 1,
                        deliveryType = 1,
                        description = "Açıklama 1",
                        marketPlaceShortCode = "",
                        marketPlaceSaleCode = "",
                        pudoId = ""
                    };
                    tokenModel.recipient = new Recipient
                    {
                        customerId = 58513278,
                        refCustomerId = "",
                        cityCode = 0,
                        cityName = "",
                        districtName = "",
                        districtCode = 0,
                        address = "",
                        bussinessPhoneNumber = "",
                        email = "",
                        taxOffice = "",
                        taxNumber = "",
                        fullName = "",
                        homePhoneNumber = "",
                        mobilePhoneNumber = ""
                    };
                    tokenModel.orderPieceList = new List<Orderpiecelist>();
                    tokenModel.orderPieceList.Add(new Orderpiecelist
                    {
                        barcode = "SIPARIS34567_PARCA1",
                        desi = 0,
                        kg = 0,
                        content = "Parça açıklama 1"

                    });
                    // adres alanı 10 ve üzeri olmalıdır
                    // customerid var ise fulname bo olmalıdır
                    // cityCode ve dstrict Code mngId olmalıdır

                    var myContent = JsonHelper.Serialize(orderModel);
                    var content = new StringContent(myContent, Encoding.UTF8, "application/json");
                    var response = client.PostAsync("/mngapi/api/standardcmdapi/createOrder", content);
                    response.Wait();

                    var result = response.Result;
                    var ss = result.Content.ReadAsStringAsync();
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        return new ApiResponse(new ApiResponseModel { Repsonse = readTask.Result, StatusCode = 200 });
                    }
                    else return new ApiResponse("Sipariş Oluşturulamadı.");
                }
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message);
            }

        }

        public ApiResponse GetCities()
        {
            ApiResponse responseToken = GetTokenAsync();
            if (!responseToken.Success)
                return new ApiResponse("Token Alınamadı.");
            try
            {
                MngTokenModel token = JsonHelper.Deserialize<MngTokenModel>(responseToken.response.Repsonse);

                using (var client = factory.CreateClient())
                {
                    client.BaseAddress = new Uri("https://testapi.mngkargo.com.tr/");
                    client.DefaultRequestHeaders.Clear();

                    //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-IBM-Client-Id", _configuration.XIBMClientId/* "92c21416-8b6c-49ff-b248-4c63395515d3"*/);
                    client.DefaultRequestHeaders.Add("X-IBM-Client-Secret", _configuration.XIBMClientSecret/* "eA1fN7tR2tB2jQ2cV6bE5yG1hU4vB5sC5wP7tX6sF1iU5fQ6iU"*/);
                    client.DefaultRequestHeaders.Add("x-api-version", "");
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.jwt);


                    var response = client.GetAsync("mngapi/api/cbsinfoapi/getcities");
                    response.Wait();
                    var result = response.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        return new ApiResponse(new ApiResponseModel { Repsonse = readTask.Result, StatusCode = 200 });
                    }
                    else
                        return new ApiResponse("İl Bilgileri Alınamadı.");
                }
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message);
            }
        }

        public ApiResponse GetDistrict(string cityCode)
        {
            ApiResponse responseToken = GetTokenAsync();
            if (!responseToken.Success)
                return new ApiResponse("Token Alınamadı.");

            try
            {
                MngTokenModel token = JsonHelper.Deserialize<MngTokenModel>(responseToken.response.Repsonse);

                using (var client = factory.CreateClient())
                {
                    client.BaseAddress = new Uri("https://testapi.mngkargo.com.tr/");
                    client.DefaultRequestHeaders.Clear();

                    //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-IBM-Client-Id", _configuration.XIBMClientId/* "92c21416-8b6c-49ff-b248-4c63395515d3"*/);
                    client.DefaultRequestHeaders.Add("X-IBM-Client-Secret", _configuration.XIBMClientSecret/* "eA1fN7tR2tB2jQ2cV6bE5yG1hU4vB5sC5wP7tX6sF1iU5fQ6iU"*/);
                    client.DefaultRequestHeaders.Add("x-api-version", "");
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.jwt);

                    var response = client.GetAsync("mngapi/api/cbsinfoapi/getdistricts/" + cityCode);
                    response.Wait();
                    var result = response.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        return new ApiResponse(new ApiResponseModel { Repsonse = readTask.Result, StatusCode = 200 });
                    }
                    else
                        return new ApiResponse("İlçe Bilgileri Alınamadı.");
                }
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message);
            }
        }
    }
}
