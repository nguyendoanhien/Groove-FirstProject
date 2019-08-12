import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../../environments/environment";
import { Observable } from "rxjs";
import { IndexMessageModel } from "app/models/indexMessage.model";
import { map } from "rxjs/operators";
const cloudinaryUrl = environment.cloudinary.url;
const cloudinaryPreset = environment.cloudinary.upload_preset;

@Injectable()
export class MessageService {

    constructor(private http: HttpClient) {

    }

    addMessage(model: IndexMessageModel): Observable<any> {
        return this.http.post(environment.apiMessageUrl, model);
    }
    addMessageToFroup(model: IndexMessageModel): Observable<any> {
        return this.http.post(environment.apiMessageWithGroup, model);
    }
    sendUnreadMessages(conversationId: string): Observable<any> {
        return this.http.get(environment.apiUnreadMessage + conversationId);
    }

    updateUnreadMessages(conversationId: string): Observable<any> {
        return this.http.get(environment.apiReadMessage + conversationId);
    }

    onUpload(fd: FormData, image: IndexMessageModel, isGroup: boolean) {
        fd.append("upload_preset", cloudinaryPreset);
        return this.http.post(cloudinaryUrl, fd).pipe(
            map((res: any) => {
                image.content = res.url;
                image.type = "Image";
                if (!isGroup) {
                    this.addMessage(image).subscribe(data => {
                        this.sendUnreadMessages(image.conversationId).subscribe();
                    });

                }
                else {
                    image.receiver = null;
                    this.addMessageToFroup(image).subscribe(data => {
                        this.sendUnreadMessages(image.conversationId).subscribe();
                    });
                }
                return res.url;
            }));
    }
}