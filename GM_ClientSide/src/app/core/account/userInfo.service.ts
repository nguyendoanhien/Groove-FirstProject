import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserInfo } from '../../apps/chat/sidenavs/left/user/userInfo.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';


const cloudinaryUrl = environment.cloudinary.url;
const cloudinaryPreset = environment.cloudinary.upload_preset;



const apiUserUrl = environment.apiUserUrl;
@Injectable()
export class UserInfoService {

    constructor(private router: Router,
                private http: HttpClient) {
    }

    getUserInfo(): Observable<object> {
        return this.http.get(apiUserUrl).pipe();
    }

    changeDisplayName(userInfo: UserInfo): Observable<object> {
        return this.http.put(apiUserUrl, userInfo).pipe();
    }

    onUpload(fd: FormData): Observable<object> {
        fd.append('upload_preset', cloudinaryPreset);
        return this.http.post(cloudinaryUrl, fd).pipe();
    }

}
