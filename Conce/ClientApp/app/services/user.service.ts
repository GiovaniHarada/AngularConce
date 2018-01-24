import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { UserRegistration } from '../models/user.registration';
import { ConfigService } from '../utils/config.service';

import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx';


@Injectable()
export class UserService {
    baseUrl: string = '';

    private _authNavStatusSource = new BehaviorSubject<boolean>(false);

    authNavStatus$ = this._authNavStatusSource.asObservable();

    private loggedIn = false;

    constructor(private http: Http, private configService: ConfigService) {
        this.loggedIn = !!localStorage.getItem('auth_token');

        this._authNavStatusSource.next(this.loggedIn);
        this.baseUrl = configService.getApiURI();
    }

    register(email: string, password: string, firstName: string, lastName: string): Observable<UserRegistration> {
        let body = JSON.stringify({ email, password, firstName, lastName });
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.baseUrl + "/accounts", body, options)
            .map(res => true);

    }
}
