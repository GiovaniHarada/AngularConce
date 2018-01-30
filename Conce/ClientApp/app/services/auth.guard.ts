import { Injectable } from '@angular/core';
import { UserService } from './user.service';
import { Router, CanActivate } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(protected auth: UserService, protected router: Router) { }

    canActivate() {
        if (this.auth.isLoggedIn())
            return true;

        this.router.navigate(['login']);
        return false;
    }
}