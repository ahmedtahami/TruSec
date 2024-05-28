import { Truck } from "./Truck";


export class TruckDataLog {
    id?: number;
    timeStamp?: Date;
    driverExpression?: string;
    latitude?: number;
    longitude?: number;
    truckId?: number;
    truck?: Truck;
    speedKPH?: number;
}
