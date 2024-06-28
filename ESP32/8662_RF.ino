#include <ESP8266WiFi.h>
#include <WiFiClientSecure.h>
#include <ArduinoJson.h>
#include <RCSwitch.h>

const char* ssid = "Ahmed's A54";
const char* password = "ahmed1234";

const char* serverUrl = "https://trusec-dev-api.azurewebsites.net/api/TruckDataLogs";
const char* driverExpression = "RF";
float latitude = 0.0;
float longitude = 0.0;
int truckId = 1;
float speedKPH = 0.0;

RCSwitch mySwitch = RCSwitch();

void parseRFData(String data) {
  int latIndex = data.indexOf("lat:");
  int lngIndex = data.indexOf("lng:");

  if (latIndex != -1 && lngIndex != -1) {
    latitude = data.substring(latIndex + 4, data.indexOf(',', latIndex)).toFloat();
    longitude = data.substring(lngIndex + 4).toFloat();
  }
}

bool sendPostRequest(String jsonPayload) {
  WiFiClientSecure client;
  client.setInsecure();

  if (!client.connect(serverUrl, 443)) {
    Serial.println("Connection to server failed");
    return false;
  }
  String postRequest = String("POST /api/TruckDataLogs HTTP/1.1\r\n") +
                       "Host: trusec-dev-api.azurewebsites.net\r\n" +
                       "Content-Type: application/json\r\n" +
                       "Content-Length: " + jsonPayload.length() + "\r\n" +
                       "Connection: close\r\n\r\n" +
                       jsonPayload;
  client.print(postRequest);
  while (client.connected()) {
    String line = client.readStringUntil('\n');
    if (line == "\r") {
      Serial.println("Headers received");
      break;
    }
  }
  String response = client.readString();
  Serial.println("Response: " + response);
  if (response.indexOf("200 OK") != -1 || response.indexOf("201 Created") != -1) {
    return true;
  } else {
    return false;
  }
}

void setup(){
  pinMode(0, INPUT);

  Serial.begin(115200);  
  WiFi.begin(ssid, password);
  Serial.print("Connecting to WiFi...");
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("connected");
  mySwitch.enableReceive(0); 
}

void loop(){

  if (mySwitch.available()) {
    String receivedData = String(mySwitch.getReceivedValue(), DEC);
    Serial.println("Received: " + receivedData);
    parseRFData(receivedData);
    DynamicJsonDocument doc(1024);
    doc["timeStamp"] = millis();
    doc["driverExpression"] = driverExpression;
    doc["latitude"] = latitude;
    doc["longitude"] = longitude;
    doc["truckId"] = truckId;
    doc["speedKPH"] = speedKPH;
    String output;
    serializeJson(doc, output);
    if (sendPostRequest(output))
    {
      Serial.println("POST request sent successfully");
    } 
    else
    {
      Serial.println("Error sending POST request");
    }
    mySwitch.resetAvailable();
  }
}
