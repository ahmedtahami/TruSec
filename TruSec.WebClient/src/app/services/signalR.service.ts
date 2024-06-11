import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { LogLevel } from '@microsoft/signalr';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;
  constructor() {
    console.log(environment.defaultUrl);
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.defaultUrl}/expressionsHub`)
      .build();
  }

  startConnection(): Promise<void> {
    return this.hubConnection
      .start()
      .catch(err => console.error('Error starting SignalR connection: ', err));
  }

  stopConnection(): Promise<void> {
    return this.hubConnection
      .stop()
      .catch(err => console.error('Error stopping SignalR connection: ', err));
  }

  onTruckDataReceived(callback: (data: any) => void): void {
    this.hubConnection.on('SendExpression', callback);
  }

  joinTruckGroup(truckId: number): void {
    this.hubConnection.invoke('JoinTruckGroup', truckId)
      .catch(err => console.error('Error joining truck group: ', err));
  }

  leaveTruckGroup(truckId: number): void {
    this.hubConnection.invoke('LeaveTruckGroup', truckId)
      .catch(err => console.error('Error leaving truck group: ', err));
  }
}
