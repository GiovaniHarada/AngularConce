import * as Raven from 'raven-js';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule, BrowserXhr } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ToastyModule } from 'ng2-toasty';
import { ErrorHandler } from '@angular/core';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { VehicleListComponent } from "./components/vehicle-list/vehicle-list.component";
import { PaginationComponent } from './components/shared/pagination.component'
import { ViewVehicleComponent } from "./components/view-vehicle/view-vehicle.component";
import { LoginFormComponent } from './components/login-form/login-form.component';
import { AppErrorHandler } from './app.error-handler';

import { VehicleService } from "./services/vehicle.service";
import { PhotoService } from "./services/photo.service";
import { BrowserXhrWithProgress, ProgressService } from './services/progress.service';
import { RegistrationFormComponent } from './components/registration-form/registration-form.component';
import { UserService } from './services/user.service';
import { ConfigService } from './utils/config.service';
import { AuthGuard } from './services/auth.guard';


Raven.config('https://c5be7b893ecf4f12a9350d8f3d68915f@sentry.io/258923').install();

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        VehicleFormComponent,
        VehicleListComponent,
        PaginationComponent,
        ViewVehicleComponent,
        LoginFormComponent,
        RegistrationFormComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ToastyModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'login', component: LoginFormComponent },
            { path: 'counter', component: CounterComponent, canActivate: [AuthGuard] },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'vehicles', component: VehicleListComponent },
            { path: 'vehicles/new', component: VehicleFormComponent },
            { path: 'vehicles/:id', component: ViewVehicleComponent },
            { path: 'vehicles/edit/:id', component: VehicleFormComponent },
            { path: 'login', component: LoginFormComponent },
            { path: 'register', component: RegistrationFormComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        { provide: ErrorHandler, useClass: AppErrorHandler},
        { provide: BrowserXhr, useClass: BrowserXhrWithProgress },
        VehicleService,
        PhotoService,
        ProgressService,
        UserService,
        ConfigService,
        AuthGuard
    ]
})
export class AppModuleShared {
}
