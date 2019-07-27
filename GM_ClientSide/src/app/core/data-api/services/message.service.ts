import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { IndexMessageModel } from 'app/models/indexMessage.model';
import { UnreadMessage } from 'app/models/UnreadMessage.model';


@Injectable()
export class MessageService {

    constructor(private http: HttpClient) {

    }
    addMessage(model: IndexMessageModel): Observable<any> {
        return this.http.post(environment.apiMessageUrl, model);
    }

    sendUnreadMessages(conversationId: string): Observable<any> {
        return this.http.get(environment.apiUnreadMessage + conversationId);
    }

    updateUnreadMessages(conversationId: string): Observable<any> {
        return this.http.get(environment.apiReadMessage + conversationId);
    }
}


