from tensorflow.keras import Sequential
from tensorflow.keras.models import load_model
import cv2
import numpy as np
from tensorflow.keras.preprocessing.image import img_to_array
import requests
import json
from datetime import datetime
import geocoder
from picamera2 import Picamera2, Preview
import time
import cv2

model = Sequential()
classifier = load_model('/home/ahmed/env2/emotion_detection.h5')
face_classifier = cv2.CascadeClassifier('haarcascade_frontalface_default.xml')

class_labels = {0: 'Angry', 1: 'Fear', 2: 'Happy', 3: 'Neutral', 4: 'Sad', 5: 'Surprise'}
classes = list(class_labels.values())


def text_on_detected_boxes(text,text_x,text_y,image,font_scale = 1,

                           font = cv2.FONT_HERSHEY_SIMPLEX,

                           FONT_COLOR = (0, 0, 0),

                           FONT_THICKNESS = 2,

                           rectangle_bgr = (0, 255, 0)):
    (text_width, text_height) = cv2.getTextSize(text, font, fontScale=font_scale, thickness=2)[0]
    box_coords = ((text_x-10, text_y+4), (text_x + text_width+10, text_y - text_height-5))
    cv2.rectangle(image, box_coords[0], box_coords[1], rectangle_bgr, cv2.FILLED)
    cv2.putText(image, text, (text_x, text_y), font, fontScale=font_scale, color=FONT_COLOR,thickness=FONT_THICKNESS)



def face_detector_image(img):
    gray = cv2.cvtColor(img.copy(), cv2.COLOR_BGR2GRAY)
    faces = face_classifier.detectMultiScale(gray, 1.3, 5)
    if faces is ():
        return (0, 0, 0, 0), np.zeros((48, 48), np.uint8), img

    allfaces = []
    rects = []
    for (x, y, w, h) in faces:
        cv2.rectangle(img, (x, y), (x + w, y + h), (0, 255, 0), 2)
        roi_gray = gray[y:y + h, x:x + w]
        roi_gray = cv2.resize(roi_gray, (48, 48), interpolation=cv2.INTER_AREA)
        allfaces.append(roi_gray)
        rects.append((x, w, y, h))
        
    return rects, allfaces, img

def emotionImage(imgPath):
    img = cv2.imread(imgPath)
    rects, faces, image = face_detector_image(img)
    expected_shape = (48,48)
    i = 0
    for face in faces:
        roi = face.astype("float") / 255.0
        if roi.shape != expected_shape:
            print("Error processing image")
            label = "No Face"
            break
        else:
            roi = img_to_array(roi)
            if roi is not None:
                roi = np. expand_dims(roi, axis=0)
                preds = classifier.predict(roi)[0]
                label = class_labels[preds.argmax()]
                label_position = (rects[i][0] + int((rects[i][1] / 2)), abs(rects[i][2] - 10))
                i = + 1
                text_on_detected_boxes(label, label_position[0],label_position[1], image)
            else:
                print("Error processing image")
                label = "No Face"
                break
            
    sendDataToDashboard(label)
    #cv2.imshow("Emotion Detector", image)
    cv2.imwrite('output2.jpg', image)
    cv2.waitKey(0)
    cv2.destroyAllWindows()


def getLatLong():
    g = geocoder.ip('me')
    if g.ok:
        return {
        'lat': g.latlng[0],
        'lng': g.latlng[1],
        'address': g.address
        }
    else:
        return None


def sendDataToDashboard(exp):
    current_timestamp = datetime.utcnow().isoformat() + 'Z'
    location = getLatLong()
    url = 'https://trusec-dev-api.azurewebsites.net/api/TruckDataLogs'
    headers = {
        'accept': '/',
        'Content-Type': 'application/json',
    }
    data = {
        "timeStamp": current_timestamp,
        "driverExpression": exp,
        "latitude": location['lat'],
        "longitude": location['lng'],
        "truckId": 1,
        "speedKPH": 0   
    }
    response = requests.post(url, headers=headers, data=json.dumps(data))
    print(response.status_code)
    print(response.json())
    
    
if __name__ == '__main__':
    cam =Picamera2()
    cam_config = cam.create_still_configuration(main={"size": (500, 334)}, lores={"size": (500, 334)}, display="lores")
    cam.configure(cam_config)
    while True:
        cam.start()
        cam.capture_file("driver_img2.jpg")
        time.sleep(3)
        emotionImage('driver_img2.jpg')
                 
