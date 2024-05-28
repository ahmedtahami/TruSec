import { Company } from "./Company";


export class Truck {
    id?: number;
    modelName?: string;
    registrationNumber?: string;
    imageSrc?: string;
    companyId?: number;
    company?: Company;
}
