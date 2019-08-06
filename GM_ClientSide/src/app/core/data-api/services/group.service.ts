import { Router } from "@angular/router";
import { Injectable } from "@angular/core";
import { AuthService } from "../../auth/auth.service";
import { environment } from "../../../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { BehaviorSubject, Observable } from "rxjs";
import { map } from "rxjs/operators";
const cloudinaryUrl = environment.cloudinary.url;
const cloudinaryPreset = environment.cloudinary.upload_preset;



@Injectable()
export class GroupService {
    contactGroup: any = [];
    nameGroup: string = '';
    avatarGroup: string = 'https://res.cloudinary.com/groovemessenger/image/upload/v1564275178/kpzefhbl03rklqh58tos.jpg';
    constructor(private _httpClient: HttpClient) {

    }

    initAddGroup() {
        this.contactGroup = [];
        this.nameGroup = '';
        this.avatarGroup = 'https://res.cloudinary.com/groovemessenger/image/upload/v1564275178/kpzefhbl03rklqh58tos.jpg'
    }

    onUpload(fd: FormData) {
        fd.append("upload_preset", cloudinaryPreset);
        return this._httpClient.post(cloudinaryUrl, fd).pipe(
            map((res: any) => {
                this.avatarGroup = res.url;
            }));
    }

    updateUpLoadAvatar(fd: FormData) {
        fd.append("upload_preset", cloudinaryPreset);
        return this._httpClient.post(cloudinaryUrl, fd).pipe();
    }

    addGroup() {
        var group = { name: this.nameGroup, avatar: this.avatarGroup, members: this.contactGroup };
        return this._httpClient.post(environment.apiGetConversationGroup,group).pipe();  
    }

    getGroupChat(): Observable<any[]> {
        return this._httpClient.get<any[]>(environment.apiGetConversationGroup);    
    }

    editGroupChat(conv:any) {
        return this._httpClient.put(environment.apiConvUrl,conv).pipe();
    }

}