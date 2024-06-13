import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';


@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(
        private authService: AuthService,
        private router: Router
    ) { }

    /**
     * Check if the user is logged in. Only allows logged in users.
     * @returns Boolean
     */
    async canActivate(): Promise<any> {
        const isLoggedIn = this.authService.isLoggedIn;
        if (!isLoggedIn) {
            this.router.navigate(['login']);
        }
        return isLoggedIn;
    }
}
