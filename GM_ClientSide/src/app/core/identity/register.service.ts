import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { environment } from '../../../environments/environment';
import { HttpClient} from '@angular/common/http';
import { RegisterModel } from 'app/account/register/register.model';

const authUrl = environment.authUrl;

@Injectable()
export class RegisterService {

    private registerModel: RegisterModel;
    constructor(private router: Router,
                private authService: AuthService,
                private http: HttpClient) {
        this.registerModel = new RegisterModel();
    }
}
