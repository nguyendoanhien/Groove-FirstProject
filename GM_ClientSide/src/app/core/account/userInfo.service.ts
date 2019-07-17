import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { userInfo } from '../../apps/chat/sidenavs/left/user/userInfo.model';

const httpOptions = {
    headers: new HttpHeaders({
        "Access-Control-Allow-Origin": "*",
        "Access-Control-Allow-Credentials": "true",
        "Access-Control-Allow-Methods": "GET,HEAD,OPTIONS,POST,PUT",
        "Access-Control-Allow-Headers": "Origin, X-Requested-With, Content-Type, Accept, Authorization"
    })
    ,responseType: 'text' as 'json'
};

@Injectable()
export class UserInfoService {

    COUNDINARY_URL:string = 'https://api.cloudinary.com/v1_1/groovemessenger/upload'
    COUNDINARY_UPLOAD_PRESET:string = 'pvrfqetm';
    constructor(private router: Router,
        private http: HttpClient) {
    }

    getUserInfo() {
        return this.http.get('https://localhost:44383/api/user').pipe();
    }

    changeDisplayName(userInfo:userInfo) {
        return this.http.put('https://localhost:44383/api/user',userInfo).pipe();
    }

    onUpload(fd:FormData) {

        fd.append('upload-preset',this.COUNDINARY_UPLOAD_PRESET)
        return this.http.post(this.COUNDINARY_URL,fd,httpOptions).pipe();
    }

}