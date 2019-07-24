import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, retry, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { UserInfo } from 'app/apps/chat/sidenavs/left/user/userInfo.model';
import { ProfileHubService } from '../data-api/hubs/profile.hub';
const apiUserUrl = environment.apiUserUrl;
const cloudinaryUrl = environment.cloudinary.url;
const cloudinaryPreset = environment.cloudinary.upload_preset;
@Injectable()
export class UserInfoService {

    userInfo: UserInfo

    constructor(private router: Router,
        private http: HttpClient,
        ) {
        this.userInfo = new UserInfo();
    }

    getUserInfo() {
        return this.http.get(apiUserUrl).pipe(
            map((res: any) => this.userInfo = res as UserInfo)
        );
    }

    changeDisplayName() {
        return this.http.put(apiUserUrl, this.userInfo).pipe(
            map((res: any) => {
                this.userInfo = res as UserInfo;
            })
        );
    }

    onUpload(fd: FormData) {


        fd.append('upload_preset', cloudinaryPreset)
        return this.http.post(cloudinaryUrl, fd).pipe(
            map((res: any) => {
                this.userInfo.avatar = res.url;
               
                this.changeDisplayName().subscribe();
            }));
    }


}


