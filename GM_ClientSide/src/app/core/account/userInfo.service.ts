import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { userInfo } from '../../apps/chat/sidenavs/left/user/userInfo.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';


const apiUserUrl = environment.apiUserUrl;
@Injectable()
export class UserInfoService {

    COUNDINARY_URL = 'https://api.cloudinary.com/v1_1/groovemessenger/upload';
    COUNDINARY_UPLOAD_PRESET = 'qlbjv3if';
    constructor(private router: Router,
                private http: HttpClient) {
    }

    getUserInfo() {
        return this.http.get(apiUserUrl).pipe();
    }

    changeDisplayName(userInfo: userInfo) {
        return this.http.put(apiUserUrl, userInfo).pipe();
    }

    onUpload(fd: FormData) {


        fd.append('upload_preset', this.COUNDINARY_UPLOAD_PRESET);
        return this.http.post(this.COUNDINARY_URL, fd).pipe();
    }

}
