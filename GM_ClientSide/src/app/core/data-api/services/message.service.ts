import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { IndexMessageModel } from 'app/models/indexMessage.model';


@Injectable()
export class MessageService {

    constructor(private http: HttpClient) {

    }
    addMessage(model: IndexMessageModel): Observable<any> {
        return this.http.post(environment.apiMessageUrl, model);
    }
}


