# Real-Time Facial Expression Detection Dashboard

This project captures images using a Raspberry Pi, runs a facial expression detection AI model, and displays the real-time results on an Angular dashboard using a .NET backend with SignalR for real-time communication.

## Table of Contents
- [Overview](#overview)
- [Prerequisites](#prerequisites)
- [Setup](#setup)
  - [Raspberry Pi](#raspberry-pi)
  - [.NET Backend](#net-backend)
  - [Angular Frontend](#angular-frontend)
- [Running the Project](#running-the-project)
- [Troubleshooting](#troubleshooting)
- [Contributing](#contributing)
- [License](#license)

## Overview
The system captures images from a Raspberry Pi camera, processes the images using a pre-trained facial expression detection model, and sends the results to a .NET backend. The backend uses SignalR to push real-time updates to an Angular frontend, which displays the current facial expression.

## Prerequisites
- Raspberry Pi with camera module
- .NET SDK
- Node.js and Angular CLI
- Python and required Python packages (Tensorflow, requests)
