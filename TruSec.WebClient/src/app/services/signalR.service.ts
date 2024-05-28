import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { LogLevel } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;

  public startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7177/expressionsHub')
      .configureLogging(LogLevel.Information)
      .build();

    this.hubConnection.start().catch(err => console.error('Error while starting connection: ' + err));
  }

  public addTransferDataListener(callback: (data: any) => void) {
    this.hubConnection.on('ReceiveExpression', (data: any) => {
      callback(data);
    });
  }
}
