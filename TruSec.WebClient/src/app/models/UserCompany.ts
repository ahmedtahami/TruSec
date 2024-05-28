import { Company } from "./Company";
import { ApplicationUser } from "./ApplicationUser";


export class UserCompany {
    id?: number;
    companyId?: number;
    company?: Company;
    userId?: string;
    user?: ApplicationUser;
}
