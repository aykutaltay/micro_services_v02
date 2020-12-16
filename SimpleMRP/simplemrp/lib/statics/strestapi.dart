import 'dart:convert';
import 'dart:io';

import 'package:http/http.dart' as http;
import 'package:simplemrp/models/cresponse.dart';
import 'package:simplemrp/models/crequest.dart';
import 'package:simplemrp/statics/stnumber.dart';
import 'package:simplemrp/statics/stpoolstr.dart';

class stRestApi {
  // Future<String> postSecV01(String url, String data) async {
  //   /// burada [await] ile [http.get] nesnesinin çalışmasını bekleyip
  //   /// [response] değişkenine attık
  //   final http.Response response = await http.post(
  //
  //       /// [Uri.encodeFull] ile adreste çıkabilecek (!#$&'()*+,-./:;=?@_~)
  //       /// gibi karakter hatalarının önüne geçiyoruz.
  //       Uri.encodeFull(url),
  //       headers: <String, String>{
  //         'Access-Control-Allow-Origin': '*',
  //         'Access-Control-Allow-Headers':
  //             'Origin, X-Requested-With, Content-Type, Accept',
  //         'Content-Type': 'application/json',
  //         "Authorization": "Bearer ${stPoolStr().token}",
  //       },
  //       body: data);
  //   if (response.statusCode == 200) {
  //     return response.body;
  //   } else {
  //     return stPoolStr().msgError;
  //   }
  // }

  // Future<String> postV01(String url, String data) async {
  //   /// burada [await] ile [http.get] nesnesinin çalışmasını bekleyip
  //   /// [response] değişkenine attık
  //   final http.Response response = await http.post(
  //
  //       /// [Uri.encodeFull] ile adreste çıkabilecek (!#$&'()*+,-./:;=?@_~)
  //       /// gibi karakter hatalarının önüne geçiyoruz. localhost:44342/OptCore
  //       //Uri.https("localhost:44342","/OptCore/auth")
  //       //url,
  //       'https://localhost:44342/OptCore',
  //       headers: <String, String>{
  //         'Access-Control-Allow-Origin': '*',
  //         'Access-Control-Allow-Headers':
  //             'Origin, X-Requested-With, Content-Type, Accept',
  //         'Content-Type': 'application/json',
  //       },
  //       body: data);
  //   if (response.statusCode == 200) {
  //     return response.body;
  //   } else {
  //     return stPoolStr().msgError;
  //   }
  // }

  Future<cResponse> post(String url, cRequest data) async {
    data.project_code=stNumber().project_Code;

    HttpClient client = new HttpClient();
    client.badCertificateCallback =
        ((X509Certificate cert, String host, int port) => true);

    HttpClientRequest request = await client.postUrl(Uri.parse(url));
    request.headers.add('Access-Control-Allow-Origin', '*');
    request.headers.add('Access-Control-Allow-Headers',
        'Origin, X-Requested-With, Content-Type, Accept');
    request.headers.add('Content-Type', 'application/json');
    request.add(utf8.encode(json.encode(data)));
    //String tt =request.toString();
    HttpClientResponse response = await request.close();

    String responseStr = await response.transform(utf8.decoder).join();
    cResponse result = cResponse.fromJson(jsonDecode(responseStr));

    return result;
    //print(reply);
  }

  Future<cResponse> postSec(String url, cRequest data) async {

    data.token=stPoolStr().token;
    data.project_code=stNumber().project_Code;


    HttpClient client = new HttpClient();
    client.badCertificateCallback =
        ((X509Certificate cert, String host, int port) => true);

    HttpClientRequest request = await client.postUrl(Uri.parse(url));

    request.headers.add('Access-Control-Allow-Origin', '*');
    request.headers.add('Access-Control-Allow-Headers',
        'Origin, X-Requested-With, Content-Type, Accept');
    request.headers.add('Content-Type', 'application/json');
    request.headers.add("Authorization", "Bearer ${stPoolStr().token}");

    request.add(utf8.encode(json.encode(data)));
    HttpClientResponse response = await request.close();

    String responseStr = await response.transform(utf8.decoder).join();
    cResponse result = cResponse.fromJson(jsonDecode(responseStr));

    return result;
    //print(reply);
  }

  cResponse postSec_sync(String url, cRequest data) {

    data.token=stPoolStr().token;
    data.project_code=stNumber().project_Code;


    HttpClient client = new HttpClient();
    client.badCertificateCallback =
    ((X509Certificate cert, String host, int port) => true);

    //HttpClientRequest request = await client.postUrl(Uri.parse(url));
    HttpClientRequest request; 
    client.postUrl(Uri.parse(url)).then((value) => request=value);

    request.headers.add('Access-Control-Allow-Origin', '*');
    request.headers.add('Access-Control-Allow-Headers',
        'Origin, X-Requested-With, Content-Type, Accept');
    request.headers.add('Content-Type', 'application/json');
    request.headers.add("Authorization", "Bearer ${stPoolStr().token}");

    request.add(utf8.encode(json.encode(data)));
    HttpClientResponse response;
    request.close().then((value) => response=value);
    //HttpClientResponse response = await request.close();

    //String responseStr = await response.transform(utf8.decoder).join();
    String responseStr ; 
    response.transform(utf8.decoder).join().then((value) => responseStr=value);
    
    final cResponse result = cResponse.fromJson(jsonDecode(responseStr));

    return result;
    //print(reply);
  }

  Future<cResponse> postSecV02(String url, cRequest data) async {

    data.token=stPoolStr().token;
    data.project_code=stNumber().project_Code;

    cResponse result = cResponse (
      data: "",message: "HATA",message_code: 0,token: data.token
    );


    // HttpClient client = new HttpClient();
    // client.badCertificateCallback =
    // ((X509Certificate cert, String host, int port) => true);
    //
    // HttpClientRequest request = await client.postUrl(Uri.parse(url));
    //
    // request.headers.add('Access-Control-Allow-Origin', '*');
    // request.headers.add('Access-Control-Allow-Headers',
    //     'Origin, X-Requested-With, Content-Type, Accept');
    // request.headers.add('Content-Type', 'application/json');
    // request.headers.add("Authorization", "Bearer ${stPoolStr().token}");
    //
    // request.add(utf8.encode(json.encode(data)));
    // HttpClientResponse response = await request.close();
    //
    // String responseStr = await response.transform(utf8.decoder).join();
    // cResponse result = cResponse.fromJson(jsonDecode(responseStr));
    //
    // return result;
    // //print(reply);


      final http.Response response = await http.post(

          /// [Uri.encodeFull] ile adreste çıkabilecek (!#$&'()*+,-./:;=?@_~)
          /// gibi karakter hatalarının önüne geçiyoruz.
          Uri.encodeFull(url),
          headers: <String, String>{
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Headers':
                'Origin, X-Requested-With, Content-Type, Accept',
            'Content-Type': 'application/json',
            "Authorization": "Bearer ${stPoolStr().token}",
          },
          body: utf8.encode(json.encode(data)),
      );

      if (response.statusCode == 200) {
        String bilgi = response.body;
       result = cResponse.fromJson(jsonDecode(bilgi));

      } else {
        result.message=stPoolStr().msgError;
      }

      final cResponse r = result;
      return r;
    }

}
