import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { environment } from '../../../environments/environment';
import { HttpClient} from '@angular/common/http';
import { RegisterModel } from 'app/account/register/register.model';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { MailConfirmModel } from 'app/account/mail-confirmer/mail-confirmer.model';
const authUrl = environment.authUrl;

@Injectable()
export class RegisterService {
    email : BehaviorSubject<any>;
    constructor(private router: Router, private authService: AuthService, private http: HttpClient) {
        this.email = new BehaviorSubject(null);
    }

    register(model:RegisterModel) : Observable<any> {
        return this.http.post('https://localhost:44383/Indentity/ClientAccount/register',model);
    }
    confirmEmail(model:MailConfirmModel):Observable<any>{
        return this.http.post('https://localhost:44383/Indentity/ClientAccount/confirmemail',model)
    }
}
