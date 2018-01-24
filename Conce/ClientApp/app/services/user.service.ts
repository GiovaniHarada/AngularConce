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

    constructor() { }

}
