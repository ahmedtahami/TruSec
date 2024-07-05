import { AfterViewInit, Component, Input, OnChanges, OnDestroy, SimpleChanges } from '@angular/core';
import * as L from 'leaflet';
import { Truck } from '../../models/Truck';
import { SignalRService } from '../../services/signalR.service';

@Component({
  selector: 'app-truck-live-feed',
  templateUrl: './truck-live-feed.component.html',
  styleUrls: ['./truck-live-feed.component.css']
})
export class TruckLiveFeedComponent implements OnChanges, OnDestroy {
  @Input() truck!: Truck;
  public expression!: string;
  private map!: L.Map;
  private marker!: L.Marker;
  public latitude!: number;
  public longitude!: number;
  private truckIcon = L.icon({
    iconUrl: 'assets/logos/logo.png',
    iconSize: [38, 38],
    iconAnchor: [22, 38],
    popupAnchor: [-3, -76]
  });
  constructor(private signalRService: SignalRService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['truck'] && changes['truck'].currentValue) {
      this.signalRService.startConnection().then(() => {
        if (this.truck && this.truck.id) {
          this.signalRService.joinTruckGroup(this.truck.id);
          this.signalRService.onTruckDataReceived(data => {
            debugger;
            if (data.truckId === this.truck.id) {
              this.expression = data.driverExpression ?? '';
              this.latitude = data.latitude ?? NaN;
              this.longitude = data.longitude ?? NaN;
              this.updateMarker();
            }
          });
        }
      });
    }
  }

  ngOnDestroy(): void {
    if (this.truck && this.truck.id) {
      this.signalRService.leaveTruckGroup(this.truck.id);
    }
    this.signalRService.stopConnection();
  }

  initMap(): void {
    setTimeout(() => {
      this.map = L.map('map').setView([0, 0], 2); // Initial map view

      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
      }).addTo(this.map);

      this.marker = L.marker([0, 0], {icon: this.truckIcon}).addTo(this.map)
        .bindPopup('Truck Location');
    }, 0);
  }

  private updateMarker(): void {
    if (this.marker) {
      this.marker.setLatLng([this.latitude, this.longitude]);
      this.map.setView([this.latitude, this.longitude], 13);
      this.marker.getPopup()?.setContent(`${this.truck.modelName}`);
      this.marker.openPopup();
    }
  }
}
