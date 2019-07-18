import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { userInfo } from '../../apps/chat/sidenavs/left/user/userInfo.model';
import { catchError, retry, map } from 'rxjs/operators';
@Injectable()
export class UserInfoService {

    userInfo: userInfo
    COUNDINARY_URL: string = 'https://api.cloudinary.com/v1_1/groovemessenger/upload'
    COUNDINARY_UPLOAD_PRESET: string = 'qlbjv3if';
    constructor(private router: Router,
        private http: HttpClient) {
        this.userInfo = new userInfo();
    }

    getUserInfo() {
        return this.http.get('https://localhost:44383/api/user').pipe(
            map((res: any) => this.userInfo = res as userInfo)
        );
    }

    changeDisplayName() {
        return this.http.put('https://localhost:44383/api/user', this.userInfo).pipe(
            map((res: any) => this.userInfo = res as userInfo)
        );
    }

    onUpload(fd: FormData) {


        fd.append('upload_preset', this.COUNDINARY_UPLOAD_PRESET)
        return this.http.post(this.COUNDINARY_URL, fd).pipe(
            map((res: any) => {
                this.userInfo.avatar = res.url;
                this.changeDisplayName().subscribe();
            }));
    }


}